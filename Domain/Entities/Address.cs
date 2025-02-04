using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEFApp.Domain.Entities;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Street { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string City { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string PostalCode { get; set; } = string.Empty;

    [ForeignKey(nameof(ClientType))]
    public int ClientTypeId { get; set; }

    public ClientType? ClientType { get; set; }
}
