using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngotracker.Models.GrantModels;

public class GrantModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(200)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Provider { get; set; }

    public double Amount { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Currency { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Description { get; set; }

    [MaxLength(300)]
    public required string Eligibility { get; set; }

    [Required]
    public required string Status { get; set; }

    [Phone]
    public required string ContactPhone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime Deadline { get; set; } = DateTime.UtcNow;



}
