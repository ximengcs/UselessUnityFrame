
using System.Collections.Generic;
using UnityEngine;
using UselessFrame.NewRuntime.Worlds;

namespace TestGame
{
    public class WorldView : EntityView
    {
        private Dictionary<long, SceneView> _scenes;

        public WorldView() : base(null, 0)
        {
            _scenes = new Dictionary<long, SceneView>();
        }

        public SceneView AddScene(long id)
        {
            if (!_scenes.ContainsKey(id))
            {
                SceneView sceneView = new SceneView(this, id);
                _scenes.Add(id, sceneView);
                return sceneView;
            }
            return _scenes[id];
        }

        public void RemoveScene(long id)
        {
            if (_scenes.TryGetValue(id, out SceneView sceneView))
            {
                _scenes.Remove(id);
                sceneView.Dispose();
            }
        }

        public SceneView GetScene(long id)
        {
            if (_scenes.TryGetValue(id, out SceneView sceneView))
                return sceneView;
            return null;
        }
    }
}
