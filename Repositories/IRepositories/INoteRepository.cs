using NotasApi.Models;

namespace NotasApi.Repositories.IRepositories
{
    public interface INoteRepository
    {
        List<Note> GetNotes();

        Note GetNoteById(int id);

        bool CreateNote(Note note);

        bool UpdateNote(Note updatedNote);

        bool DeleteNote(Note note);

        bool ExistNote(int id);

        bool ToggleArchiveNote(int noteId, bool archive);

        List<Note> GetActiveOrArchivedNotes(bool archived);

        bool Save();
    }
}
