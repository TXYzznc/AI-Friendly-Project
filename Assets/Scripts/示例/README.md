# Templates — AI 参考脚本模板

> AI 开发时的参考模板。文件后缀 `.cs.txt` 防止 Unity 编译。
> **不要直接修改这些文件**——复制需要的部分到目标位置。

## 文件（2 个）

| 文件 | 说明 |
|---|---|
| `ModuleTemplate.cs.txt` | IGameModule 标准写法（含 Category 选择指南、EventHandler/RequestHandler 示例、CancellationToken 用法） |
| `EventTemplate.cs.txt` | 事件类型定义模板（含命名规范和存放位置规则） |

## 使用方式

1. AI 开发新模块时，阅读 `ModuleTemplate.cs.txt` 理解标准写法
2. 创建新事件时，阅读 `EventTemplate.cs.txt` 理解事件定义规范
3. 复制模板到目标位置，将 `.cs.txt` 改为 `.cs` 开始编写

## 为什么是 .cs.txt

模板代码不完整（缺少 using、依赖未安装），如果以 `.cs` 结尾会被 Unity 编译报错。