using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DOTween 使用示例
///
/// DOTween 是一个强大的 Unity 补间动画库，用于创建平滑的动画效果。
/// 官方文档：http://dotween.demigiant.com/
///
/// 关键特性：
/// - 支持所有 Transform、UI、材质、颜色等属性的动画
/// - 支持动画序列和回调
/// - 支持 Ease（缓动）函数
/// - 支持 Killed / Looped 等控制
/// - 高性能，支持 JobSystem
/// </summary>
public class DOTweenExample : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Image targetImage;
    [SerializeField] private Text targetText;

    private Sequence animSequence;

    private void Start()
    {
        // 示例 1: 基础 Transform 动画
        ExampleBasicTransform();

        // 示例 2: UI 动画
        // ExampleUIAnimation();

        // 示例 3: 序列动画
        // ExampleSequence();

        // 示例 4: 回调和生命周期
        // ExampleCallbacks();
    }

    /// <summary>
    /// 示例 1: 基础的 Transform 动画
    /// </summary>
    private void ExampleBasicTransform()
    {
        if (targetTransform == null) return;

        // 移动动画
        targetTransform.DOMove(new Vector3(5, 0, 0), 2f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Debug.Log("移动动画完成"));

        // 旋转动画
        targetTransform.DORotate(new Vector3(0, 360, 0), 3f)
            .SetEase(Ease.Linear);

        // 缩放动画
        targetTransform.DOScale(2f, 1.5f)
            .SetEase(Ease.OutElastic);
    }

    /// <summary>
    /// 示例 2: UI 动画
    /// </summary>
    private void ExampleUIAnimation()
    {
        if (targetImage == null) return;

        // 淡入淡出
        targetImage.DOFade(0.5f, 1f)
            .SetEase(Ease.InOutQuad);

        // 按钮点击缩放效果
        // targetImage.transform.DOScale(1.1f, 0.1f)
        //     .OnComplete(() =>
        //     {
        //         targetImage.transform.DOScale(1f, 0.1f);
        //     });

        // 颜色变化
        Color targetColor = new Color(1, 0, 0, 1);
        targetImage.DOColor(targetColor, 2f)
            .SetEase(Ease.InOutQuad);
    }

    /// <summary>
    /// 示例 3: 序列动画
    ///
    /// Sequence 允许你将多个 Tween 组合成一个动画序列
    /// </summary>
    private void ExampleSequence()
    {
        if (targetTransform == null) return;

        // Kill 之前的序列（避免重复播放）
        animSequence?.Kill();

        // 创建新序列
        animSequence = DOTween.Sequence();

        // 序列：移动 → 旋转 → 缩放
        animSequence
            .Append(targetTransform.DOMove(Vector3.right * 5, 1f))
            .Append(targetTransform.DORotate(Vector3.up * 360, 1f))
            .Append(targetTransform.DOScale(2f, 1f))
            .AppendInterval(0.5f) // 等待 0.5 秒
            .Append(targetTransform.DOMove(Vector3.zero, 1f))
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Debug.Log("序列动画完成"));
    }

    /// <summary>
    /// 示例 4: 回调和生命周期管理
    /// </summary>
    private void ExampleCallbacks()
    {
        if (targetTransform == null) return;

        targetTransform
            .DOMove(Vector3.up * 3, 2f)
            .SetEase(Ease.InOutQuad)
            .OnStart(() => Debug.Log("动画开始"))
            .OnUpdate(() => Debug.Log($"当前位置: {targetTransform.position}"))
            .OnComplete(() => Debug.Log("动画完成"))
            .OnKill(() => Debug.Log("动画被杀死"));

        // 使用 SetId 来管理动画
        targetTransform.DOMove(Vector3.down, 1f).SetId("moveAnim");
    }

    /// <summary>
    /// 重要：在对象销毁时 Kill 所有动画，避免内存泄漏
    /// </summary>
    private void OnDestroy()
    {
        animSequence?.Kill();
        DOTween.Kill(targetTransform); // Kill 所有与这个对象相关的动画
    }

    /// <summary>
    /// DOTween 最佳实践总结：
    ///
    /// 1. 使用 DOTween.Sequence() 来管理多个动画
    /// 2. 使用 SetEase() 来设置缓动函数（推荐 InOutQuad, OutQuad 等）
    /// 3. 在对象销毁时调用 Kill() 清理动画
    /// 4. 使用 OnComplete() 来处理动画完成后的逻辑
    /// 5. 使用 SetId() 来标识和管理动画组
    /// 6. 对于 UI 动画，记得调用 DOFade() 前检查 CanvasGroup
    /// 7. 避免在 Update 中创建新的 Tween，应该在 Start 或事件中创建
    /// 8. 对于需要暂停/恢复的动画，保存 Tween 引用并使用 Pause() / Play()
    /// </summary>
}
