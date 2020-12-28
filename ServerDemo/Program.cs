using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Caching;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 13000;
            string IpAddress = "127.0.0.1";
            Socket ServerListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAddress), port);
            ServerListener.Bind(ep);
            ServerListener.Listen(100);
            Console.WriteLine("Server is listening");
            Socket ClientSocket = default(Socket);
            Program p = new Program();

            while (true)
            {
                ClientSocket = ServerListener.Accept();
                Console.WriteLine("Clients connected");
                Thread UserThread = new Thread(new ThreadStart(() => p.User(ClientSocket)));
                UserThread.Start();
            }
            
        }

        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        private ObjectCache cache = null;
        void Chaching(List<FileInfo> fileInfo)
        {
            cache = MemoryCache.Default;
            //CacheItemPolicy policy = new CacheItemPolicy();

            List<FileInfo> fileContents;
            fileContents = fileInfo;
            cache["filecontents"] = fileContents;
            //cache.Set("filecontents", fileContents, policy);
        }



        private string[] previousDirectory = null;

        bool isTheSame(string[] currentDirectory)
        {
            if (previousDirectory == null || currentDirectory == null)
                return false;
            else 
                return currentDirectory[0] == previousDirectory[0] && currentDirectory[1] == previousDirectory[1];
        }


        public void User(Socket client)
        {
            
            while (true)
            {   
                byte[] msg = new byte[9999];
                int size = client.Receive(msg);


                //Console.WriteLine(System.Text.Encoding.ASCII.GetString(msg, 0, size));

                string dataFromClient = System.Text.Encoding.ASCII.GetString(msg, 0, size);
                string[] pathExtension = dataFromClient.Split(' ');

                if (isTheSame(pathExtension))//Timer
                {
                    using (FileStream fs = File.Open(@"C:\Users\Roman\Desktop\151.txt", FileMode.Open, FileAccess.Write, FileShare.None))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("Cache works.");
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }

                    List<FileInfo> fileInfo = cache["filecontents"] as List<FileInfo>;
                    byte[] msgForClient = ObjectToByteArray(fileInfo);

                    client.Send(msgForClient, 0, msgForClient.Length, SocketFlags.None);
                }
                else
                {
                    previousDirectory = pathExtension;
                    List<FileInfo> fileInfo = new List<FileInfo>();
                    foreach (string item in Directory.GetFiles(pathExtension[0]))
                    {
                        var rightExtension = System.IO.Path.GetExtension(item);
                        if (rightExtension == pathExtension[1])
                        {
                            fileInfo.Add(new FileInfo(item));
                        }
                    }


                    byte[] msgForClient = ObjectToByteArray(fileInfo);

                    client.Send(msgForClient, 0, msgForClient.Length, SocketFlags.None);

                    Chaching(fileInfo);

                    using (FileStream fs = File.Open(@"C:\Users\Roman\Desktop\151.txt", FileMode.Open, FileAccess.Write, FileShare.None))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("none");
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                }
            }
        }
    }
}
