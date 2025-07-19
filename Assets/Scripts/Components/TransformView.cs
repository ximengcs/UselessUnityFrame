
using UnityEngine;
using UselessFrame.NewRuntime.Entities;

namespace TestGame
{
    [ComponentView(typeof(TransformComponent))]
    public class TransformView : EntityComponent
    {
        private EntityView _entityView;

        public Transform Tf => _entityView.GameObject.transform;

        protected override void OnInit()
        {
            base.OnInit();
            _entityView = Entity.GetComponent<EntityView>();
        }
    }
}
