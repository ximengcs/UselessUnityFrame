
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UselessFrame.NewRuntime;
using UselessFrame.Runtime.Observable;

namespace UselessFrame.UIElements
{
    internal class UIHandle : IUIHandle
    {
        private IUI _ui;
        private Type _uiType;
        private object _userData;
        private UIAttribute _attr;
        private UIGroup _group;
        private Subject<UIHandle, UIState> _state;
        private CancellationTokenSource _disposeTokenSource;

        IReadonlySubject<UIState> IUIHandle.State => _state;

        public IUI UI => _ui;

        public ISubject<UIHandle, UIState> State => _state;

        public UIHandle(Type type, object userData)
        {
            _uiType = type;
            _userData = userData;
            _disposeTokenSource = new CancellationTokenSource();
            _attr = (UIAttribute)X.Type.GetAttribute(_uiType, typeof(UIAttribute));
            _state = new Subject<UIHandle, UIState>(this, UIState.Ready);
        }

        public bool InGroup(string groupName)
        {
            if (_group == null)
                return false;
            return _group.Name == groupName;
        }

        public void SetGroup(UIGroup group)
        {
            if (_group == group) return;
            _group = group;
            switch (_state.Value)
            {
                case UIState.Ready:
                case UIState.Loading:
                case UIState.Initing:
                    _group = group;
                    break;

                case UIState.Open:
                case UIState.OpenBegin:
                case UIState.OpenEnd:
                    if (_ui is IUIGroupElement groupElement)
                    {
                        _group.AddUI(groupElement);
                    }
                    break;
            }
        }

        public async void Start()
        {
            if (_state.Value == UIState.Ready)
            {
                _state.Value = UIState.Loading;
                _ui = (IUI)await X.Pool.RequireAsync(_uiType, _attr.ResSource);
                if (_disposeTokenSource.IsCancellationRequested)
                    return;
            }

            if (_state.Value == UIState.Loading)
            {
                _state.Value = UIState.Initing;
                if (_ui is IUIGroupElement groupElement)
                {
                    _group.AddUI(groupElement);
                    groupElement.OnInit(_userData);
                }
            }

            if (_state.Value == UIState.Initing)
            {
                _ui.OpenAsync();
            }
        }

        public async UniTask<IUI> WaitUI()
        {
            switch (_state.Value)
            {
                case UIState.Ready:
                    {
                        TaskInfo info = new TaskInfo();
                        await info.Start(_state, UIState.Loading);
                        break;
                    }
            }

            return _ui;
        }

        public UniTask WaitStateTask(UIState state)
        {
            TaskInfo info = new TaskInfo();
            return info.Start(_state, state);
        }

        private struct TaskInfo
        {
            private UniTaskCompletionSource _source;
            private IReadonlySubject<UIState> _subject;
            private UIState _state;

            public async UniTask Start(IReadonlySubject<UIState> subject, UIState state)
            {
                _state = state;
                _subject = subject;
                _source = new UniTaskCompletionSource();
                _subject.Subscribe(StateChangeHandler);
                await _source.Task;
                _subject.Unsubscribe(StateChangeHandler);
            }

            private void StateChangeHandler(UIState state)
            {
                if (_state == state)
                    _source.TrySetResult();
            }
        }
    }
}
