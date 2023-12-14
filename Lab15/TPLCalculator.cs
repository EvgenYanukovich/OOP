using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace OOP_lab_15 {
    //Работа с Task
    internal static class TPLCalculator {
        public static void FindNums() {
            const int runs = 5;

            for (int i = 0; i < runs; i++) {
                Console.WriteLine($"iteration: {i + 1}");
                Stopwatch stopwatch = Stopwatch.StartNew();

                FindPrimeNumbers();

                stopwatch.Stop();
                Console.WriteLine($"all time: {stopwatch.Elapsed}\n");
            }
        }

        public static void FindNumsAgain() {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task task = Task.Run(() => FindPrimeNumbers(cancellationToken));

            Thread.Sleep(2000);

            cancellationTokenSource.Cancel();

            try {
                task.Wait();
            } catch (AggregateException ex) {
                foreach (Exception innerException in ex.InnerExceptions) {
                    if (innerException is TaskCanceledException) {
                        Console.WriteLine("Task was denied.");
                    } else {
                        Console.WriteLine(innerException.Message);
                    }
                }
            }
        }

        private static void FindPrimeNumbers() {
            int[] numbers = new int[99_999_999];

            for (int i = 0; i < numbers.Length; i++) {
                numbers[i] = i + 2;
            }

            Task task = Task.Run(() => {
                for (int i = 0; i < numbers.Length; i++) {
                    int currentNumber = numbers[i];

                    if (currentNumber != -1) {
                        for (int j = i + currentNumber; j < numbers.Length; j += currentNumber) {
                            numbers[j] = -1;
                        }
                    }
                }
            });

            Console.WriteLine($"task id: {task.Id}");
            Console.WriteLine($"task status: {task.Status}");

            task.Wait();

            Console.WriteLine("task is done");
        }

        private static void FindPrimeNumbers(CancellationToken cancellationToken) {

            try {
                for (int i = 2; i <= int.MaxValue; i++) {
                    if (cancellationToken.IsCancellationRequested) {
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    if (IsPrimeNumber(i)) {
                        Console.WriteLine($"simple num is : {i}");
                    }
                }
            }
            catch (OperationCanceledException) {
                Console.WriteLine("Closed.");
            }

            Console.WriteLine("Done.");
        }

        private static bool IsPrimeNumber(int number) {
            if (number < 2)
                return false;

            for (int i = 2; i <= Math.Sqrt(number); i++) {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}
