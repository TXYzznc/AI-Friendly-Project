# 任务清单 - 01.配置表系统

> 设计依据：[design.md](./design.md)

## 总体进度
- [x] 任务0：添加 `com.unity.nuget.newtonsoft-json` 包（用户手动完成）
- [x] 任务1：框架机制代码（IDataTable / DataTableFile / DataTableModule）
- [x] 任务2：DataTableGenerator（Editor 生成器）
- [x] 任务3：示例表 ItemTable.json
- [x] 任务4：运行生成器，产出 ItemTable.cs + DataTableRegistry.cs
- [x] 任务5：GameApp 注册 DataTableModule，验证查询
- [x] 任务6：更新/清理旧的 DataTable README 占位文档

---

## 任务详情

### 任务0：添加 Newtonsoft.Json 包
- 状态：✅ 完成
- 说明：用户已通过 Package Manager 添加 `com.unity.nuget.newtonsoft-json`

### 任务1：框架机制代码
- 状态：✅ 完成
- 代码位置：`Assets/Scripts/Modules/DataTable/`
  - `IDataTable.cs`
  - `DataTableFile.cs`
  - `DataTableModule.cs`

### 任务2：DataTableGenerator
- 状态：✅ 完成
- 代码位置：`Assets/Scripts/Modules/DataTable/Editor/DataTableGenerator.cs`
- 要点：扫描 JSON → 校验 schema → 生成 Row/Table 类 + Registry

### 任务3：示例表 ItemTable.json
- 状态：✅ 完成
- 代码位置：`Assets/Resources/DataTable/ItemTable.json`
- 字段：Id(int) / Name(string) / IconId(int) / Tags(int[])，3 行示例数据

### 任务4：运行生成器
- 状态：✅ 完成
- 产出：`Assets/Scripts/DataTable/ItemTable.cs` + `Assets/Scripts/DataTable/DataTableRegistry.cs`

### 任务5：GameApp 接入与验证
- 状态：✅ 完成
- 代码位置：`Assets/Scripts/Core/GameApp.cs`
- 验证结果：Play 模式下 `GetTable<ItemTable>().GetById(1001)` 正确返回 `Name=治疗药水 IconId=2001`，验证代码已移除
- 附带修复：`ModuleRunner.cs` 中 `UniTask.WhenAny` 后访问 `.Status` 导致 "Token version is not matched" 的预存 bug（首次注册真实模块才触发），通过 `.Preserve()` 修复

### 任务6：文档清理
- 状态：✅ 完成
- 涉及文件：`Assets/Resources/DataTable/README.md`（已更新为新 JSON 流程说明；`Assets/DataTable/README.md` 不存在，无需处理）
