
using UnityEngine;
using UselessFrame.NewRuntime;
using UselessFrame.Runtime.Collections;
using UselessFrame.Runtime.Pools;

namespace UselessFrame.UIElements
{
    public class MonoUI : MonoBehaviour, IUI, IPoolUI
    {
        public int PoolKey => throw new System.NotImplementedException();

        RectTransform IUI.RootRect => throw new System.NotImplementedException();

        int IUI.Layer { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        IUIGroup IUI.Group => throw new System.NotImplementedException();

        IUI IContainer<IUI>.Owner => throw new System.NotImplementedException();

        long IContainer<IUI>.Id => throw new System.NotImplementedException();

        IDataProvider IContainer<IUI>.Data => throw new System.NotImplementedException();

        IContainer<IUI> IContainer<IUI>.Root => throw new System.NotImplementedException();

        IContainer<IUI> IContainer<IUI>.Parent => throw new System.NotImplementedException();

        int IPoolObject.PoolKey => throw new System.NotImplementedException();

        IPool IPoolObject.InPool { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        IContainer<IUI> IContainer<IUI>.AddCom()
        {
            throw new System.NotImplementedException();
        }

        T IContainer<IUI>.AddCom<T>()
        {
            throw new System.NotImplementedException();
        }

        IUIHandle IUI.Close()
        {
            throw new System.NotImplementedException();
        }

        void IPoolObject.OnCreate()
        {
            throw new System.NotImplementedException();
        }

        void IPoolObject.OnDelete()
        {
            throw new System.NotImplementedException();
        }

        void IPoolObject.OnRelease()
        {
            throw new System.NotImplementedException();
        }

        void IPoolObject.OnRequest(object userData)
        {
            throw new System.NotImplementedException();
        }

        IUIHandle IUI.Open()
        {
            throw new System.NotImplementedException();
        }

        void IContainer<IUI>.RemoveCom(IContainer<IUI> child)
        {
            throw new System.NotImplementedException();
        }

        void IContainer<IUI>.Trigger<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
