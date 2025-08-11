using UnityEngine;
using UselessFrame.NewRuntime;

namespace UselessFrame.UIElements
{
    public interface IUI : IContainer<IUI>
    {
        RectTransform RootRect { get; }

        int Layer { get; set; }

        IUIHandle Open();

        IUIHandle Close(bool destroy = true);

        IUIGroup Group { get; }
    }
}
