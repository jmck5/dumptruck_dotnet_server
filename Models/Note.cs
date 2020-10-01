using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dumptruck.Models
{
    public class Note
    {
        public long NoteId { get; set; }
        public string NoteContent { get; set; }
        public string NoteAuthor { get; set; }
        public TimestampAttribute NoteTimeStamp { get; set; }

    }
}
