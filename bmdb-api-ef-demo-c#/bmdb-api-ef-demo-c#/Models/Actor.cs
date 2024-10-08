﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace bmdb_api_ef_demo_c_.Models;

[Table("Actor")]
[Index("FirstName", "LastName", "Birthdate", Name = "UQ_Actor_Name_bd", IsUnique = true)]
public partial class Actor
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string Gender { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    [JsonIgnore]
    [InverseProperty("Actor")]
    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();
}
