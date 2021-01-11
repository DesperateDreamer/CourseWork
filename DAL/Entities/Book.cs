using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Serializable]
    public class Book
    {
        private string title;
        private string author;
        private bool isInLibrary;

        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public bool IsInLibrary { get => isInLibrary; set => isInLibrary = value; }
    }
}
