using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTrasure
{
    class SearchResult
    {
        public int Steps { get; set; }
        public Node Node { get; set; }
        public bool Found { get; set; }
        public List<char> History { get; set; }

        public SearchResult()
        {
            History = new List<char>();
        }
    }
}
