/// ================================================================
/// 事件类型模板 — 标准写法参考
///
/// 使用说明：
/// 1. 复制此文件到 Assets/Scripts/Events/
/// 2. 改名为具体的事件名（如 PlayerDiedEvent.cs）
/// 3. 替换 SomeEvent 为你的事件名
/// 4. 根据需要添加事件数据字段
/// ================================================================

/// <summary>
/// 事件命名规范：[描述]Event
///
/// 设计原则：
/// - 事件类型定义为 class
/// - 数据应为只读（构造时传入，之后不可修改）
/// - 只存储事件相关的数据，不包含逻辑
/// </summary>
public class SomeEvent
{
    /// <summary>事件携带的数据示例。</summary>
    public int SomeData { get; }

    public SomeEvent(int data)
    {
        SomeData = data;
    }
}

/// <summary>
/// 文件存放位置规则：
///
/// 跨模块共享的事件 → Assets/Scripts/Events/
/// 示例：PlayerDiedEvent, GameWinEvent, ItemObtainedEvent
///
/// 只被一个模块使用的事件 → 模块文件夹下的 Events/ 子目录
/// 示例：Assets/Scripts/Modules/CombatModule/Events/
/// </summary>

// ========== 常见事件模板 ==========

/// <summary>事件 1：带有单个整数值的事件</summary>
public class IntValueEvent
{
    public int Value { get; }

    public IntValueEvent(int value)
    {
        Value = value;
    }
}

/// <summary>事件 2：带有多个参数的事件</summary>
public class ComplexEvent
{
    public string Message { get; }
    public int Count { get; }
    public bool IsSuccess { get; }

    public ComplexEvent(string message, int count, bool isSuccess)
    {
        Message = message;
        Count = count;
        IsSuccess = isSuccess;
    }
}

/// <summary>事件 3：无数据的事件（纯通知）</summary>
public class SimpleNotificationEvent
{
    // 无需存储任何数据，仅作为信号
}
