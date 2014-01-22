using System;
using Gos.SimpleObjectStore;

namespace BooksCollector.Core
{
    public class Book : IEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }

        public override bool Equals(object obj)
        {
            if(!(obj is Book)) return false;
            return Isbn.Equals(((Book)obj).Isbn);
        }

        public override int GetHashCode()
        {
            return Isbn.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("[{0}]", Isbn);
        }
    }
}