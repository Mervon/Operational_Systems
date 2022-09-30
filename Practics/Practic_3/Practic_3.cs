using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Security.Permissions;
using ThreadState = System.Threading.ThreadState;

namespace Practic_3 {
    class Program { 
        
        static void Main(string[] args) {
            Stack<int> pool = new Stack<int>();

            Console.WriteLine(pool.Count);

            Random rnd = new Random();

            for (int i = 0; i < 200; ++i) {
                int val = rnd.Next(1, 100);
                pool.Push(val);
            }

            Console.WriteLine(pool.Count);

            Mutex mutexObj = new();
            
            bool is_stop = false;
            
            Thread Producer_1 = new(Producer);
            Producer_1.Name = $"Поток {1}";

            Thread Producer_2 = new(Producer);
            Producer_2.Name = $"Поток {2}";
            
            Thread Producer_3 = new(Producer);
            Producer_3.Name = $"Поток {3}";
            
            Thread Consumer_1 = new(Consumer);
            Consumer_1.Name = $"Поток {4}";
            
            Thread Consumer_2 = new(Consumer);
            Consumer_2.Name = $"Поток {5}";

            Producer_1.Start();
            Producer_2.Start();
            Producer_3.Start();
            Consumer_1.Start();
            Consumer_2.Start();
            

            while (!is_stop) {
                Console.WriteLine("Если введёте 'q', то будет произведена остановка программы");
                if (Console.ReadLine() == "q") {
                    is_stop = true;
                }
            }

            void Producer() {
                while (true) {
                    int val_to_push = rnd.Next(1, 100);
                    if (is_stop) {
                        return;
                    }
                    
                    mutexObj.WaitOne();
                    
                    if (pool.Count > 100) {
                        mutexObj.ReleaseMutex();
                        Thread.CurrentThread.Interrupt();
                    }
                    else {
                        pool.Push(val_to_push);
                        mutexObj.ReleaseMutex();
                    }

                    
                }
            }

            void Consumer() {
                while (true) {
                    if (is_stop) {
                        return;
                    }
                    mutexObj.WaitOne();

                    if (pool.Count < 80 && pool.Count != 0) {
                        if (!Producer_1.IsAlive) Producer_1.Start();
                        if (!Producer_2.IsAlive) Producer_2.Start();
                        if (!Producer_3.IsAlive) Producer_3.Start();
                    } else if (pool.Count == 0) {
                        mutexObj.ReleaseMutex();
                        return;
                    }
                    
                    pool.Pop();
                    mutexObj.ReleaseMutex();
                }
            }
        }
    }
}