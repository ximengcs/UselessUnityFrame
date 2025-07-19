
using System.Collections.Generic;

namespace TestGame
{
    public class SceneView : EntityView
    {
        private Dictionary<long, EntityView> _entities;

        public SceneView(WorldView world, long id) : base(world, id)
        {
            Scene = this;
            _entities = new Dictionary<long, EntityView>();
        }

        public EntityView FindEntity(long id)
        {
            if (id == Id) return this;

            if (_entities.TryGetValue(id, out var entity))
                return entity;
            return null;
        }

        internal void OnAddEntity(EntityView child)
        {
            _entities.Add(child.Id, child);
        }

        internal void OnRemoveEntity(long id)
        {
            _entities.Remove(id);
        }
    }
}
