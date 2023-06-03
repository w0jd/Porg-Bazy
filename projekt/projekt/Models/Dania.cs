using System;
using System.Collections.Generic;

namespace projekt.Models;

public partial class Dania
{
    [Key]
    public int Id { get; set; }

    public string NazwaDania { get; set; } = null!;
}
