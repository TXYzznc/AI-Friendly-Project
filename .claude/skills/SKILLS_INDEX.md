# SKILL 系统索引与使用指南

> 所有可用 SKILL 的索引文件。AI 每次任务开始时应先查看本文档，确认是否有合适的 SKILL 可用。

---

## 一、使用规则

**任务需要使用 SKILL 时的标准流程：**

1. **查看本文档的索引表** — 确认是否有匹配的 SKILL
2. **打开对应 SKILL 的 `SKILL.md`** — 理解该技能的子技能列表和应用场景
3. **根据任务需求选择子技能** — 匹配最合适的子技能
4. **读取对应的 `references/*.md`** — 获取详细操作指令和规范
5. **按照 references 文件的规范执行任务**

**重要原则：** 即使没有 CLAUDE.md 指导，也应该能够通过探索本文件夹和阅读各 SKILL.md 来理解整个 SKILL 系统。大部分 SKILL 的详细指令以 references 文件的形式存在，**必须手动索引**。

---

## 二、SKILL 索引

| SKILL | 路径 | 适用场景 | 子技能数 |
|---|---|---|---|
| **ai-art** | `ai-art/SKILL.md` | AI 绘图提示词生成（图标/立绘/场景/UI） | 6 |
| **deep-research** | `deep-research/SKILL.md` | 联网深度研究，收集整理资料 | 1 |
| **dev-tools** | `dev-tools/SKILL.md` | MCP Server 创建、Skill 创建/改进 | 2 |
| **document-tools** | `document-tools/SKILL.md` | 文档创建/转换（Word/PDF/PPT/Excel） | 6 |
| **gitnexus** | `gitnexus/gitnexus-guide/SKILL.md` | 代码分析（架构/影响范围/Bug追踪/重构） | 6 |
| **openspec** | `openspec/SKILL.md` | 规范化需求管理和变更流程 | — |
| **unity-dev** | `unity-dev/SKILL.md` | Unity 开发（UI/动画/配置表/Editor） | 6 |

---

## 三、快速匹配表

| 任务关键词 | 使用 SKILL |
|---|---|
| 画图、图标、立绘、场景图、提示词 | `ai-art` |
| 搜索资料、深度研究、收集信息 | `deep-research` |
| 创建 MCP、创建 Skill、改进 Skill | `dev-tools` |
| Word、PDF、PPT、Excel、文档转换 | `document-tools` |
| 代码分析、架构、影响范围、Bug追踪、重构 | `gitnexus` |
| 需求管理、变更流程 | `openspec` |
| Unity UI、动画、配置表、预制体、Editor | `unity-dev` |

---

## 四、各 SKILL 子技能速查

### ai-art
| 子技能 | 参考文件 |
|---|---|
| 技能/Buff 图标 | `references/drawing-prompt-ICON.md` |
| 角色立绘 | `references/drawing-prompt-CHARACTER.md` |
| 场景图 | `references/drawing-prompt-SCENE.md` |
| UI 元素 | `references/drawing-prompt-UI.md` |
| 提示词生成器 | `references/drawing-prompt-generator.md` |
| 图片反推提示词 | `references/image-to-prompt-generator.md` |

### dev-tools
| 子技能 | 参考文件 |
|---|---|
| 创建 MCP Server | `references/mcp-builder.md` |
| 创建/改进 Skill | `references/skill-creator.md` |

### document-tools
| 子技能 | 参考文件 |
|---|---|
| Word 文档 | `references/docx.md` |
| PDF 文档 | `references/pdf.md` |
| PowerPoint | `references/pptx.md` |
| Excel 表格 | `references/xlsx.md` |
| 文件格式转换 | `references/markitdown.md` |
| 复制文档格式 | `references/docx-format-replicator.md` |

### gitnexus
| 子技能 | 参考文件 |
|---|---|
| 代码库概览 | `gitnexus-guide/SKILL.md` |
| 架构探索 | `gitnexus-exploring/SKILL.md` |
| 影响范围分析 | `gitnexus-impact-analysis/SKILL.md` |
| Bug 调试 | `gitnexus-debugging/SKILL.md` |
| 重构 | `gitnexus-refactoring/SKILL.md` |
| CLI 命令 | `gitnexus-cli/SKILL.md` |

### unity-dev
| 子技能 | 参考文件 |
|---|---|
| Unity Editor 自动化 | `references/unity-skills.md` |
| UI 全流程开发 | `references/unity-ui-builder.md` |
| 游戏 UI 设计 | `references/game-ui-design.md` |
| 动画与微交互 | `references/animate.md` |
| 配置表设计 | `references/cog-config-table-designer.md` |
| 前端界面设计 | `references/frontend-design.md` |