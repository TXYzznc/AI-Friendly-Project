# AI 友好型项目 — 项目指南

## AI 行为准则（每次任务必须严格执行）

> 以臆猜接口为耻，以认真查询为荣。
> 以模糊执行为耻，以寻求确认为荣。
> 以臆想业务为耻，以人类确认为荣。
> 以创造接口为耻，以复用现有为荣。
> 以跳过验证为耻，以主动测试为荣。
> 以破坏架构为耻，以遵循规范为荣。
> 以假装理解为耻，以诚实无知为荣。
> 以盲目修改为耻，以谨慎重构为荣。

- 本项目为 Unity 游戏项目，自研轻量模块化框架。始终用**中文**回答。
- 回复尽量简洁，不要加无关的客套话。
- 优先用简单方案，不要过度工程。
- 涉及项目代码和业务开发时，必须先遵循 [CLAUDE.md]中的约束和规范。
- 查找具体系统或问题的文档时，先查阅**项目知识库索引**：
  - [INDEX.md](../项目知识库（AI自行维护）/INDEX.md) — 项目知识库索引
  - [GITNEXUS.md](./GITNEXUS.md) — 代码分析工具指南
- 所有按键输入必须走 InputModule。

## 需求分析流程（必须执行）

每当收到一个新需求时，**必须先判断是否需要多Agent协作**：

1. **解析需求** — 提取核心目标、约束、涉及的系统
2. **判断模式** — 是否需要多Agent协作？（参考 [AGENTS.md](./AGENTS.md)）
   - ✅ 需要 → 进入Step 3
   - ❌ 不需要 → 直接设计实现方案
3. **选择协作模式** — 并行 / 顺序 / DAG / 事件驱动（详见 [AGENTS.md](./AGENTS.md)）
4. **拆分任务** — 明确各Agent的职责和数据流
5. **设计同步** — 定义结果聚合和错误处理机制
6. **执行** — 按照设计实现

**快速参考**: [AGENTS.md](./AGENTS.md) 包含完整的模式指南、检查清单、常见陷阱。

---

## 工作流系统（项目级）

> 参考文档：[工作/README.md](../工作/README.md) 和 [工作/QUICKSTART.md](../工作/QUICKSTART.md)

### 工作流 5 个 Phase

本项目使用**结构化的功能开发工作流**，与 OpenSpec SKILL 和多Agent协作整合：

```
Phase 1️⃣ 策划讨论 → Phase 2️⃣ 需求拆解 → Phase 3️⃣ 执行追踪 → Phase 4️⃣ 测试验证 → Phase 5️⃣ 归档完成
```

### Phase 映射与自动化

| Phase | 你的操作 | 我的自动行为 | 输出位置 |
|-------|---------|-----------|---------|
| **1️⃣** | 描述需求，多轮讨论，最后确认 | 记录讨论，生成策划案 | [工作/1.策划/](../工作/1.策划/) |
| **2️⃣** | （无需操作，多Agent自动执行） | **需求Agent**: 调用 `/openspec new change` 拆解需求<br>**美术Agent**: 分析美术资源，生成提示词 | [工作/2.需求列表/](../工作/2.需求列表/)<br>[工作/1.美术/](../工作/1.美术/)<br>[工作/3.正在处理的任务/](../工作/3.正在处理的任务/) |
| **3️⃣** | 实现代码，更新进度 | 追踪 tasks.md 进度 | [工作/3.正在处理的任务/](../工作/3.正在处理的任务/) |
| **4️⃣** | （无需操作，自动执行） | 设计测试方案，编写脚本，执行验证 | [工作/3.测试/](../工作/3.测试/) |
| **5️⃣** | （无需操作，自动执行） | 调用 `/openspec archive-change`，生成完成清单 | [工作/4.已归档任务/](../工作/4.已归档任务/) |

### OpenSpec 整合规则

**何时调用 OpenSpec**：
- **进入 Phase 2️⃣** 时：`/openspec new change "NN-功能名"` 创建变更
- **进入 Phase 3️⃣** 时：`/openspec apply-change` 实现任务
- **进入 Phase 4️⃣** 时：`/openspec verify-change` 验证实现
- **进入 Phase 5️⃣** 时：`/openspec archive-change` 归档变更

### 多Agent协作规则

**Phase 2️⃣ 时触发并行协作**：
```
需求Agent:
  1. 读取 工作/1.策划/NN.功能名/策划案.md
  2. 调用 /openspec new change "NN-功能名"
  3. 生成 spec.md + design.md + tasks.md
  4. 保存到 工作/3.正在处理的任务/NN.功能名/

美术Agent:
  1. 读取 工作/1.策划/NN.功能名/策划案.md
  2. 分析美术资源需求
  3. 生成 美术需求分析.md + 提示词.md
  4. 保存到 工作/1.美术/NN.功能名/

⚠️ 注意：
- 实际示例文件夹名为 01.背包系统（示例），遵循 NN.功能名（标记） 的格式
- 版本化时改为 NN.功能名_v1（标记）
```

### 文件夹命名规则

**必须遵循**：所有功能文件夹都带顺序前缀
```
NN.功能名  （NN = 两位数序号）

例如：
  01.背包系统
  02.战斗系统
  03.任务系统
```

**版本化**：
```
首次：NN.功能名  →  归档后  →  NN.功能名_v1
迭代：NN.功能名_v2  →  归档后  →  NN.功能名_v2
```

## 技术栈

