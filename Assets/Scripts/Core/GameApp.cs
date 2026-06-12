using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 框架启动入口。MonoBehaviour，非静态，Domain Reload 兼容。
/// </summary>
public class GameApp : MonoBehaviour
{
    EventBus _bus;
    ModuleRunner _runner;

    async void Start()
    {
        try
        {
            _bus = new EventBus();
            _runner = new ModuleRunner(_bus);

            _runner.AddModule(new DataTableModule());

            // TODO: 注册项目需要的其他通用模块或项目模块
            // _runner.AddModule(new SceneModule(_bus));
            // _runner.AddModule(new FlowModule(_bus));

            await _runner.StartAsync();
            Debug.Log("[GameApp] 所有模块初始化完成，游戏就绪");
        }
        catch (Exception ex)
        {
            FrameworkLogger.Error("GameApp", $"启动失败 Exception={ex.GetType().Name} Msg=\"{ex.Message}\"");
            throw;
        }
    }

    async void OnDestroy()
    {
        try
        {
            if (_runner != null)
                await _runner.StopAsync();
        }
        catch (Exception ex)
        {
            FrameworkLogger.Error("GameApp", $"关闭异常 Exception={ex.GetType().Name} Msg=\"{ex.Message}\"");
        }
    }
}
