
using UnityEngine;
using UselessFrame.NewRuntime.Entities;

namespace TestGame
{
    internal class TransformUpdateSystem : IUpdateSystem<TransformComponent>
    {
        public void OnUpdate(TransformComponent oldComp, TransformComponent newComp)
        {
            Debug.LogWarning($"TransformUpdateSystem OnUpdate {newComp.Entity.Id} {newComp.Position}");
            Transform tf = newComp.Entity.GetComponent<TransformView>().Tf;
            tf.localPosition = new Vector3(newComp.Position.x, newComp.Position.y, newComp.Position.z);
        }
    }
}
