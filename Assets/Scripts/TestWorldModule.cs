
using UselessFrame.NewRuntime;
using UselessFrame.NewRuntime.Worlds;
using UselessFrame.Runtime;
using UselessFrameUnity.Attributes;

namespace TestGame
{
    [CustomModule]
    public class TestWorldModule : ModuleBase
    {
        private World _world;

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            X.World.SetHelper(new ClientWorldHelper());
            _world = X.World.Create();
            _world.SetHelper(new CreateWorldObjectHelper());
        }
    }
}
