using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace WebProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 8000;
            TcpListener tcpListener = new TcpListener(port);
            tcpListener.Start();
            Console.WriteLine("侦听端口号: " + port.ToString());
            // 侦听端口
            tcpListener.Start();
            while (true)
            {
                // 并获取传送和接收数据的Scoket实例
                Socket socket = tcpListener.AcceptSocket();
                Console.WriteLine("侦听到连接");
                // Proxy类实例
                Proxy proxy = new Proxy(socket);
                // 创建线程
                Thread thread = new Thread(new ThreadStart(proxy.Run));
                // 启动线程
                thread.Start();
            }
        }
    }
}
