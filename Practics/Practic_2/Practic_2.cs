using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Practic_2 {
    class Program {
        private static char[] charactersToTest = {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
        };

        private static void func_2(int len, List<String> passes) {
            char[] chars1 = new char[len];

            var totalWordsCount = 1;

            for (var i = 0; i < len; i++) {
                totalWordsCount *= charactersToTest.Length;
            }

            List<string> res = new List<string>();

            for (int i = 0; i < totalWordsCount; i++) {
                int accum = i;
                for (int j = chars1.Length - 1; j >= 0; j--) {
                    chars1[j] = charactersToTest[accum % charactersToTest.Length];
                    accum /= charactersToTest.Length;
                }

                byte[] bytes = Encoding.UTF8.GetBytes(chars1);
                SHA256Managed hashstring = new SHA256Managed();
                byte[] hash = hashstring.ComputeHash(bytes);
                string hashString = string.Empty;
                foreach (byte x in hash) {
                    hashString += String.Format("{0:x2}", x);
                }

                for (int n = 0; n < passes.Count; ++n) {
                    if (passes[n] == hashString) {
                        res.Add(new string(chars1));
                    }
                }
            }

            Console.WriteLine("Найденные пароли:");
            for (int i = 0; i < res.Count; ++i) {
                Console.WriteLine(res[i]);
            }
        }

        private static void func_1(int len, List<String> passes) {
            char[] chars1 = new char[len];
            char[] chars2 = new char[len];
            char[] chars3 = new char[len];
            char[] chars4 = new char[len];
            char[] chars5 = new char[len];
            char[] chars6 = new char[len];
            char[] chars7 = new char[len];
            char[] chars8 = new char[len];
            char[] chars9 = new char[len];

            var totalWordsCount = 1;

            for (var i = 0; i < len - 1; i++) {
                totalWordsCount *= charactersToTest.Length;
            }

            List<string> res = new List<string>();

            Task task1 = new Task(() => {
                for (int k = 0; k < 3; ++k) {
                    chars1[0] = charactersToTest[k];
                    for (int i = 0; i < totalWordsCount; i++) {
                        int accum = i;
                        for (int j = chars1.Length - 1; j >= 1; j--) {
                            chars1[j] = charactersToTest[accum % charactersToTest.Length];
                            accum /= charactersToTest.Length;
                        }

                        /*string ss = new string(chars);*/

                        byte[] bytes = Encoding.UTF8.GetBytes(chars1);
                        SHA256Managed hashstring = new SHA256Managed();
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        foreach (byte x in hash) {
                            hashString += String.Format("{0:x2}", x);
                        }

                        for (int n = 0; n < passes.Count; ++n) {
                            if (passes[n] == hashString) {
                                res.Add(new string(chars1));
                            }
                        }
                    }
                }
            });

            Task task2 = new Task(() => {
                for (int k = 3; k < 6; ++k) {
                    chars2[0] = charactersToTest[k];
                    for (int i = 0; i < totalWordsCount; i++) {
                        int accum = i;
                        for (int j = chars2.Length - 1; j >= 1; j--) {
                            chars2[j] = charactersToTest[accum % charactersToTest.Length];
                            accum /= charactersToTest.Length;
                        }

                        /*string ss = new string(chars);*/

                        byte[] bytes = Encoding.UTF8.GetBytes(chars2);
                        SHA256Managed hashstring = new SHA256Managed();
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        foreach (byte x in hash) {
                            hashString += String.Format("{0:x2}", x);
                        }

                        for (int n = 0; n < passes.Count; ++n) {
                            if (passes[n] == hashString) {
                                res.Add(new string(chars1));
                            }
                        }
                    }
                }
            });

            Task task3 = new Task(() => {
                for (int k = 6; k < 9; ++k) {
                    chars3[0] = charactersToTest[k];
                    for (int i = 0; i < totalWordsCount; i++) {
                        int accum = i;
                        for (int j = chars3.Length - 1; j >= 1; j--) {
                            chars3[j] = charactersToTest[accum % charactersToTest.Length];
                            accum /= charactersToTest.Length;
                        }

                        /*string ss = new string(chars);*/

                        byte[] bytes = Encoding.UTF8.GetBytes(chars3);
                        SHA256Managed hashstring = new SHA256Managed();
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        foreach (byte x in hash) {
                            hashString += String.Format("{0:x2}", x);
                        }

                        for (int n = 0; n < passes.Count; ++n) {
                            if (passes[n] == hashString) {
                                res.Add(new string(chars1));
                            }
                        }
                    }
                }
            });

            Task task4 = new Task(() => {
                for (int k = 9; k < 12; ++k) {
                    chars4[0] = charactersToTest[k];
                    for (int i = 0; i < totalWordsCount; i++) {
                        int accum = i;
                        for (int j = chars4.Length - 1; j >= 1; j--) {
                            chars4[j] = charactersToTest[accum % charactersToTest.Length];
                            accum /= charactersToTest.Length;
                        }

                        /*string ss = new string(chars);*/

                        byte[] bytes = Encoding.UTF8.GetBytes(chars4);
                        SHA256Managed hashstring = new SHA256Managed();
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        foreach (byte x in hash) {
                            hashString += String.Format("{0:x2}", x);
                        }

                        for (int n = 0; n < passes.Count; ++n) {
                            if (passes[n] == hashString) {
                                res.Add(new string(chars1));
                            }
                        }
                    }
                }
            });

            Task task5 = new Task(() => {
                for (int k = 12; k < 15; ++k) {
                    chars5[0] = charactersToTest[k];
                    for (int i = 0; i < totalWordsCount; i++) {
                        int accum = i;
                        for (int j = chars5.Length - 1; j >= 1; j--) {
                            chars5[j] = charactersToTest[accum % charactersToTest.Length];
                            accum /= charactersToTest.Length;
                        }

                        /*string ss = new string(chars);*/

                        byte[] bytes = Encoding.UTF8.GetBytes(chars5);
                        SHA256Managed hashstring = new SHA256Managed();
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        foreach (byte x in hash) {
                            hashString += String.Format("{0:x2}", x);
                        }

                        for (int n = 0; n < passes.Count; ++n) {
                            if (passes[n] == hashString) {
                                res.Add(new string(chars1));
                            }
                        }
                    }
                }
            });

            Task task6 = new Task(() => {
                for (int k = 15; k < 18; ++k) {
                    chars6[0] = charactersToTest[k];
                    for (int i = 0; i < totalWordsCount; i++) {
                        int accum = i;
                        for (int j = chars6.Length - 1; j >= 1; j--) {
                            chars6[j] = charactersToTest[accum % charactersToTest.Length];
                            accum /= charactersToTest.Length;
                        }

                        /*string ss = new string(chars);*/

                        byte[] bytes = Encoding.UTF8.GetBytes(chars6);
                        SHA256Managed hashstring = new SHA256Managed();
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        foreach (byte x in hash) {
                            hashString += String.Format("{0:x2}", x);
                        }

                        for (int n = 0; n < passes.Count; ++n) {
                            if (passes[n] == hashString) {
                                res.Add(new string(chars1));
                            }
                        }
                    }
                }
            });

            Task task7 = new Task(() => {
                for (int k = 18; k < 21; ++k) {
                    chars7[0] = charactersToTest[k];
                    for (int i = 0; i < totalWordsCount; i++) {
                        int accum = i;
                        for (int j = chars7.Length - 1; j >= 1; j--) {
                            chars7[j] = charactersToTest[accum % charactersToTest.Length];
                            accum /= charactersToTest.Length;
                        }

                        /*string ss = new string(chars);*/

                        byte[] bytes = Encoding.UTF8.GetBytes(chars7);
                        SHA256Managed hashstring = new SHA256Managed();
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        foreach (byte x in hash) {
                            hashString += String.Format("{0:x2}", x);
                        }

                        for (int n = 0; n < passes.Count; ++n) {
                            if (passes[n] == hashString) {
                                res.Add(new string(chars1));
                            }
                        }
                    }
                }
            });

            Task task8 = new Task(() => {
                for (int k = 21; k < 24; ++k) {
                    chars8[0] = charactersToTest[k];
                    for (int i = 0; i < totalWordsCount; i++) {
                        int accum = i;
                        for (int j = chars8.Length - 1; j >= 1; j--) {
                            chars8[j] = charactersToTest[accum % charactersToTest.Length];
                            accum /= charactersToTest.Length;
                        }

                        /*string ss = new string(chars);*/

                        byte[] bytes = Encoding.UTF8.GetBytes(chars8);
                        SHA256Managed hashstring = new SHA256Managed();
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        foreach (byte x in hash) {
                            hashString += String.Format("{0:x2}", x);
                        }

                        for (int n = 0; n < passes.Count; ++n) {
                            if (passes[n] == hashString) {
                                res.Add(new string(chars1));
                            }
                        }
                    }
                }
            });

            Task task9 = new Task(() => {
                for (int k = 24; k < 26; ++k) {
                    chars9[0] = charactersToTest[k];
                    for (int i = 0; i < totalWordsCount; i++) {
                        int accum = i;
                        for (int j = chars9.Length - 1; j >= 1; j--) {
                            chars9[j] = charactersToTest[accum % charactersToTest.Length];
                            accum /= charactersToTest.Length;
                        }

                        /*string ss = new string(chars);*/

                        byte[] bytes = Encoding.UTF8.GetBytes(chars9);
                        SHA256Managed hashstring = new SHA256Managed();
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        foreach (byte x in hash) {
                            hashString += String.Format("{0:x2}", x);
                        }

                        for (int n = 0; n < passes.Count; ++n) {
                            if (passes[n] == hashString) {
                                res.Add(new string(chars1));
                            }
                        }
                    }
                }
            });

            task1.Start();
            task2.Start(); 
            task3.Start();
            task4.Start();
            task5.Start();
            task6.Start();
            task7.Start();
            task8.Start();
            task9.Start();

            task1.Wait();
            task2.Wait();
            task3.Wait();
            task4.Wait();
            task5.Wait();
            task6.Wait();
            task7.Wait();
            task8.Wait();
            task9.Wait();
            
            Console.WriteLine("Найденные пароли:");
            for (int i = 0; i < res.Count; ++i) {
                Console.WriteLine(res[i]);
            }
        }

        static void Main(string[] args) {
            List<String> passes = new List<string>();
            string filename = "passes.txt";
            using (StreamReader reader = new StreamReader(filename)) {
                string? line;
                while ((line = reader.ReadLine()) != null) {
                    passes.Add(line);
                }
            }
            
            Stopwatch stopWatch_2 = new Stopwatch();
            stopWatch_2.Start();
            func_2(5, passes);
            stopWatch_2.Stop();
            TimeSpan ts_2 = stopWatch_2.Elapsed; 
            Console.WriteLine("RunTime of Non-parallel version : " + (ts_2.Seconds + 60 * ts_2.Minutes) + " seconds");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            func_1(5, passes);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("RunTime of Parallel version: " + (ts.Seconds + 60 * ts.Minutes) + " seconds");
            /*
            Результат паролей для "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad"
            zyzzx
            Результат паролей для "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b"
            apple
            Результат паролей для "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f"
            mmmmm
            
            Непаралельная версия
            RunTime 31 + 33 + 33 + 30 + 30 seconds
            Паралельная версия
            RunTime 10 + 19 + 19 + 12 + 11 seconds
            */
        }
    }
}