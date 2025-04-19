using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ngotracker.Models.GrantModels;
using ngotracker.Models.NgoModels;

namespace ngotracker.Models.ApplicationModels;

public class ApplicationModel
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("Ngo")]
    public Guid NgoId { get; set; }

    public required NgoModel Ngo { get; set; }

    [ForeignKey("Grant")]
    public Guid GrantId { get; set; }

    public required GrantModel Grant { get; set; }

    [Required]
    public required string Status { get; set; }

    public DateTime? SubmissiDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(100)]
    public required string Notes { get; set; }
}
