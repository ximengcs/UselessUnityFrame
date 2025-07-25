using System;
using UnityEngine;
using UnityXFrame.Core.Diagnotics;
using UselessFrame.NewRuntime;
using UselessFrame.Runtime;
using UselessFrameUnity.Attributes;

namespace UselessFrameUnity
{
    public class Entrance : MonoBehaviour
    {
        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this);
        }

        private async void Start()
        {
            InitApplicationSetting();
            XSetting setting = new XSetting();
            setting.Loggers = new[] { typeof(UnityLogger) };
            setting.ModuleAttributes = new[] 
            { 
                typeof(FrameModuleAttribute),
                typeof(CustomModuleAttribute),
            };
            await X.Initialize(setting);
            X.Module.AddHandler<UpdateHandler>();
        }

        private void InitApplicationSetting()
        {
            int refreshRate = Screen.currentResolution.refreshRate;
            Application.targetFrameRate = refreshRate;
            QualitySettings.vSyncCount = 1;
        }

        private void Update()
        {
            X.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            X.Shutdown();
        }
    }
}