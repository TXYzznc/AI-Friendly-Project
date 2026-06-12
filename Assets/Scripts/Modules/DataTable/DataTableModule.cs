using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class DataTableModule : IGameModule
{
    public int ModuleCategory => 0;
    public Type[] Dependencies => Type.EmptyTypes;

    readonly Dictionary<Type, IDataTable> _tables = new();

    public async UniTask InitializeAsync(CancellationToken ct = default)
    {
        foreach (var entry in DataTableRegistry.Entries)
        {
            var asset = await Resources.LoadAsync<TextAsset>($"DataTable/{entry.FileName}")
                .ToUniTask(cancellationToken: ct) as TextAsset;

            if (asset == null)
                throw new InvalidOperationException($"配置表资源未找到: DataTable/{entry.FileName}");

            var table = (IDataTable)Activator.CreateInstance(entry.TableType);
            table.Load(asset.text);
            _tables[entry.TableType] = table;

            FrameworkLogger.Info("DataTableModule",
                $"Action=TableLoaded Table={entry.TableType.Name} File={entry.FileName}");
        }
    }

    public UniTask ShutdownAsync(CancellationToken ct = default)
    {
        _tables.Clear();
        return UniTask.CompletedTask;
    }

    public T GetTable<T>() where T : IDataTable
    {
        if (_tables.TryGetValue(typeof(T), out var table))
            return (T)table;
        throw new KeyNotFoundException($"配置表 {typeof(T).Name} 未加载");
    }
}
