using System;
using System.Collections.Generic;
using UnityEngine;
using UselessFrame.NewRuntime;
using UselessFrame.Runtime;

namespace UselessFrame.UIElements
{
    public class UIModule : ModuleBase, IUIManager
    {
        private Canvas _canvas;
        private GameObject _gameObject;
        private RectTransform _transform;
        private Camera _uiCamera;
        private Dictionary<string, UIGroup> _groups;
        private Dictionary<Type, Dictionary<long, UIHandle>> _handles;

        public Camera Camera => _uiCamera;

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            Initialize((Canvas)param);
        }

        public void Initialize(Canvas canvas)
        {
            _canvas = canvas;
            _uiCamera = _canvas.worldCamera;
            _gameObject = _canvas.gameObject;
            _transform = (RectTransform)_gameObject.transform;
            _groups = new Dictionary<string, UIGroup>();
            _handles = new Dictionary<Type, Dictionary<long, UIHandle>>();
            X.Pool.RegisterCommonHelper<IPoolUI>(new UIPoolHelper());
        }

        public IUIHandle OpenUIAsync<T>(string groupName, long id, object userData = null) where T : IUI
        {
            Type uiType = typeof(T);
            if (!_handles.TryGetValue(uiType, out Dictionary<long, UIHandle> handlesMap))
            {
                handlesMap = new Dictionary<long, UIHandle>();
                _handles.Add(uiType, handlesMap);
            }

            if (!handlesMap.TryGetValue(id, out UIHandle uiHandle))
            {
                uiHandle = new UIHandle(uiType);
                handlesMap.Add(id, uiHandle);
            }

            if (!uiHandle.InGroup(groupName))
            {
                UIGroup group = InnerGetOrNewGroup(groupName);
                uiHandle.SetGroup(group);
            }

            uiHandle.TriggerOpen(userData);

            return uiHandle;
        }

        public IUIHandle CloseUIAsync<T>(long id, bool destroy)
        {
            Type uiType = typeof(T);
            if (!_handles.TryGetValue(uiType, out Dictionary<long, UIHandle> handlesMap))
            {
                handlesMap = new Dictionary<long, UIHandle>();
                _handles.Add(uiType, handlesMap);
            }

            if (handlesMap.TryGetValue(id, out UIHandle handle))
            {
                handle.TriggerClose();
                return handle;
            }

            return handle != null ? handle : EmptyUIHandle.Default;
        }

        public IUIGroup GetOrNewGroup(string groupName)
        {
            return InnerGetOrNewGroup(groupName);
        }

        private UIGroup InnerGetOrNewGroup(string groupName)
        {
            if (_groups.TryGetValue(groupName, out UIGroup group))
            {
                return group;
            }

            GameObject groupRoot = new GameObject(groupName, typeof(RectTransform), typeof(CanvasGroup));
            groupRoot.transform.SetParent(_transform, false);
            group = new UIGroup(this, groupRoot, groupName);
            _groups[groupName] = group;
            return group;
        }

        internal int SetUIGroupLayer(UIGroup group, int layer)
        {
            return SetLayer(_transform, group, layer, InnerGroupLayerChange);
        }

        internal void InnerGroupLayerChange(string nodeName, int index)
        {
            if (_groups.TryGetValue(nodeName, out UIGroup group))
            {
                group.UpdateLayer(index);
            }
        }

        internal static int SetLayer(Transform root, IUINode element, int layer, Action<string, int> onIndexChange)
        {
            if (root.childCount == 0)
                return 0;
            layer = Mathf.Clamp(layer, 0, root.childCount - 1);
            Transform check = root.GetChild(layer);
            if (check.name == element.Name)
                return layer;

            bool find = false;
            int curIndex = 0;
            Transform[] list = new Transform[root.childCount];
            for (int i = 0; i < list.Length; i++, curIndex++)
            {
                Transform child = root.GetChild(i);
                if (!find && child.name == element.Name)
                {
                    find = true;
                    list[layer] = child;
                    if (layer != curIndex)
                        curIndex--;
                }
                else
                {
                    if (layer == curIndex)
                        curIndex++;
                    list[curIndex] = child;
                }
            }

            for (int i = 0; i < list.Length; i++)
            {
                Transform child = list[i];
                if (child.GetSiblingIndex() != i)
                {
                    child.SetSiblingIndex(i);
                    onIndexChange(child.name, i);
                }
            }
            return layer;
        }
    }
}
