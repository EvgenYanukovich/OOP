
namespace OOP_lab_15 {
    internal static class TPLAsyncTasks {
        //синтаксис async/await
        public static async Task InvokeAsyncTask() {
            Console.WriteLine("async method start");

            await DoSomethingAsync();

            Console.WriteLine("wait untill done");
        }

        private static async Task DoSomethingAsync() {
            Console.WriteLine("async operation start");

            await Task.Delay(2000);

            Console.WriteLine("done.");
        }
    }
}
