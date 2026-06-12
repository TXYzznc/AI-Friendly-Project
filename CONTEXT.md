# 项目上下文 — AI 快速理解指南

> 本文件用于让 AI 在新会话中快速理解项目状态。每次重大变更后应更新。

---

## 一、项目是什么

**AI 友好型轻量游戏开发框架** — 一个为 Unity 回合制 RPG 设计的自研模块化框架。

核心思路：框架只做两件事——**模块生命周期管理** + **模块间通信**。不引入 DI 容器，依赖关系显式声明在模块自身。

---

## 二、已完成

### 2.1 框架设计（全部讨论完成，已写入文档）

| 文档 | 内容 |
|---|---|
| `AI友好型项目探讨/01-框架核心设计概述.md` | 项目愿景、架构全景、设计原则 |
| `AI友好型项目探讨/02-AI友好型日志规范.md` | 结构化日志格式（`[模块名\|级别] Key=Value Location=文件:行号`） |
| `AI友好型项目探讨/03-模块系统详细设计.md` | IGameModule 接口、ModuleCategory(0-4)、Dependencies(具体类型)、WhenAny 持续扫描、调度优先级 `Category×100-OutDegree`、GetModule\<T\> |
| `AI友好型项目探讨/04-事件系统详细设计.md` | EventBus 三种模式(Fire-and-Forget/PublishAndWait/RequestAsync)、`[EventHandler]`/`[RequestHandler]` 属性驱动、诊断报告 |
| `AI友好型项目探讨/05-项目文件结构.md` | 完整目录结构，每个文件夹有 README.md |

### 2.2 框架代码（11 个文件，已实现）

**Core/ (7 个文件):**
| 文件 | 说明 |
|---|---|
| `Assets/Scripts/Core/EventHandlerAttribute.cs` | 广播事件处理器标记 |
| `Assets/Scripts/Core/RequestHandlerAttribute.cs` | 请求响应处理器标记 |
| `Assets/Scripts/Core/IGameModule.cs` | 模块接口（ModuleCategory/Dependencies/InitAsync(CancellationToken)/ShutdownAsync(CancellationToken)） |
| `Assets/Scripts/Core/FrameworkLogger.cs` | AI 友好型日志（Error/Warn/Info/Debug + `[CallerFilePath]` 自动定位） |
| `Assets/Scripts/Core/EventBus.cs` | 事件系统（广播+请求响应分离、手动订阅、PlannedHandler 诊断、Origin 栈帧提取） |
| `Assets/Scripts/Core/ModuleRunner.cs` | 模块生命周期管理（AddModule 不要求顺序、ValidateGraph Kahn 检测、WhenAny 持续扫描 + MaxConcurrency 限流、失败时反向 Shutdown、自动扫描 Handler 注册到 EventBus） |
| `Assets/Scripts/Core/GameApp.cs` | MonoBehaviour 启动入口（非静态字段，Domain Reload 兼容） |

**Utils/ (2 个文件):**
| 文件 | 说明 |
|---|---|
| `Assets/Scripts/Utils/StateMachine.cs` | 通用泛型状态机（RegisterState/ChangeState/Update） |
| `Assets/Scripts/Utils/CompositeDisposable.cs` | 聚合 IDisposable（幂等 Dispose，Add after Dispose 抛异常） |

**Templates/ (2 个文件，.cs.txt 不编译):**
| 文件 | 说明 |
|---|---|
| `Assets/Scripts/Templates/ModuleTemplate.cs.txt` | IGameModule 完整实现示例 |
| `Assets/Scripts/Templates/EventTemplate.cs.txt` | 事件类型定义模板 |

### 2.3 Claude Code 配置

| 文件 | 说明 |
|---|---|
| `.claude/CLAUDE.md` | AI 指令（八荣八耻 + 框架概念 + 指向 SKILLS_INDEX.md） |
| `.claude/settings.json` | API 配置 + UserPromptSubmit Hook |
| `.claude/conventions.md` | 编码规范（Category/Dependencies/EventHandler/命名/日志/异步） |
| `.claude/agents/` | 4 个 Agent（code-reviewer, bug-tracer, datatable-helper, ui-scaffold） |
| `.claude/skills/` | 7 个通用 Skill（ai-art, deep-research, dev-tools, document-tools, gitnexus, openspec, unity-dev） |
| `.claude/skills/SKILLS_INDEX.md` | 所有 SKILL 的总索引 |

### 2.4 OpenSpec 变更

```
openspec/changes/implement-framework-core/
├── proposal.md   ← 范围和设计依据
├── design.md     ← 每个文件的详细实现规格
├── tasks.md      ← 6 Phase / 11 Task（全部已完成）
└── specs/framework-core/spec.md  ← GIVEN/WHEN/THEN 需求规格
```

---

## 三、关键设计决策（必须遵守）

| 决策 | 内容 |
|---|---|
| **无 DI 容器** | 依赖通过 `Type[] Dependencies` 显式声明 |
| **接口不作为依赖** | Dependencies 只接受具体 Module 类型 |
| **事件继承不支持** | 精确类型匹配（`typeof(T)` 作 key） |
| **InitAsync 异常终止** | 不允许跳过失败模块 |
| **ModuleRunner 非静态** | 挂在 GameApp MonoBehaviour 上，Domain Reload 兼容 |
| **`[EventHandler]` vs `[RequestHandler]` 分离** | 广播和请求响应分开标记 |
| **广播/请求响应分开存储** | EventBus 内部 `_eventSubs` / `_requestSubs` 两张表 |
| **模板 .cs.txt** | 防止 Unity 编译不完整的示例代码 |
| **Category 显式声明** | 移除接口默认实现，强制模块显式声明 Category 和 Dependencies |
| **Category × 100 - OutDegree** | 调度优先级公式 |

---

## 四、目录结构速览

```
AI友好型项目开发/
├── CONTEXT.md                     ← 本文件
├── README.md                      ← 项目总览
├── Assets/Scripts/
│   ├── Core/       (7 文件)      ← ✅ 已实现
│   ├── Modules/    (10 空目录)   ← 🔲 待实现
│   ├── Utils/      (2 文件)      ← ✅ 已实现
│   ├── Templates/  (2 文件)      ← ✅ 已实现
│   ├── ExternalAPI/              ← 🔲 待设计
│   └── Events/                   ← 🔲 待填充
├── OutPackages/                   ← ✅ UniTask 本地缓存
├── .claude/                       ← ✅ 完整配置
├── AI友好型项目探讨/ (5 文档)      ← ✅ 设计完成
└── 项目知识库（AI自行维护）/       ← ✅ 初始结构
```

---

## 五、待完成

| 优先级 | 内容 |
|---|---|
| 🔴 高 | 实现 Modules/ 下的具体游戏模块 |
| 🟡 中 | ExternalAPI 设计（MCP 调用接口） |
| 🟡 中 | 编写 Events/ 下的跨模块事件 |
| 🟡 中 | 编写 Templates/ 更多参考模板 |
| 🟢 低 | 创建 Main.unity 场景 |
| 🟢 低 | 设计工作流 MCP |

---

## 六、AI 重新进入时的推荐步骤

1. 阅读本文件了解项目状态
2. 阅读 `.claude/CLAUDE.md` 了解行为准则
3. 阅读 `.claude/skills/SKILLS_INDEX.md` 了解可用 SKILL
4. 阅读 `.claude/conventions.md` 了解编码规范
5. 如需深入了解某系统，查阅 `AI友好型项目探讨/` 下的设计文档

---

*最后更新：2026-06-10*