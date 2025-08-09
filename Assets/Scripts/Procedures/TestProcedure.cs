
using UnityEngine;
using UnityXFrameLib.UIElements;
using UselessFrame.UIElements;
using UselessFrameUnity;
using XFrame.Modules.Procedure;

namespace TestGame
{
    public class TestProcedure : ProcedureBase
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            IUIGroup mainGroup = G.UI.GetOrNewGroup("Main");
            OnlyOneUIGroupHelper groupHelper = mainGroup.AddHelper<OnlyOneUIGroupHelper>();
            groupHelper.SetMatchMode(UIGroupHelperInEffect.MatchUIMode.Exclude);
            groupHelper.SetEffect(new ScaleEffect(Vector2.zero, Vector2.one, 1f), new ScaleEffect(Vector2.one, Vector2.zero, 1f));

            IUIGroup testGroup = G.UI.GetOrNewGroup("Test");
            OnlyOneUIGroupHelper testHelper = testGroup.AddHelper<OnlyOneUIGroupHelper>();
            testHelper.SetMatchMode(UIGroupHelperInEffect.MatchUIMode.Exclude);
            testHelper.SetEffect(new ScaleEffect(Vector2.zero, Vector2.one, 0.3f), new ScaleEffect(Vector2.one, Vector2.zero, 0.2f));

            G.UI.OpenUIAsync<TestUI>("Main", 0);
        }
    }
}
