using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DOTween 进阶示例：与项目框架整合
///
/// 演示如何在模块化框架中合理使用 DOTween：
/// 1. UI 界面显示/隐藏动画
/// 2. 按钮交互反馈
/// 3. 动画生命周期管理
/// 4. 与 EventBus 的协作
/// </summary>
public class DOTweenAdvancedExample : MonoBehaviour
{
    [SerializeField] private CanvasGroup panelCanvasGroup;
    [SerializeField] private RectTransform panelRect;
    [SerializeField] private Button showButton;
    [SerializeField] private Button hideButton;

    private Sequence showHideSequence;

    private void Start()
    {
        if (showButton != null)
            showButton.onClick.AddListener(OnShowPanelClicked);

        if (hideButton != null)
            hideButton.onClick.AddListener(OnHidePanelClicked);

        // 初始化面板为隐藏状态
        if (panelCanvasGroup != null)
            panelCanvasGroup.alpha = 0;
    }

    /// <summary>
    /// 示例：显示面板（带动画）
    ///
    /// 这个示例展示：
    /// - 结合 CanvasGroup 的淡入效果
    /// - 使用 Sequence 组合多个动画
    /// - Kill 之前的序列以避免冲突
    /// </summary>
    private void OnShowPanelClicked()
    {
        if (panelCanvasGroup == null || panelRect == null) return;

        // 重要：Kill 之前的序列
        showHideSequence?.Kill();

        // 创建新序列
        showHideSequence = DOTween.Sequence();

        // 组合动画：
        // 1. 淡入（CanvasGroup.alpha 0→1）
        // 2. 缩放进入（0.8→1）
        showHideSequence
            .Join(panelCanvasGroup.DOFade(1f, 0.3f))
            .Join(panelRect.DOScale(1f, 0.3f).From(0.8f))
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                Debug.Log("面板显示完成");
                // 可以在这里发送事件或回调
                // eventBus.Publish(new PanelShowedEvent());
            });
    }

    /// <summary>
    /// 示例：隐藏面板（带动画）
    /// </summary>
    private void OnHidePanelClicked()
    {
        if (panelCanvasGroup == null || panelRect == null) return;

        // Kill 之前的序列
        showHideSequence?.Kill();

        // 创建隐藏序列
        showHideSequence = DOTween.Sequence();

        showHideSequence
            .Join(panelCanvasGroup.DOFade(0f, 0.3f))
            .Join(panelRect.DOScale(0.8f, 0.3f))
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                Debug.Log("面板隐藏完成");
            });
    }

    /// <summary>
    /// 示例：按钮点击反馈动画
    ///
    /// 用于给按钮添加视觉反馈
    /// </summary>
    public void PlayButtonClickFeedback(Transform buttonTransform)
    {
        if (buttonTransform == null) return;

        // 快速缩放反馈：0.95 → 1 → 0.95
        buttonTransform
            .DOScale(0.95f, 0.05f)
            .OnComplete(() =>
            {
                buttonTransform.DOScale(1f, 0.05f);
            });
    }

    /// <summary>
    /// 示例：数字递增动画
    ///
    /// 用于显示金币增加、等级提升等效果
    /// </summary>
    public void AnimateNumberChange(Text targetText, int fromValue, int toValue, float duration)
    {
        if (targetText == null) return;

        var tweener = DOTween.To(
            () => fromValue,
            (value) => targetText.text = value.ToString(),
            toValue,
            duration
        ).SetEase(Ease.OutQuad);
    }

    /// <summary>
    /// 示例：闪烁效果
    ///
    /// 用于提示玩家某个对象需要关注
    /// </summary>
    public void PlayFlashEffect(Image targetImage, Color highlightColor, float duration = 0.5f, int flashCount = 3)
    {
        if (targetImage == null) return;

        Color originalColor = targetImage.color;

        targetImage
            .DOColor(highlightColor, duration / 2)
            .SetLoops(flashCount * 2, LoopType.Yoyo)
            .OnComplete(() =>
            {
                targetImage.color = originalColor;
            });
    }

    /// <summary>
    /// 示例：进度条填充动画
    ///
    /// 用于显示加载、升级等进度
    /// </summary>
    public void AnimateProgressBar(Image progressBar, float targetFill, float duration)
    {
        if (progressBar == null) return;

        progressBar
            .DOFillAmount(targetFill, duration)
            .SetEase(Ease.OutQuad);
    }

    /// <summary>
    /// 重要：清理动画
    ///
    /// 在 UI 销毁时必须调用 Kill()，否则：
    /// 1. 动画继续更新已销毁的对象 → NullReferenceException
    /// 2. 内存泄漏
    /// </summary>
    private void OnDestroy()
    {
        // Kill 当前管理的所有动画
        showHideSequence?.Kill();

        // Kill 与这个 GameObject 关联的所有动画
        DOTween.Kill(gameObject);

        // 如果需要清理按钮事件
        if (showButton != null)
            showButton.onClick.RemoveListener(OnShowPanelClicked);

        if (hideButton != null)
            hideButton.onClick.RemoveListener(OnHidePanelClicked);
    }

    /// <summary>
    /// DOTween 与项目框架的最佳实践：
    ///
    /// 架构建议：
    /// 1. 创建一个 TweenManager 模块来集中管理所有动画（避免内存泄漏）
    /// 2. UI 脚本中使用 DOTween，逻辑脚本通过事件通知 UI
    /// 3. 在 OnDestroy 中始终调用 Kill()
    /// 4. 使用 SetId() 来标识动画组，便于统一管理
    /// 5. 避免在 Coroutine 中使用 DOTween，容易产生意外行为
    ///
    /// 性能优化：
    /// 1. 使用对象池回收频繁创建的动画对象
    /// 2. 避免在 Update 中创建 Tween，应该在事件处理中创建
    /// 3. 使用 Pooling 来复用 Sequence 对象
    /// 4. 对于大量对象的动画，考虑使用 Jobs System
    ///
    /// 常见错误：
    /// ❌ 不调用 Kill() → 内存泄漏
    /// ❌ 频繁 Kill/Create 同一个动画 → 性能下降
    /// ❌ 在 Destroy 前不停止动画 → 崩溃
    /// ❌ 忘记 Ease 函数 → 动画看起来生硬
    /// ✅ 使用 DOTween 可大大简化动画代码
    /// </summary>
}
