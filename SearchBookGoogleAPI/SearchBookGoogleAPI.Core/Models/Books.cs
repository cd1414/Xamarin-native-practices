using System;
using System.Collections.Generic;

namespace MyBookshelf.Core.Models
{
    public class Books
    {
        public string Kind { get; set; }
        public string TotalItems { get; set; }
        public BookInfo[] Items { get; set; }
    }
}

