using UnityEngine;
using UselessFrame.NewRuntime;
using UselessFrame.Runtime.Observable;
using UselessFrame.Runtime.Pools;

namespace UselessFrame.UIElements
{
    public interface IUI : IContainer, IPoolObject
    {
        RectTransform RootRect { get; }

        int Layer { get; }

        void Open();

        void Close();

        UIHandle OpenAsync();

        UIHandle CloseAsync();

        IUIGroup Group { get; }
    }
}
