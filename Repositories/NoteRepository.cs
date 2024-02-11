using Microsoft.EntityFrameworkCore;
using NotasApi.Data;
using NotasApi.Models;
using NotasApi.Repositories.IRepositories;

namespace NotasApi.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _db;
        public NoteRepository(ApplicationDbContext db)
        {
            _db = db;     
        }

        public bool CreateNote(Note note)
        {
            note.CreationDate = DateTime.Now;
            note.IsArchived = false;
            _db.Notes.Add(note);
            return Save();
        }

        public bool DeleteNote(Note note)
        {
            _db.Notes.Remove(note);
            return Save();
        }

        public bool ExistNote(int id)
        {
            var note = _db.Notes.Find(id);
            return note!=null?true:false;
        }

        public List<Note> GetActiveOrArchivedNotes(bool archived)
        {
            return _db.Notes.OrderBy(n => n.CreationDate).Where(n=>n.IsArchived== archived).ToList();
        }

        public Note GetNoteById(int id)
        {
            return _db.Notes.FirstOrDefault(n => n.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool ToggleArchiveNote(int noteId, bool archive)
        {
            var note = _db.Notes.Find(noteId);

            if (note == null)
            {
                return false;
            }

            note.IsArchived = archive;
            note.ModificationDate= DateTime.Now;
            _db.Notes.Update(note);
            return Save();
        }

        public bool UpdateNote(Note updatedNote)
        {
            updatedNote.ModificationDate= DateTime.Now;
            _db.Notes.Update(updatedNote);
            return Save();
        }
        public List<Note> GetNotes()
        {
            return _db.Notes.OrderBy(n=>n.CreationDate).ToList();
        }

        public ICollection<Note> GetNotesInTag(int tId)
        {
            return _db.Notes.Include(n => n.Tag).Where(n=>n.tagId==tId).ToList();        
        }
    }
}
