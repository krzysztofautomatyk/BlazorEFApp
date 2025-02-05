using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEFApp.Domain.Entities;

public class ClientType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public bool IsAvailable { get; set; } = true;

    public ICollection<Address>? Addresses { get; set; }

}
