
using System.Collections.Generic;
using UnityEngine;
using UselessFrame.NewRuntime.Worlds;

namespace TestGame
{
    public class WorldView
    {
        private GameObject _inst;
        private Dictionary<long, SceneView> _scenes;

        public WorldView()
        {
            _scenes = new Dictionary<long, SceneView>();
        }
    }
}
