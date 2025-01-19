using System;
using System.Collections.Generic;

namespace FormFiller.Models;

public partial class User
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string PostalCode { get; set; } = null!;

    public string StreetName { get; set; } = null!;

    public string HouseNumberAddition { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
