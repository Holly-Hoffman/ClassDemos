﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace bmdb_api_ef_demo_c_.Models;

[Table("User")]
[Index("Username", Name = "UQ_User", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Password { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(12)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    [StringLength(70)]
    [Unicode(false)]
    public string Email { get; set; } = null!;
}
