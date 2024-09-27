using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace bmdb_api_ef_demo_c_.Models;

[Table("Credit")]
[Index("ActorId", "MovieId", Name = "UQ_Credit_Actor_Movie", IsUnique = true)]
public partial class Credit
{
    [Key]
    public int Id { get; set; }

    public int MovieId { get; set; }

    [Column("ActorID")]
    public int ActorId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Role { get; set; }

    [ForeignKey("ActorId")]
    [InverseProperty("Credits")]
    public virtual Actor? Actor { get; set; } = null!;

    [ForeignKey("MovieId")]
    [InverseProperty("Credits")]
    public virtual Movie? Movie { get; set; } = null!;
}
