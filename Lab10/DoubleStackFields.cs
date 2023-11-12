using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab02 {
    public partial class DoubleStack {
        public const string CLASS_NAME = "DoubleStack"; 

        private static int _totalInstanceCount;

        private static int _currentInstanceCount;

        private static readonly DateTime _firstCreationTime;

        private readonly int _id;

        private readonly DateTime _creationTime;
        
        private readonly List<double> _storage;

        private string _title;
    }
}
