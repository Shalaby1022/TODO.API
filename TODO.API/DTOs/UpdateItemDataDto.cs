﻿namespace TODO.API.DTOs
{
    public class UpdateItemDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; } 
    }
}
