
using Google.Protobuf;
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

        [Command("connect-server", MonoTargetType.Registry)]
        public void Connect(int port)
        {
            IPEndPoint ip = NetUtility.GetLocalIPEndPoint(port);
            //IPEndPoint ip = new IPEndPoint(IPAddress.Parse("8.137.158.164"), port);
            _connection = IServer.Connect(ip, X.MainFiber);
        }

        [Command("server-test1", MonoTargetType.Registry)]
        public void TestTexture()
        {
            Sprite sprite = Resources.Load<Sprite>("test-res");
            byte[] data = sprite.texture.EncodeToPNG();
            TextureTest test = new TextureTest()
            {
                Data = ByteString.CopyFrom(data)
            };
            _connection.Send(test);
        }

        private IServer _server;
        [Command("create-server", MonoTargetType.Registry)]
        public void CreateServer(int port)
        {
            _server = IServer.Create(port, X.MainFiber);
            _server.NewConnectionEvent += SerOnNewConnection;
            _server.Start();
        }

        private void SerOnNewConnection(IConnection connection)
        {
            connection.ReceiveMessageEvent += ReceiveMessage;
        }

        private void ReceiveMessage(IMessageResult msg)
        {
            Debug.Log($"receive message {msg.MessageType.Name}");
            if (msg.Message is TextureTest texMsg)
            {
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(texMsg.Data.Span.ToArray());
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                GameObject inst = new GameObject();
                var render = inst.AddComponent<SpriteRenderer>();
                render.sprite = sprite;
            }
        }
    }
}
