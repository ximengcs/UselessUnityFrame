
using QFSW.QC;
using UnityEngine;

namespace Game.Commands
{
    public class TestCmd : MonoBehaviour
    {
        [Command("t1", MonoTargetType.Singleton)]
        public void PrintDebug()
        {
            Debug.Log("test");
        }

        [Command("t2", MonoTargetType.Singleton)]
        public void PrintWarn()
        {
            Debug.LogWarning("test");
        }

        [Command("t3", MonoTargetType.Singleton)]
        public void PrintError()
        {
            Debug.LogError("test");
        }
    }
}
