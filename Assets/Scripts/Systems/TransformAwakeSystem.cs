
using UnityEngine;
using UselessFrame.NewRuntime.Entities;

namespace TestGame
{
    internal class TransformAwakeSystem : IAwakeSystem<TransformComponent>
    {
        public void OnAwake(TransformComponent comp)
        {
            Debug.Log($"TransformAwakeSystem OnAwake");
        }

        public void OnAwake(EntityComponent comp)
        {
            throw new System.NotImplementedException();
        }
    }
}
