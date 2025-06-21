
using QFSW.QC;
using System.Net;
using UnityEngine;
using UselessFrame.Net;
using UselessFrame.NewRuntime;

namespace Game.Commands
{
    public class TestServerCmd
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            QuantumRegistry.RegisterObject(new TestServerCmd());
        }

        private IConnection _connection;

        [Command("connect", MonoTargetType.Registry)]
        public void Connect(int port)
        {
            IPEndPoint ip = NetUtility.GetLocalIPEndPoint(port);
            //IPEndPoint ip = new IPEndPoint(IPAddress.Parse("8.137.158.164"), port);
            _connection = IServer.Connect(ip, X.MainFiber);
        }
    }
}
