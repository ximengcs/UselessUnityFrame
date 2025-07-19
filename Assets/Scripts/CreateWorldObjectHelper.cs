
using System;
using System.Collections.Generic;
using UselessFrame.NewRuntime;
using UselessFrame.NewRuntime.Entities;
using UselessFrame.NewRuntime.Scenes;
using UselessFrame.NewRuntime.Worlds;
using UselessFrame.Runtime.Types;

namespace TestGame
{
    public class CreateWorldObjectHelper : IEntityHelper
    {
        private World _world;
        private WorldView _view;
        private Dictionary<Type, Type> _viewMaps;

        public void Bind(World world)
        {
            _viewMaps = new Dictionary<Type, Type>();
            ITypeCollection types = X.Type.GetCollection(typeof(ComponentViewAttribute));
            foreach (var type in types)
            {
                var attr = (ComponentViewAttribute)X.Type.GetAttribute(type, typeof(ComponentViewAttribute));
                _viewMaps[attr.Type] = type;
            }

            _world = world;
            _view = new WorldView();
            _world.AttachComponent(_view);
        }

        public void OnCreateComponent(EntityComponent component)
        {
            if (_viewMaps.TryGetValue(component.GetType(), out Type viewType))
            {
                EntityComponent comp = (EntityComponent)X.Type.CreateInstance(viewType);
                component.Entity.AttachComponent(comp);
            }
        }

        public void OnCreateEntity(Entity entity)
        {
            X.SystemLog.Debug($"OnCreateEntity {entity.GetType().Name} {entity.Id}");
            if (entity is Scene)
            {
                SceneView scene = _view.AddScene(entity.Id);
                entity.AttachComponent(scene);
            }
            else
            {
                SceneView scene = _view.GetScene(entity.Scene.Id);
                EntityView parent = scene.FindEntity(entity.Parent.Id);
                EntityView newEntityView = parent.AddEntity(entity.Id);
                entity.AttachComponent(newEntityView);
            }
        }

        public void OnDestroyComponent(EntityComponent component)
        {

        }

        public void OnDestroyEntity(Entity entity)
        {
            X.SystemLog.Debug($"OnDestroyEntity {entity.GetType().Name} {entity.Id}");
            if (entity is Scene)
            {
                _view.RemoveScene(entity.Id);
            }
            else
            {
                SceneView scene = _view.GetScene(entity.Scene.Id);
                EntityView parent = scene.FindEntity(entity.Parent.Id);
                parent.RemoveEntity(entity.Id);
            }
        }

        public void OnUpdateComponent(EntityComponent component)
        {

        }
    }
}
