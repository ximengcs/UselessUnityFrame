
using UnityEngine;
using UselessFrame.NewRuntime.ECS;


namespace TestGame
{
    internal class TransformSystem : IAwakeSystem<TransformView>, IUpdateSystem<TransformComponent>
    {
        public void OnAwake(TransformView comp)
        {
            Debug.Log($"TransformSystem OnAwake");
        }
        
        public void OnUpdate(TransformComponent oldComp, TransformComponent newComp)
        {
            Debug.LogWarning($"TransformSystem OnUpdate {newComp.Entity.Id} {newComp.Position}");
            Transform tf = newComp.Entity.GetComponent<TransformView>().Tf;
            tf.localPosition = new Vector3(newComp.Position.x, newComp.Position.y, newComp.Position.z);
        }
    }
}
