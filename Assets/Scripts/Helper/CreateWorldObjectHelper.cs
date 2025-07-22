
using System;
using System.Collections.Generic;
using UnityEngine;
using UselessFrame.NewRuntime;
using UselessFrame.NewRuntime.ECS;
using UselessFrame.Runtime.Types;

namespace TestGame
{
    public class CreateWorldObjectHelper : IEntityHelper
    {
        private World _world;
        private WorldView _view;
        private Dictionary<Type, Type> _compViewMaps;
        private Dictionary<Type, Type> _entityViewMaps;

        public void Bind(World world)
        {
            _compViewMaps = new Dictionary<Type, Type>();
            ITypeCollection types = X.Type.GetCollection(typeof(ComponentOfAttribute));
            foreach (var type in types)
            {
                var attr = (ComponentOfAttribute)X.Type.GetAttribute(type, typeof(ComponentOfAttribute));
                _compViewMaps[attr.Type] = type;
                Debug.LogWarning($"OnCreateComponent add {attr.Type.Name} {type.Name}");
            }

            _entityViewMaps = new Dictionary<Type, Type>();
            types = X.Type.GetCollection(typeof(EntityOfAttribute));
            foreach (var type in types)
            {
                var attr = (EntityOfAttribute)X.Type.GetAttribute(type, typeof(EntityOfAttribute));
                _entityViewMaps[attr.Type] = type;
                Debug.LogWarning($"OnCreateEntity add {attr.Type.Name} {type.Name}");
            }

            _world = world;
            _view = new WorldView();
            _world.AttachComponent(_view);
        }

        public void OnCreateEntity(Entity entity)
        {
            X.SystemLog.Debug($"OnCreateEntity {entity.GetType().Name} {entity.Scene == null} {entity.Id}");
            if (_entityViewMaps.TryGetValue(entity.GetType(), out Type viewType))
            {
                entity.GetOrAddComponent(viewType);
            }
        }

        public void OnCreateComponent(EntityComponent component)
        {
            if (component.Entity.IsDisposed) return;
            X.SystemLog.Debug($"OnCreateComponent {component.Entity.Id} {component.GetType().Name}");
            if (_compViewMaps.TryGetValue(component.GetType(), out Type viewType))
            {
                component.Entity.GetOrAddComponent(viewType);
            }
        }

        public void OnUpdateComponent(EntityComponent component)
        {

        }

        public void OnDestroyComponent(EntityComponent component)
        {
            if (component.Entity.IsDisposed) return;
            X.SystemLog.Debug($"OnDestroyComponent {component.GetHashCode()} {component.Entity.Id} {component.GetType().Name}");
            if (_compViewMaps.TryGetValue(component.GetType(), out Type viewType))
            {
                component.Entity.RemoveComponent(viewType);
            }
        }

        public void OnDestroyEntity(Entity entity)
        {
            X.SystemLog.Debug($"OnDestroyEntity {entity.GetType().Name} {entity.Id}");
        }
    }
}
