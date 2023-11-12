using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab09 {
    internal class ConcertDatabase {
        private readonly string _name;
        private readonly Dictionary<string, Concert> _database = new Dictionary<string, Concert>();

        public string Name { get => _name; }

        public ConcertDatabase(string name) {
            _name = name;
        }

        public void AddConcert(string id, Concert concert) {
            _database.Add(id, concert);
        }

        public bool RemoveConcert(string id) {
            return _database.Remove(id);
        }

        public Concert GetConcert(string id) {
            return _database[id];
        }

        public override string ToString() {
            string output = $"\tConcertDatabase '{_name}'\n";
            foreach (var pair in _database) {
                output += $"<{pair.Key}>: {pair.Value}\n";
            }
            output = output.Remove(output.Length - 1);
            return output;
        }
    }
}
