using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTrasure
{
    class Node
    {
        public bool Visited { get; set; }

        public bool IsRoot { get; set; }

        private readonly char _label;
        public char Label
        {
            get { return _label; }
        }

        private readonly int _column;
        public int Column
        {
            get { return _column; }
        }

        private readonly int _row;
        public int Row
        {
            get { return _row; }
        }

        public Node(char c, int column, int row, bool isRoot)
        {
            _label = c;
            _column = column;
            _row = row;
            IsRoot = isRoot;
        }
    }
}
