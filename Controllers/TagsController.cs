using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotasApi.Models.DTOs;
using NotasApi.Models;
using NotasApi.Repositories.IRepositories;

namespace NotasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepo;
        private readonly IMapper _mapper;

        public TagsController(IMapper mapper, ITagRepository tagRepo)
        {
            _tagRepo = tagRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateTag([FromBody] CreateTagDTO createTagDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createTagDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (_tagRepo.ExistTag(createTagDTO.Name))
            {
                return BadRequest(ModelState);
            }

            var tag = _mapper.Map<Tag>(createTagDTO);

            if (!_tagRepo.CreateTag(tag))
            {
                ModelState.AddModelError("", $"Couldn`t create Tag {tag.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTagById", new { tagId = tag.Id }, tag);
        }

        [HttpDelete("{id:int}", Name = "DeleteTag")]
        public IActionResult DeleteTag(int id)
        {
            if (!_tagRepo.ExistTag(id))
            {
                return NotFound();
            }

            var tag = _tagRepo.GetTagById(id);

            if (!_tagRepo.DeleteTag(tag))
            {
                ModelState.AddModelError("", $"Couldn`t delete Tag {tag.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetTags()
        {
            var tagList = _tagRepo.GetTags();

            var tagsListDTO = new List<TagResponseDTO>();

            foreach (var t in tagList)
            {
                tagsListDTO.Add(_mapper.Map<TagResponseDTO>(t));
            }
            return Ok(tagsListDTO);
        }

        [HttpGet("{tagId}", Name = "GetTagById")]
        public IActionResult GetTagById(int tagId)
        {
            var tag = _tagRepo.GetTagById(tagId);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPatch]
        public IActionResult UpdateTags(int idTag, [FromBody] CreateTagDTO tagDTO)
        {
            if (!_tagRepo.ExistTag(idTag))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (tagDTO == null)
            {
                return BadRequest(ModelState);
            }

            var tag = _mapper.Map<Tag>(_tagRepo.GetTagById(idTag));

            tag.Name = tagDTO.Name;

            if (!_tagRepo.UpdateTag(tag))
            {
                ModelState.AddModelError("", $"Couldn`t update Note {tag.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