- **框架核心**：自研轻量 IGameModule / ModuleRunner / EventBus（详见 [设计文档](#设计文档)）
- **UniTask**：所有异步操作用 `await UniTask`，不用协程
- **DOTween**：UI 动画（[Assets/Demigiant/](../Assets/Demigiant/)）
- **DataTable**：Excel → 自动生成 .cs + .bytes（不要手改生成文件）
- **开发时热重载**：Unity Domain Reload + Enter Play Mode Options

## SKILL 系统

所有 SKILL 的索引和使用指南 → [skills/SKILLS_INDEX.md](./skills/SKILLS_INDEX.md)

任务开始前先查看该文件，确认是否有可用的 SKILL。大部分 SKILL 的详细指令以 references 文件形式存在，必须通过 SKILLS_INDEX.md 和对应的 SKILL.md 手动索引。

## 框架核心概念

### 模块系统

```
IGameModule          — 模块接口（ModuleCategory, Dependencies, InitAsync, ShutdownAsync）
ModuleRunner         — 模块生命周期管理（WhenAny + 持续扫描）
EventBus             — 模块间通信（Publish / Subscribe / RequestAsync）
[EventHandler]       — 属性驱动的事件订阅（不在类声明上挂接口）
ModuleRunner.GetModule<T>() — 高频数据查询的缓存入口
```

### 模块通信

| | EventBus | GetModule\<T\> |
|---|---|---|
| 用途 | "某件事发生了" | "当前状态是什么" |
| 方式 | Publish / Subscribe | 读缓存引用 |
| 开销 | 异步 | 同步，零开销 |

### 初始化流程

1. GameApp.Start() 创建 EventBus → ModuleRunner → AddModule → StartAsync
2. ModuleRunner：依赖校验 → 循环检测 → WhenAny + 持续扫描初始化
3. 模块 InitAsync 完成后自动扫描 [EventHandler] 并注册
4. 所有模块就绪 → 游戏启动

## 命名规范

| 类型 | 规范 | 示例 |
|---|---|---|
| 模块 | `[Name]Module` | `CombatModule`, `UIModule` |
| 事件 | `[描述]Event` | `CombatEndEvent`, `HPChangedEvent` |
| 事件处理方法 | `On[事件名]` | `OnCombatEnd`, `OnHPChanged` |
| UI 表单 | `[Name]UI` 或 `[Name]UIForm` | `GameUIForm` |
| 管理器（非模块） | `[Name]Manager` | `PlayerInputManager` |

## 关键约束

- **ModuleCategory 必须正确设置**。参照 [IGameModule.cs](../Assets/Scripts/Core/IGameModule.cs#L12-L18) 中的优先级规则
- **Dependencies 必须是具体类型**，不支持接口
- **InitAsync 期间不发事件**，初始化通信用 Dependencies
- **行为修改走事件，数据读取走 GetModule\<T\>**
- 异步方法名必须以 `Async` 结尾，返回 `UniTask` 或 `UniTask<T>`
- 不要硬编码数值，所有配置读 DataTable
- **遇到必须手动完成的任务时，必须先通知用户完成后再继续开发**：
  - 配置表更新（新增字段、新建表 → 运行 DataTableGenerator）
  - Prefab 创建与 UI 层级搭建
  - 正确顺序：**用户先定义 Prefab/配置表 → 工具生成 → 再编写对应逻辑脚本**
- 输出日志使用 `FrameworkLogger` 类（详见 [02-AI友好型日志规范.md](../AI友好型项目探讨/02-AI友好型日志规范.md)）

## 日志规范

日志使用 `FrameworkLogger`，格式为 `[模块名|级别] 类型标识 Key=Value... Location=文件:行号`。

```csharp
FrameworkLogger.Error("EventBus", $"Event=CombatEndEvent Handler=CombatModule.OnCombatEnd Exception={ex.GetType().Name} Msg=\"{ex.Message}\"");
FrameworkLogger.Warn("ModuleRunner", $"Module=QuestModule InitAsync 超时 Elapsed=30s");
FrameworkLogger.Info("EventBus", $"Handler=CombatModule Subscribe=CombatEndEvent");
```

## 常见陷阱

- `async void` 方法无法被 `await`，导致时序问题——一律改为返回 `UniTask`
- UI 关闭时 DOTween 动画可能还在播放，需要 `DOTween.Kill(target)` 或 `DOComplete`
- `[EventHandler]` 方法签名错误不会在编译期报错，ModuleRunner 注册时会校验
- 临时订阅（`EventBus.Subscribe`）必须用 `IDisposable` 管理，否则内存泄漏
- Domain Reload 会导致静态字段归零，ModuleRunner 不做静态单例

## 设计文档

| 文档 | 内容 |
|---|---|
| [01-框架核心设计概述.md](../AI友好型项目探讨/01-框架核心设计概述.md) | 框架架构、设计原则 |
| [02-AI友好型日志规范.md](../AI友好型项目探讨/02-AI友好型日志规范.md) | 结构化日志格式、grep 速查 |
| [03-模块系统详细设计.md](../AI友好型项目探讨/03-模块系统详细设计.md) | IGameModule、ModuleRunner、调度优先级 |
| [04-事件系统详细设计.md](../AI友好型项目探讨/04-事件系统详细设计.md) | EventBus、[EventHandler]、三种通信模式 |
| [05-项目文件结构.md](../AI友好型项目探讨/05-项目文件结构.md) | 完整目录结构 |

## 压缩时保留

压缩上下文时，始终保留：
- 已修改的文件列表
- 当前 Phase 编号和完成状态
- 关键架构决策（如为什么选某个方案）