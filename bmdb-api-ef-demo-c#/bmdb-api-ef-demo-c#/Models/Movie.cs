﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace bmdb_api_ef_demo_c_.Models;

[Table("Movie")]
[Index("Title", "Year", Name = "UQ_Movie_Title_Year", IsUnique = true)]
public partial class Movie
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    public short Year { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? Rating { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Director { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Movie")]
    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();
}
