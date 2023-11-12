using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace lab09 {
    internal class Program {
        static void Main(string[] args) {
            Task1();
            Task2();
            Task3();
        }

        static void Task1() {
            Console.WriteLine("\n\t\tTask1");

            ConcertDatabase db = new ConcertDatabase("Belarus Concerts'24");

            db.AddConcert("EGFN", new("Electro Groove Fusion Night", "Minsk", new(2024, 05, 15), new List<string>() { "Daft Punk", "Disclosure", "Moby" }));
            db.AddConcert("RLR", new("Rock Legends Reunion", "Postavy", new(2024, 08, 20), new List<string>() { "Led Zeppelin", "Queen", "Samurai" }));
            db.AddConcert("CMG", new("Classical Masterpieces Gala", "Baranovichy", new(2024, 12, 15), new List<string>() { "Yo-Yo Ma", "Lang Lang", "Sarah Chang" }));
            db.AddConcert("CMR", new("Country Music Roundup", "Vitebsk", new(2024, 10, 06), new List<string>() { "Johnny Cash", "Dolly Parton", "Willie Nelson" }));

            Console.WriteLine($"\n{db}\n");

            db.RemoveConcert("CMG");

            Console.WriteLine($"\n{db}\n");

            Console.WriteLine($"Found RLR concert: {db.GetConcert("RLR")}");
        }

        static void Task2() {
            Console.WriteLine("\n\t\tTask2");

            Dictionary<int, string> dict = new() {
                { 0, "zero" }
            };

            dict.Add(1, "one");
            Console.WriteLine($"TryAdd: {dict.TryAdd(2, "two")}");
            dict.Add(3, "three");

            Console.WriteLine("\tdict:");
            foreach (var pair in dict) {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }

            dict.Remove(1);
            dict.Remove(2);

            Console.WriteLine("\tdict:");
            foreach (var pair in dict) {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }

            List<int> dictKeys = new();
            foreach (int key in dict.Keys) {
                dictKeys.Add(key);
            }

            Console.WriteLine($"dictKeys: {string.Join(", ", dictKeys)}");

            Console.WriteLine($"dictKeys.FindIndex((int el) => el == 3): {dictKeys.FindIndex((int el) => el == 3)}");
        }

        static void Task3() {
            Console.WriteLine("\n\t\tTask3");

            ObservableCollection<Concert> observableConcerts = new();

            observableConcerts.CollectionChanged += (object? sender, NotifyCollectionChangedEventArgs e) => {
                Console.WriteLine("\t Event");
                Console.WriteLine($"sender: {sender}");
                Console.WriteLine($"Action: {e.Action}");
                Console.WriteLine($"OldStartingIndex: {e.OldStartingIndex}");
                Console.WriteLine($"NewStartingIndex: {e.NewStartingIndex}");
            };

            Concert concert1 = new("Electro Groove Fusion Night", "Minsk", new(2024, 05, 15), new List<string>() { "Daft Punk", "Disclosure", "Moby" });
            Concert concert2 = new("Classical Masterpieces Gala", "Baranovichy", new(2024, 12, 15), new List<string>() { "Yo-Yo Ma", "Lang Lang", "Sarah Chang" });
            Concert concert3 = new("Rock Legends Reunion", "Postavy", new(2024, 08, 20), new List<string>() { "Led Zeppelin", "Queen", "Samurai" });
            Concert concert4 = new("Country Music Roundup", "Vitebsk", new(2024, 10, 06), new List<string>() { "Johnny Cash", "Dolly Parton", "Willie Nelson" });

            observableConcerts.Add(concert1);
            observableConcerts.Add(concert2);
            observableConcerts.Add(concert3);
            observableConcerts.Add(concert4);

            observableConcerts.Remove(concert4);
            observableConcerts.Remove(concert1);
            observableConcerts.Remove(concert3);
            observableConcerts.Remove(concert2);
        }
    }
}