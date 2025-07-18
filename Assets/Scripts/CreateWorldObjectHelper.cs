
using UselessFrame.NewRuntime.Entities;
using UselessFrame.NewRuntime.Worlds;

namespace TestGame
{
    public class CreateWorldObjectHelper : IEntityHelper
    {
        private World _world;

        public void Bind(World world)
        {
            _world = world;
        }

        public void OnCreateComponent(Component component)
        {

        }

        public void OnCreateEntity(Entity entity)
        {

        }

        public void OnDestroyComponent(Component component)
        {

        }

        public void OnDestroyEntity(Entity entity)
        {

        }

        public void OnUpdateComponent(Component component)
        {

        }
    }
}
