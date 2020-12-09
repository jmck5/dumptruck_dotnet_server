using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dumptruck.Models
{
    public class NoteDTO
    {
        public long NoteId { get; set; }
        public string NoteContent { get; set; }
        public string NoteAuthor { get; set; }
    }
}
