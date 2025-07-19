
using System.Collections.Generic;
using UselessFrame.NewRuntime.Entities;

namespace TestGame
{
    public class EntityView : EntityComponent
    {
        private long _id;
        private EntityView _parent;
        private SceneView _scene;
        private Dictionary<long, EntityView> _children;
        private UnityEngine.GameObject _inst;

        public long Id => _id;

        public EntityView Parent => _parent;

        public SceneView Scene
        {
            get => _scene;
            protected set => _scene = value;
        }

        public EntityView(EntityView parent, long id)
        {
            _inst = new UnityEngine.GameObject($"{GetType().Name}-{id}");
            _parent = parent;
            if (_parent != null)
                _inst.transform.SetParent(_parent._inst.transform);
            _id = id;
            _children = new Dictionary<long, EntityView>();
        }

        public void Dispose()
        {
            UnityEngine.GameObject.Destroy(_inst);
        }

        public EntityView AddEntity(long id)
        {
            EntityView entity = new EntityView(this, id);
            _children.Add(id, entity);
            _scene?.OnAddEntity(entity);
            return entity;
        }

        public void RemoveEntity(long id)
        {
            if (_children.TryGetValue(id, out EntityView view))
            {
                _scene?.OnRemoveEntity(id);
                _children.Remove(id);
                view.Dispose();
            }
        }
    }
}
