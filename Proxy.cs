using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace WebProxy
{
    // 代理类
    class Proxy
    {
        public Socket clientSocket;
        // 存储来自客户端请求数据包
        Byte[] read = new byte[1024];

        // 设定编码
        Byte[] Buffer = null;
        Encoding ASCII = Encoding.ASCII;

        // 存储服务器返回数据
        Byte[] RecvBytes = new Byte[4096];

        public Proxy(Socket socket)
        {
            this.clientSocket = socket;
        }

        public void Run()
        {
            // 存放来自客户端的HTTP请求字符串
            string clientmessage = " ";
            // 存放解析出的地址请求信息
            string URL = " ";
            int bytes = ReadMessage(read,ref clientSocket,ref clientmessage);
            Console.WriteLine(bytes);
            var str = Http.HttpGet("http://www.baidu.com/s", "word=worldhello");
            SendMessage(clientSocket, str);
        }

        // 接收客户端的HTTP请求数据
        private int ReadMessage(byte[] ByteArray, ref Socket s,ref String clientmessage)
        {
            int bytes = s.Receive(ByteArray, 1024, 0);
            string mesaagefromclient = Encoding.ASCII.GetString(ByteArray);
            clientmessage = (String)mesaagefromclient;
            return bytes;
        }

        // 传送web服务器反馈的数据到客户端
        private void SendMessage(Socket s,string message)
        {
            Buffer = new Byte[message.Length + 1];
            int length = ASCII.GetBytes(message, 0, message.Length, Buffer, 0);
            Console.WriteLine("传送字节数: " + length.ToString());
            s.Send(Buffer, length, 0);
        }
    }
}
