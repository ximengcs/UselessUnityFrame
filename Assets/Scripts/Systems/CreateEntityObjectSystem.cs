
using UselessFrame.NewRuntime.Entities;

namespace TestGame
{
    public class CreateEntityObjectSystem : IAwakeSystem
    {
        public void OnAwake(Component comp)
        {
            UnityEngine.Debug.Log($"CreateEntityObjectSystem {comp}");
        }
    }
}
