# AI 友好型项目

基于自研轻量模块化框架的 Unity 游戏项目。

## 技术栈

- **框架核心**：自研 IGameModule / ModuleRunner / EventBus
- **Unity** + UniTask + DOTween
- **配置表**：Excel → DataTableGenerator → .bytes

## 项目结构

| 目录 | 说明 |
|---|---|
| `Assets/Scripts/Core/` | 框架核心 |
| `Assets/Scripts/Modules/` | 游戏功能模块 |
| `Assets/Scripts/Utils/` | 通用工具类 |
| `Assets/Scripts/Templates/` | AI 参考脚本 |
| `Assets/Scripts/Events/` | 跨模块事件定义 |
| `.claude/` | Claude Code 配置 |
| `AI友好型项目探讨/` | 框架设计文档 |
| `项目知识库（AI自行维护）/` | AI 维护的知识库 |

## 开始

1. 用 Unity 打开本目录
2. 打开 `Assets/Scenes/Main.unity`
3. 进入 Play Mode