using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ProjectCompleteWorks.Entitys
{
    public class SearchBase
    {
        public int Page { get; set; } = 1;

        public int Size { get; set; } = 10;

        public string Text { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }
    }
}
