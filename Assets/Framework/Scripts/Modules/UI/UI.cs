
using UnityEngine;
using UselessFrame.NewRuntime;
using UselessFrame.Runtime.Collections;
using UselessFrame.Runtime.Pools;

namespace UselessFrame.UIElements
{
    public class UI : IUI, IPoolUI, IUIGroupElement, IUIGameObjectBinder
    {
        private IContainer<IUI> _container;
        private UIHandle _handle;
        private UIGroup _uiGroup;
        private int _layer;

        protected GameObject _gameObject;
        protected RectTransform _transform;

        #region IUI Interface
        public RectTransform RootRect => _transform;

        public int Layer
        {
            get => _layer;
            set => _uiGroup.SetUILayer(this, value);
        }

        public IUIGroup Group => _uiGroup;

        public IUIHandle Open()
        {
            _uiGroup.OpenUI(this);
            return _handle;
        }

        public IUIHandle Close()
        {
            _uiGroup.CloseUI(this);
            return _handle;
        }
        #endregion

        #region Container Interface
        public IUI Owner => _container.Owner;

        public long Id => _container.Id;

        public IDataProvider Data => _container.Data;

        public IContainer<IUI> Root => _container.Root;

        public IContainer<IUI> Parent => _container.Parent;
        #endregion

        #region IUIGameObjectBinder
        void IUIGameObjectBinder.BindGameObject(GameObject gameObject)
        {
            IUINode node = this;
            _gameObject = gameObject;
            _gameObject.name = node.Name;
            _transform = gameObject.GetComponent<RectTransform>();
        }
        #endregion

        #region IUIGroupElement Function
        UIHandle IUIGroupElement.Handle => _handle;

        string IUINode.Name => $"{GetType().Name}{GetHashCode()}";

        void IUIGroupElement.OnSetLayer(int layer)
        {
            _layer = layer;
        }

        void IUIGroupElement.OnBindHandle(UIHandle handle)
        {
            _handle = handle;
        }

        void IUIGroupElement.OnInit(object userData)
        {
            _container = Container<IUI>.Create(this);
            OnInit(userData);
        }

        void IUIGroupElement.OnOpen()
        {
            OnOpen();
        }

        void IUIGroupElement.OnClose()
        {
            OnClose();
        }

        void IUIGroupElement.OnUpdate()
        {
            OnUpdate();
        }

        void IUIGroupElement.OnGroupChange()
        {
            OnGroupChange();
        }

        void IUIGroupElement.OnSetGroup(IUIGroup group)
        {
            _uiGroup = (UIGroup)group;
            _transform.SetParent(_uiGroup.Root);
        }
        #endregion

        #region IPoolUI
        int IPoolObject.PoolKey { get; }

        IPool IPoolObject.InPool { get; set; }

        void IPoolObject.OnCreate()
        {
        }

        void IPoolObject.OnRequest(object param)
        {
        }

        void IPoolObject.OnRelease()
        {
        }

        void IPoolObject.OnDelete()
        {
        }
        #endregion

        #region Child Function
        protected virtual void OnInit(object userData) { }

        protected virtual void OnOpen() { }

        protected virtual void OnClose() { }

        protected virtual void OnUpdate() { }

        protected virtual void OnGroupChange() { }

        void IContainer<IUI>.Trigger<T>()
        {
            throw new System.NotImplementedException();
        }

        IContainer<IUI> IContainer<IUI>.AddCom()
        {
            throw new System.NotImplementedException();
        }

        T IContainer<IUI>.AddCom<T>()
        {
            throw new System.NotImplementedException();
        }

        void IContainer<IUI>.RemoveCom(IContainer<IUI> child)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
