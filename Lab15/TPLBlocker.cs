
using System.Collections.Concurrent;

namespace OOP_lab_15 {
    //использование BlockeningCollection
    internal static class TPLBlocker {
        static BlockingCollection<string> _warehouse = new BlockingCollection<string>();

        ///Перзагрузка
        public static void RefreshShop() {
            Task[] suppliers = new Task[5];
            Task[] customers = new Task[10];

            for (int i = 0; i < suppliers.Length; i++) {
                suppliers[i] = Task.Run(Supplier);
            }

            for (int i = 0; i < customers.Length; i++) {
                customers[i] = Task.Run(Customer);
            }

            Task.WaitAll(suppliers);
            _warehouse.CompleteAdding();
            Task.WaitAll(customers);
        }
        //Заполнить
        private static void Supplier() {
            string[] products = { "TV", "Refregerator", "washing mashine", "combaine", "culler" };
            Random random = new Random();

            foreach (string product in products) {
                Thread.Sleep(random.Next(1000, 3000));

                _warehouse.Add(product);
                Console.WriteLine($"added product: {product}");
                PrintWarehouseContents();
            }
        }
        //Извлечь
        private static void Customer() {
            Random random = new Random();

            while (!_warehouse.IsCompleted) {
                Thread.Sleep(random.Next(500, 2000));

                string? product = null;

                if (_warehouse.TryTake(out product)) {
                    Console.WriteLine($"bought product: {product}");
                    PrintWarehouseContents();
                } else {
                    Console.WriteLine("we dont have it");
                }
            }
        }

        private static void PrintWarehouseContents() {
            Console.WriteLine("product on base: " + string.Join(", ", _warehouse));
        }
    }
}
