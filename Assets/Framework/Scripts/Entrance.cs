using System;
using UnityEngine;
using UselessFrame.Runtime;
using UselessFrame.Runtime.Configs;
using UselessFrame.Runtime.Extensions;
using UselessFrame.Runtime.Types;
using UselessFrameUnity.Attributes;

namespace UselessFrameUnity
{
    public class Entrance : MonoBehaviour
    {
        private IFrameCore _core;

        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this);
        }

        private void Start()
        {
            InitApplicationSetting();
            InitFrameCore();
        }

        private void InitFrameCore()
        {
            _core = FrameManager.Create(FrameConfig.Default);
            InitFrameModule();
            InitCustomModule();
            _core.AddHandler(typeof(UpdateHandler));
            _core.Start();
        }

        private void InitFrameModule()
        {
            ITypeCollection collection = _core.TypeSystem.GetOrNewWithAttr(typeof(FrameModuleAttribute));
            foreach (Type type in collection)
                _core.AddModule(type);
        }

        private void InitCustomModule()
        {
            ITypeCollection collection = _core.TypeSystem.GetOrNewWithAttr(typeof(CustomModuleAttribute));
            foreach (Type type in collection)
                _core.AddModule(type);
        }

        private void InitApplicationSetting()
        {
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
}