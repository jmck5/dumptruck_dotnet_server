using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dumptruck.Models;

namespace dumptruck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteContext _context;

        public NotesController(NoteContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNotes()
        {
            //should verify auth token first
            ScoobyAuthenticate(Request);
            return await _context.Notes.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/Notes/word?myword=""
        [HttpGet("word")]
        public async Task<List<NoteDTO>> GetNotes(string myWord) 
        {
            ScoobyAuthenticate(Request);
            List<NoteDTO> filtered = new List<NoteDTO>();
            IEnumerable<Note> filterQuery =
                 from scoobyNote in _context.Notes
                 where scoobyNote.NoteContent.Contains(myWord)
                 select scoobyNote;
            foreach (var note in filterQuery) {
                NoteDTO note_dto = ItemToDTO(note);
                filtered.Add(note_dto);
            }
            return filtered;
        }


        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDTO>> GetNote(long id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return ItemToDTO(note);
        }

        // PUT: api/Notes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(long id, NoteDTO noteDTO)
        {
            if (id != noteDTO.id)
            {
                return BadRequest();
            }

            var noteItem = await _context.Notes.FindAsync(id);
            if (noteItem == null)
            {
                return NotFound();
            }

            noteItem.NoteContent = noteDTO.NoteContent;
            noteItem.NoteAuthor = noteDTO.NoteAuthor;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NoteDTO>> CreateNote(NoteDTO noteDTO)
        {
            var noteItem = new Note
            {
                NoteContent = noteDTO.NoteContent,
                NoteAuthor = noteDTO.NoteAuthor
            };

            _context.Notes.Add(noteItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = noteItem.id }, ItemToDTO(noteItem));
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(long id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(long id)
        {
            return _context.Notes.Any(e => e.id == id);
        }

        public bool ScoobyAuthenticate(HttpRequest req) {
            
            return true;
        }

        private static NoteDTO ItemToDTO(Note note) =>
            new NoteDTO
            {
                NoteContent = note.NoteContent,
                NoteAuthor = note.NoteAuthor,
                id = note.id
            };
    }
}
