namespace OOP_lab_15 {
    //Работа с Parellel
    internal static class TPLTasks{
        public static void DetectText() {
            string[] strings = { "Hello", "world", "Hello", "Parallel", "world" };
            string searchString = "Hello";
            string replaceString = "Hi";

            Console.WriteLine("simple for:");
            for (int i = 0; i < strings.Length; i++) {
                strings[i] = strings[i].Replace(searchString, replaceString);
                Console.WriteLine(strings[i]);
            }

            Console.WriteLine();

            Console.WriteLine("Parallel for:");
            Parallel.For(0, strings.Length, i =>
            {
                strings[i] = strings[i].Replace(searchString, replaceString);
                Console.WriteLine(strings[i]);
            });
        }

        public static void ParallelFewTasks() {
            Parallel.Invoke(() => {
               Console.WriteLine("block A");
           }, () => {
               Console.WriteLine("block B");
           }
       );
        }

        public static void CalculateInFewThreads() {
            Task<int> taskA = Task.Run(() => CalculateA());
            Task<int> taskB = Task.Run(() => CalculateB());
            Task<int> taskC = Task.Run(() => CalculateC());

            Task<int> continuationTask1 = taskA.ContinueWith(previousTask => {
                int a = previousTask.Result;
                int b = taskB.Result;
                int c = taskC.Result;
                return (a + b) * c;
            });

            Task<int> continuationTask2 = Task.Run(async () => {
                int a = await taskA;
                int b = await taskB;
                int c = await taskC;

                return (a + b) * c;
            });


            Console.WriteLine("result 1 (by ContinueWith): " + continuationTask1.Result);
            Console.WriteLine("result 2 (with using of awaiter and GetAwaiter(), GetResult()): " + continuationTask2.Result);
        }

        private static int CalculateA() {
            int a = 2;
            return a;
        }

        private static int CalculateB() {
            int b = 3;
            return b;
        }

        private static int CalculateC() {
            int c = 4;
            return c;
        }
    }
}
