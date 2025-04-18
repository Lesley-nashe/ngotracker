using System;
using System.ComponentModel.DataAnnotations;

namespace ngotracker.Models.NgoModels;

public class NgoModel
{
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public required string RegistrationNumber { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Sector { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Country { get; set; }

        [MaxLength(300)]
        public required string Address { get; set; }

        [Required]
        [EmailAddress]
        public required string ContactEmail { get; set; }

        [Phone]
        public required string ContactPhone { get; set; }

        [Url]
        public string? Website { get; set; }

        public string? LogoUrl { get; set; }

        public bool Verified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}
