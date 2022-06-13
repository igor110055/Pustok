using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Fullname {get; set; }
        public DateTime BirthDate { get; set; }

        public List<Book> Books { get; set; }
}
}
