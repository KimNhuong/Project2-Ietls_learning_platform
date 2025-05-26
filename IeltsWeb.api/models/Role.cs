using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IeltsWeb.api.models;

namespace IeltsWeb.api.Models;

public class Role
{
    [Key]
    public int RoleId { get; set; }

    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; } = string.Empty;

    // Navigation property
    public ICollection<User> Users { get; set; }

    public Role()
    {
        Users = new List<User>();
    }
}
