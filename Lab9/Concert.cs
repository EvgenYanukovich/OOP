using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab09 {
    internal class Concert {
        private string _title;
        private string _location;
        private DateOnly _date;
        private ConcertArtists _artists;

        public string Title { get => _title; }
        public string Location { get => _location; }
        public DateOnly Date { get => _date; }
        public ConcertArtists Artists { get => _artists; }

        public Concert(string title, string location, DateOnly date, IEnumerable<string> artists) {
            _title = title;
            _location = location;
            _date = date;
            _artists = new ConcertArtists(artists);
        }

        public override string ToString() {
            return $"Concert \"{_title}\" in {_location} {_date} with artists {string.Join(", ", _artists)}";
        }
    }
}
