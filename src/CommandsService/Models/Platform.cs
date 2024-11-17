using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models;

public class Platform
{
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// ExternalId is the ID of the platform in the external service
    /// </summary>
    [Required]
    public int ExternalId { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Command> Commands { get; set; } = [];
}