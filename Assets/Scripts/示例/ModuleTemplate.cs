using System;
using System.Threading;
using Cysharp.Threading.Tasks;

/// ================================================================
/// 游戏模块模板 — IGameModule 标准实现
///
/// 使用说明：
/// 1. 复制此文件到 Assets/Scripts/Modules/YourModule/
/// 2. 改名为具体的模块名（如 CombatModule.cs）
/// 3. 替换 YourModule 为实际的模块名
/// 4. 根据需要实现初始化逻辑和事件处理器
/// ================================================================

public class YourModule : IGameModule
{
    // ========== 模块元数据 ==========

    /// <summary>
    /// 模块功能类别。根据模块职责选择：
    ///
    /// 0 = 启动基础层（必须最早可用的底层能力）
    ///     示例：ConfigModule, ResourceModule, EventBus
    ///
    /// 1 = 运行服务层（可被多个上层模块复用的服务）
    ///     示例：AudioModule, NetworkModule, LocalizationModule
    ///
    /// 2 = 应用协调层（流程、场景、状态等协调能力）
    ///     示例：UIModule, InputModule, CameraModule
    ///
    /// 3 = 项目扩展层（项目自定义模块的默认位置）
    ///     示例：CombatModule, QuestModule, PlayerModule
    ///
    /// 4 = 后台辅助层（不阻塞主要启动路径，默认值）
    ///     示例：AnalyticsModule, CrashReportModule, PerformanceMonitor
    /// </summary>
    public int ModuleCategory => 3;

    /// <summary>
    /// 硬依赖。必须放具体的 Module 类型，不支持接口。
    ///
    /// ModuleRunner 保证这些模块初始化完成后才开始本模块的 InitializeAsync。
    /// 如果没有依赖，返回 Type.EmptyTypes。
    /// </summary>
    public Type[] Dependencies => new[] { typeof(SomeDependencyModule) };

    // ========== 外部模块引用缓存 ==========

    // private ModuleRunner _runner;           // 如果在 InitAsync 中需要 GetModule<T>
    // private SomeDependencyModule _dependency; // 频繁使用的依赖模块，缓存引用

    // ========== 初始化 ==========

    /// <summary>
    /// 模块初始化。Dependencies 保证已完成初始化。
    ///
    /// 初始化流程：
    /// 1. 获取依赖模块的引用（通过 GetModule<T>）
    /// 2. 执行本模块的初始化逻辑
    /// 3. 初始化内部状态
    ///
    /// ⚠️ 重要：InitAsync 期间不发布事件。时序通信用 Dependencies 表达。
    /// </summary>
    public async UniTask InitializeAsync(CancellationToken ct = default)
    {
        // 1. 缓存依赖模块引用
        // _dependency = _runner.GetModule<SomeDependencyModule>();

        // 2. 执行初始化逻辑
        // await LoadConfigAsync(ct);
        // await InitializeStateAsync(ct);

        // 3. 初始化完成
        await UniTask.CompletedTask;
    }

    // ========== 关闭 ==========

    /// <summary>
    /// 模块关闭。按初始化完成序的逆序调用。
    ///
    /// 清理流程：
    /// 1. 取消临时订阅
    /// 2. 保存数据
    /// 3. 释放占用的资源
    ///
    /// ℹ️ ShutdownAsync 期间可以发布事件（注销在 ShutdownAsync 之后）
    /// </summary>
    public async UniTask ShutdownAsync(CancellationToken ct = default)
    {
        // 清理逻辑
        // await SaveDataAsync(ct);

        await UniTask.CompletedTask;
    }

    // ========== 事件处理器 ==========

    /// <summary>
    /// 异步事件处理示例。
    ///
    /// [EventHandler] 方法在 InitAsync 完成后自动注册，
    /// 在 ShutdownAsync 完成后自动注销。
    ///
    /// 签名规则：
    /// - 必须有且仅有一个参数（事件类型）
    /// - 返回类型可以是 void 或 UniTask
    /// - 参数类型必须是 class
    /// </summary>
    [EventHandler]
    async UniTask OnTestEventAsync(TestEvent _)
    {
        // 异步处理事件
        // 示例：等待某个操作完成
        await UniTask.CompletedTask;
    }

    /// <summary>
    /// 同步事件处理示例。
    /// </summary>
    [EventHandler]
    void OnAnotherEvent(AnotherEvent _)
    {
        // 同步处理事件
        // 示例：立即更新状态
    }

    /// <summary>
    /// 请求响应处理器示例。
    ///
    /// [RequestHandler] 方法在 InitAsync 完成后自动注册。
    ///
    /// 用于需要立即返回结果的场景：
    /// - 数据查询
    /// - 权限检查
    /// - 状态查询
    /// </summary>
    [RequestHandler]
    QueryResult OnQuery(QueryRequest req)
    {
        return new QueryResult { /* ... */ };
    }

    // ========== 内部方法示例 ==========

    // private async UniTask LoadConfigAsync(CancellationToken ct)
    // {
    //     // 加载配置的具体实现
    //     await UniTask.CompletedTask;
    // }

    // private async UniTask SaveDataAsync(CancellationToken ct)
    // {
    //     // 保存数据的具体实现
    //     await UniTask.CompletedTask;
    // }
}

// ========== 示例事件和请求类型 ==========
// 实际项目中替换为真实的事件类型
// 注意：这些是模板示例，实际使用时应在 Assets/Scripts/Events/ 中定义真实的事件类

public class TestEvent { }
public class AnotherEvent { }
public class QueryRequest { }
public class QueryResult { }

// ========== 示例依赖模块 ==========
// 实际项目中替换为真实的模块类型

public class SomeDependencyModule : IGameModule
{
    public int ModuleCategory => 0;
    public Type[] Dependencies => Type.EmptyTypes;
    public UniTask InitializeAsync(CancellationToken ct = default) => UniTask.CompletedTask;
    public UniTask ShutdownAsync(CancellationToken ct = default) => UniTask.CompletedTask;
}

/// <summary>
/// ModuleCategory 选择指南：
///
/// 选择 0（启动基础层）：
/// ✓ ConfigModule - 加载全局配置
/// ✓ ResourceModule - 管理资源加载
/// ✓ DatabaseModule - 初始化数据库
/// ✗ UIModule - 太早初始化会浪费资源
///
/// 选择 1（运行服务层）：
/// ✓ AudioModule - 被多个模块需要
/// ✓ NetworkModule - 基础网络能力
/// ✓ LocalizationModule - 本地化服务
/// ✗ CombatModule - 只被游戏逻辑使用
///
/// 选择 2（应用协调层）：
/// ✓ UIModule - 协调所有 UI 显示
/// ✓ InputModule - 管理玩家输入
/// ✓ CameraModule - 管理相机视角
/// ✗ ConfigModule - 不需要协调其他模块
///
/// 选择 3（项目扩展层）：
/// ✓ CombatModule - 项目自定义游戏逻辑
/// ✓ QuestModule - 项目自定义系统
/// ✓ PlayerModule - 项目自定义功能
/// ✓ 大多数游戏业务逻辑模块
///
/// 选择 4（后台辅助层）：
/// ✓ AnalyticsModule - 数据分析，不影响主流程
/// ✓ CrashReportModule - 崩溃上报，后台运行
/// ✓ PerformanceMonitor - 性能监控，可选能力
/// </summary>
