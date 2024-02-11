using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotasApi.Models;
using NotasApi.Models.DTOs;
using NotasApi.Repositories.IRepositories;

namespace NotasApi.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _ntRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IMapper _mapper;

        public NotesController(IMapper mapper, INoteRepository ntRepo, ITagRepository tagRepo)
        {
            _ntRepo = ntRepo;
            _mapper = mapper;
            _tagRepo = tagRepo;
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] CreateNoteDTO createNoteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createNoteDTO == null)
            {
                return BadRequest(ModelState);
            }


            var note = _mapper.Map<Note>(createNoteDTO);

            note.Tag = _tagRepo.GetTagById(createNoteDTO.tagId==null? 0 : Int32.Parse(createNoteDTO.tagId.ToString()));
            if (!_ntRepo.CreateNote(note))
            {
                ModelState.AddModelError("", $"Couldn`t create Note {note.Title}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNoteById", new { noteId = note.Id }, note);
        }

        [HttpGet("{noteId}", Name = "GetNoteById")]
        public IActionResult GetNoteById(int noteId)
        {
            var note = _ntRepo.GetNoteById(noteId);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            var noteList = _ntRepo.GetNotes();

            var noteListDTO = new List<NoteResponseDto>();

            foreach (var n in noteList)
            {
                noteListDTO.Add(_mapper.Map<NoteResponseDto>(n));
            }
            return Ok(noteListDTO);
        }

        [HttpGet("{archived:bool}", Name = "ActiveOrArchived")]
        public IActionResult GetActiveOrArchivedNotes(bool archived)
        {
            var noteList = _ntRepo.GetActiveOrArchivedNotes(archived);

            var noteListDTO = new List<NoteResponseDto>();

            foreach (var n in noteList)
            {
                noteListDTO.Add(_mapper.Map<NoteResponseDto>(n));
            }
            return Ok(noteListDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteNote")]
        public IActionResult DeleteNote(int id)
        {
            if (!_ntRepo.ExistNote(id))
            {
                return NotFound();
            }

            var note = _ntRepo.GetNoteById(id);

            if (!_ntRepo.DeleteNote(note))
            {
                ModelState.AddModelError("", $"Couldn`t delete Note {note.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch]
        public IActionResult UpdateNotes(int idNote, [FromBody] CreateNoteDTO noteDTO)
        {
            if (!_ntRepo.ExistNote(idNote))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (noteDTO == null)
            {
                return BadRequest(ModelState);
            }

            var note = _mapper.Map<Note>(_ntRepo.GetNoteById(idNote));

            note.Title = noteDTO.Title;
            note.Content = noteDTO.Content;
            note.tagId = noteDTO.tagId;
            note.Tag=_tagRepo.GetTagById(noteDTO.tagId == null ? 0 : Int32.Parse(noteDTO.tagId.ToString()));

            if (!_ntRepo.UpdateNote(note))
            {
                ModelState.AddModelError("", $"Couldn`t update Note {note.Title}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPatch("{noteId:int}/toggle/{archive:bool}", Name="ToogleArchive")]
        public IActionResult ToggleArchiveNote(int noteId, bool archive)
        {
            if (!_ntRepo.ExistNote(noteId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = _mapper.Map<Note>(_ntRepo.GetNoteById(noteId));

            note.IsArchived = archive;

            if (!_ntRepo.UpdateNote(note))
            {
                ModelState.AddModelError("", $"Couldn`t update Note {note.Title}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("GetNotesInTag/{tId:int}")]
        public IActionResult GetNotesInTag(int tId)
        {
            var noteList = _ntRepo.GetNotesInTag(tId);

            if(noteList==null)
            {
                return NotFound();
            }

            var itemNote = new List<NoteResponseDto>();

            foreach (var n in noteList)
            {
                itemNote.Add(_mapper.Map<NoteResponseDto>(n));
            }
            return Ok(itemNote);
        }
    }
}
