﻿namespace NotasApi.Models.DTOs
{
    public class NoteResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsArchived { get; set; }
        public int? tagId { get; set; }
    }
}
