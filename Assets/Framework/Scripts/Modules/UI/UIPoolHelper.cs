
using System;
using UnityEngine;
using UselessFrame.NewRuntime;
using Cysharp.Threading.Tasks;
using UselessFrame.ResourceManager;
using UselessFrame.Runtime.Pools;

namespace UselessFrame.UIElements
{
    public class UIPoolHelper : IPoolHelper
    {
        int IPoolHelper.CacheCount => 32;

        IPoolObject IPoolHelper.Factory(Type type, int poolKey)
        {
            Debug.Log($"factory  {type.Name}");
            int useResModule = poolKey;
            string uiPath = InnerUIPath(type);
            GameObject prefab = X.Module.Get<ResourceModule>().Load<GameObject>(uiPath);
            if (prefab == null)
                throw new Exception($"UI prefab is null, {uiPath} {type.FullName}");
            GameObject inst = GameObject.Instantiate(prefab);

            IPoolUI ui;
            if (type.BaseType == typeof(UI))
                ui = InnerInstantiateUI(inst, type);
            else
                ui = InnerInstantiateMonoUI(inst, type);
            return ui;
        }

        async UniTask<IPoolObject> IPoolHelper.FactoryAsync(Type type, int poolKey)
        {
            Debug.Log($"factory async {type.Name}");
            int useResModule = poolKey;
            string uiPath = InnerUIPath(type);
            GameObject prefab = await X.Module.Get<ResourceModule>().LoadAsync<GameObject>(uiPath);
            if (prefab == null)
                throw new Exception($"UI prefab is null, {uiPath} {type.FullName}");
            GameObject inst = GameObject.Instantiate(prefab);

            IPoolUI ui;
            if (type.BaseType == typeof(UI))
                ui = InnerInstantiateUI(inst, type);
            else
                ui = InnerInstantiateMonoUI(inst, type);
            return ui;
        }

        private UI InnerInstantiateUI(GameObject inst, Type type)
        {
            UI ui = (UI)Activator.CreateInstance(type);
            if (ui is IUIGameObjectBinder binder)
                binder.BindGameObject(inst);
            return ui;
        }

        private MonoUI InnerInstantiateMonoUI(GameObject inst, Type type)
        {
            MonoUI ui = (MonoUI)inst.GetComponent(type);
            if (ui == null)
                ui = (MonoUI)inst.AddComponent(type);
            return ui;
        }


        private string InnerUIPath(Type type)
        {
            return $"{UIConstant.UI_PATH}/{type.Name}";
        }

        void IPoolHelper.OnObjectCreate(IPoolObject obj)
        {
        }

        void IPoolHelper.OnObjectDestroy(IPoolObject obj)
        {
        }

        void IPoolHelper.OnObjectRelease(IPoolObject obj)
        {
        }

        void IPoolHelper.OnObjectRequest(IPoolObject obj)
        {
        }
    }
}
