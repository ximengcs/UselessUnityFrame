using Cysharp.Threading.Tasks;
using TestGame;
using UnityEngine;
using UselessFrame.Runtime;
using UselessFrame.Runtime.Configs;
using UselessFrame.Runtime.Extensions;

public class Framework : MonoBehaviour
{
    private IFrameCore _core;

    private void Start()
    {
        _core = FrameManager.Create(FrameConfig.Default);
        _core.AddHandler(typeof(UpdateHandler));
        _core.AddModule(typeof(TestModule));
        _core.Start();
        int refreshRate = Screen.currentResolution.refreshRate;
        Application.targetFrameRate = refreshRate;
        QualitySettings.vSyncCount = 1;
    }

    private void Update()
    {
        _core.Trigger<IUpdater>(Time.deltaTime);
    }

    private void OnDestroy()
    {
        _core.Destroy();
    }
}
