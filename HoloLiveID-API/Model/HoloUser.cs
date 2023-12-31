﻿using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Cosmos;

namespace HoloLiveID_API.Model
{
    public class HoloUser
    {
        [Key]
        public string HoloId { get; set; }
        public string Name { get; set; }
        public int? Gen { get; set; }
        public string? Description { get; set; }
        public string? Birthdate { get; set; }
        public string? Height { get; set; }
        public string? Zodiac { get; set; }
    }
}
