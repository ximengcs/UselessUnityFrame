using System;
using System.Collections.Generic;
using UselessFrame.NewRuntime;

namespace UselessFrame.UIElements
{
    public class UIModule
    {
        private Dictionary<string, UIGroup> _groups;
        private Dictionary<Type, Dictionary<long, UIHandle>> _handles;

        public UIModule()
        {
            _groups = new Dictionary<string, UIGroup>();
            _handles = new Dictionary<Type, Dictionary<long, UIHandle>>();
        }

        public UIHandle OpenUIAsync<T>(string groupName, long id, object userData = null) where T : IUI
        {
            Type uiType = typeof(T);
            if (!_handles.TryGetValue(uiType, out Dictionary<long, UIHandle> handlesMap))
            {
                handlesMap = new Dictionary<long, UIHandle>();
                _handles.Add(uiType, handlesMap);
            }

            if (!handlesMap.TryGetValue(id, out UIHandle uiHandle))
            {
                uiHandle = new UIHandle(uiType, userData);
                handlesMap.Add(id, uiHandle);
                uiHandle.Start();
            }

            if (!uiHandle.InGroup(groupName))
            {
                UIGroup group = InnerGetOrNewGroup(groupName);
                uiHandle.SetGroup(group);
            }

            return uiHandle;
        }

        private UIGroup InnerGetOrNewGroup(string groupName)
        {
            if (_groups.TryGetValue(groupName, out UIGroup group))
            {
                return group;
            }

            group = new UIGroup(groupName);
            _groups[groupName] = group;
            return group;
        }
    }
}
