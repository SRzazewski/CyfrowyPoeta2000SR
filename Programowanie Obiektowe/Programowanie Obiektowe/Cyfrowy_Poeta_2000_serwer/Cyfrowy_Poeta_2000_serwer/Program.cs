using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Cyfrowy_Poeta_2000_serwer
{
    class Program
    {
        static string b = "Nie pracuje";
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Witaj w Cyfrowy Poeta 2000 - Serwer\n\n");
            Console.WriteLine("Naciśnij klawisz, aby kontynuować");
            Console.ReadKey();
            while (true)
            {
                if (b == "Nie pracuje")
                {
                    Console.Clear();
                    Console.WriteLine("Wybierz 1, aby włączyć serwer.");
                    Console.WriteLine("Wybierz 2, aby dodać nowe słowe.");
                    Console.WriteLine("Wybierz 3, aby wylączyć test.");
                    Console.WriteLine("Wybierz 4, aby wyłączyć program.");
                    if (Console.ReadKey().Key == ConsoleKey.D1)
                    {
                        Console.Clear();
                        Program.b = "Pracuje";
                        Console.WriteLine("Naciśnij klawisz, aby kontynuować");
                    }
                    else if (Console.ReadKey().Key == ConsoleKey.D2)
                    {
                        Console.Clear();
                        Console.WriteLine("Dodawanie nowego słowa\n");
                        Console.WriteLine("Naciśnij klawisz, aby kontynuować");
                        Console.ReadKey();
                        Writeslowa();
                    }
                    else if (Console.ReadKey().Key == ConsoleKey.D4)
                    {
                        Console.Clear();
                        Console.WriteLine("Aby zakończyć wczyścnij dowolny klawisz...\n");
                        Console.ReadKey();
                        break;
                    }
                    else if (Console.ReadKey().Key == ConsoleKey.D3)
                    {
                        Console.Clear();
                        Console.WriteLine("Naciśnij klawisz, aby kontynuować");
                        Console.ReadKey();
                        szukaj_rym();
                    }
                }
                else if (b == "Pracuje")
                {
                    if (Console.ReadKey().Key == ConsoleKey.D1)
                    {
                        Console.Clear();
                        Console.WriteLine("Serwer pracuje\n");
                        Console.WriteLine("Wybierz 1, aby wyłączyć serwer.");
                        if (Console.ReadKey().Key == ConsoleKey.D1)
                        {
                            Console.Clear();
                            Console.WriteLine("Serwer wyłączony\n");
                            Program.b = "Nie pracuje";
                            Console.WriteLine("Naciśnij klawisz, aby kontynuować");
                        }
                    }
                }
            }
        }
        static void szukaj_rym()
        {
            Console.Clear();
            Console.WriteLine("Podaj słowo:\n");
            string slowo = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(@"D:\Poeta\Slowa.txt");
            string[] rymy = {""};
            int rym = 0;

            int b = 0;
            int a=0;
            int r=0;
            int x;

            Console.WriteLine(slowo.Length);
            Console.ReadKey();
            for (int i = slowo.Length-1; i >= 0; i--)
            {
                if (slowo[i] == 'a' || slowo[i] == 'e' || slowo[i] == 'y' || slowo[i] == 'u' || slowo[i] == 'i' || slowo[i] == 'o')
                {
                    if (b == 1)
                    {
                        a = i;
                        r = slowo.Length - a - 1;
                        Console.WriteLine(a);
                        Console.WriteLine(r);
                        break;
                    }
                    else if (b == 0)
                    {
                        b = 1;
                    }
                }
            }
            foreach (string line in lines)
            {
                if (line[line.Length-r] == slowo[a+1])
                {

                    Console.WriteLine("asdasdwfac");
                    for (x = line.Length - r; x <= line.Length; x++)
                    {
                        Console.WriteLine("qwerttyac");
                        a++;
                        Console.WriteLine(x);
                        Console.WriteLine(a);
                        if (line[x-1] != slowo[a-1])
                        {
                            break;
                        }
                        else
                        {
                        }

                        if (line.Length == x)
                        {
                            if (rym == 0)
                            {
                                rym = 1;
                                rymy[0] = line;
                            }
                            else if (rym == 1)
                            {
                                Array.Resize(ref rymy, rymy.Length + 1);
                                rymy[rymy.Length] = line;
                            }
                        }
                    }
                }
            }

            foreach (string line1 in rymy)
            {
                Console.WriteLine(line1);
            }
            Console.WriteLine("Naciśnij klawisz, aby kontynuować");
            System.Console.ReadKey();
        }

        static void Writeslowa()
        {
            bool pow = false;

            Console.Clear();
            Console.WriteLine("Podaj słowo: ");
            string slowo = Console.ReadLine();
            string[] slowo1 = {slowo};

            string[] lines = System.IO.File.ReadAllLines(@"D:\Poeta\Slowa.txt");
            
            foreach (string line in lines)
            {
                if (line == slowo)
                {
                    pow = true;
                }
            }

            if (pow == false)
            {
                System.IO.File.AppendAllLines(@"D:\Poeta\Slowa.txt", slowo1);
                Console.WriteLine("Slowo dodano.");
            }
            else
            {
                Console.WriteLine("Slowo było dodane wcześniej!");
            }

            Console.WriteLine("Naciśnij klawisz, aby kontynuować");
            System.Console.ReadKey();
        }
    }

    public class SynchronousSocketListener
    {

        // Incoming data from the client.  
        public static string data = null;

        public static void StartListening()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    // Show the data on the console.  
                    Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static int Main1(String[] args)
        {
            StartListening();
            return 0;
        }
    }
}
