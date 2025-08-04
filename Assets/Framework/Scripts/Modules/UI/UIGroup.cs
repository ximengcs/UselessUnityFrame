using System.Collections.Generic;

namespace UselessFrame.UIElements
{
    internal class UIGroup : IUIGroup
    {
        private string _name;
        private IUIGroupHelper _helper;
        private List<IUIGroupElement> _uiList;

        public string Name => _name;

        public UIGroup(string name)
        {
            _name = name;
            _uiList = new List<IUIGroupElement>();
        }

        public void AddUI(IUIGroupElement ui)
        {
            _uiList.Add(ui);
            UIGroup originGroup = ui.Group as UIGroup;
            if (originGroup != null)
                originGroup._uiList.Remove(ui);
            ui.SetGroup(this);
            ui.OnGroupChange();
        }

        public void OpenUI(IUIGroupElement ui)
        {
            ui.Handle.State.Value = UIState.OpenBegin;
            if (_helper != null)
            {

            }
            else
            {
                ui.Handle.State.Value = UIState.Open;
                ui.OnOpen();
            }
            ui.Handle.State.Value = UIState.OpenEnd;
        }
    }
}
