using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab09 {
    internal class ConcertArtists : IEnumerable<string> {
        private List<string> _artists;

        public List<string> AsList { get => _artists; }
        public int Count { get => _artists.Count; }
        public bool IsReadOnly { get => false; }

        public ConcertArtists(IEnumerable<string> artists) {
            _artists = MakeUnique(new List<string>(artists));
        }

        private static List<string> MakeUnique(List<string> oldList) {
            List<string> newList = new();

            foreach (string artist in oldList) {
                if (!newList.Contains(artist)) { 
                    newList.Add(artist);
                }
            }

            return newList;
        }

        public void Add(string artist) {
            if (_artists.Contains(artist)) {
                return;
            }
            _artists.Add(artist);
        }

        public void Clear() {
            _artists.Clear();
        }

        public bool Remove(string artist) {
            return _artists.Remove(artist);
        }

        public bool Contains(string artist) {
            return _artists.Contains(artist);
        }

        public IEnumerator<string> GetEnumerator() {
            return new ConcertArtistsEnumerator(_artists);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public class ConcertArtistsEnumerator : IEnumerator<string> {
            private readonly List<string> _artists;
            private int _index = -1;

            public object Current {
                get =>_artists[_index];
            }

            string IEnumerator<string>.Current {
                get => _artists[_index];
            }

            public ConcertArtistsEnumerator(List<string> artists) {
                _artists = artists;
            }

            public void Reset() {
                _index = -1;
            }

            public bool MoveNext() {
                _index++;
                return _index < _artists.Count;
            }

            public void Dispose() { }
        }
    }
}
