using System.Diagnostics;
using System.Timers;

namespace lab14 {
    internal class Program {
        const string LAB_PATH = @"D:\Education\2 курс\Семестр 3\Объектно-ориентированное программирование\lab14";

        private static readonly object lockObject = new object();
        private static bool isEvenThreadActive = true;

        static void Main(string[] args) {
            WriteProcessesInfo();
            Console.WriteLine();
            WriteCurrentDomainInfo();
            Console.WriteLine();
            StartThreads();

            var timer = new System.Timers.Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += Job;

            timer.Start();
            Console.WriteLine("Enter to stop timer");
            Console.ReadKey();
            timer.Stop();
        }

        static void Job(object sender, ElapsedEventArgs args) {
            Console.WriteLine(DateTime.Now);
        }

        static void WriteSeparator() {
            Console.WriteLine(string.Concat(Enumerable.Range(0, 90).Select(_ => '-')));
        }

        static void WriteProcessesInfo() {
            WriteSeparator();
            Console.WriteLine("Process ID\t|Session|Priority\t|Process Name");
            WriteSeparator();
            foreach (Process process in Process.GetProcesses()) {
                Console.WriteLine($"Process#{process.Id}\t|{process.SessionId}\t|{process.BasePriority}\t\t|{process.ProcessName}");
            }
            WriteSeparator();
        }

        static void WriteCurrentDomainInfo() {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            WriteSeparator();
            Console.WriteLine($"Domain ID: {currentDomain.Id}");
            Console.WriteLine($"Domain Name: {currentDomain.FriendlyName}");
            Console.WriteLine($"Target Framework: {currentDomain.SetupInformation.TargetFrameworkName}");
            Console.WriteLine($"Assemblies:\n\t{string.Join("\n\t", currentDomain.GetAssemblies().Select(assembly => assembly.FullName))}");
            WriteSeparator();
        }

        static void StartThreads() {
            Console.Write("Enter n: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Thread evenThread = new Thread(() => WriteEvenNumbersToFileAndConsole(n));
            evenThread.Name = "EvenThread";
            evenThread.Priority = ThreadPriority.AboveNormal;

            Thread oddThread = new Thread(() => WriteOddNumbersToFileAndConsole(n));
            oddThread.Name = "OddThread";

            Thread primeThread = new Thread(() => WritePrimesToFileAndConsole(n));
            primeThread.Name = "PrimeThread";

            evenThread.Start();
            oddThread.Start();
            primeThread.Start();

            evenThread.Join();
            oddThread.Join();
            primeThread.Join();
        }

        static void WritePrimesToFileAndConsole(int n) {
            using (StreamWriter writer = new StreamWriter(Path.Combine(LAB_PATH, "numbersPrime.txt"))) {
                for (int i = 1; i <= n; i++) {
                    if (isPrime(i)) {
                        writer.WriteLine(i);
                        Console.WriteLine($"prime: {i}");
                    }

                    Thread.Sleep(500);
                }
            }

            bool isPrime(int number) {
                if (number < 2)
                    return false;

                for (int i = 2; i <= Math.Sqrt(number); i++) {
                    if (number % i == 0)
                        return false;
                }

                return true;
            }
        }

        static void WriteEvenNumbersToFileAndConsole(int n) {
            string fileName = Path.Combine(LAB_PATH, "numbersEven.txt");

            using (StreamWriter writer = new StreamWriter(fileName)) {
                for (int i = 2; i <= n; i += 2) {
                    lock (lockObject) {
                        while (!isEvenThreadActive)
                            Monitor.Wait(lockObject);

                        writer.WriteLine(i);
                        Console.WriteLine("even: " + i);

                        Thread.Sleep(500);

                        isEvenThreadActive = false;
                        Monitor.Pulse(lockObject);
                    }
                }
            }
        }

        static void WriteOddNumbersToFileAndConsole(int n) {
            string fileName = Path.Combine(LAB_PATH, "numbersOdd.txt");

            using (StreamWriter writer = new StreamWriter(Path.GetFullPath(fileName))) {
                for (int i = 1; i <= n; i += 2) {
                    lock (lockObject) {
                        while (isEvenThreadActive)
                            Monitor.Wait(lockObject);

                        writer.WriteLine(i);
                        Console.WriteLine("odd: " + i);

                        Thread.Sleep(500);

                        isEvenThreadActive = true;
                        Monitor.Pulse(lockObject);
                    }
                }
            }
        }
    }
}