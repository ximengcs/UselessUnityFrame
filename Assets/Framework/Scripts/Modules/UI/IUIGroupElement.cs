
namespace UselessFrame.UIElements
{
    internal interface IUIGroupElement : IUI
    {
        UIHandle Handle { get; }

        void OnInit(object userData);

        void OnOpen();

        void OnGroupChange();

        void SetGroup(IUIGroup group);
    }
}
