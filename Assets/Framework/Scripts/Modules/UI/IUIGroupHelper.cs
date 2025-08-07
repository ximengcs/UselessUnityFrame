
using System;

namespace UselessFrame.UIElements
{
    public interface IUIGroupHelper
    {
        bool MatchType(Type type);

        void OnBind(IUIGroup group);

        void OnUnbind(IUIGroup group);

        void OnUIOpen(IUI ui, Action openCompleteCbk);

        void OnUIClose(IUI ui, Action closeCompleteCbk);

        void OnUIUpdate(IUI ui, float deltaTime);

        void OnGroupLayerChange();
    }
}
