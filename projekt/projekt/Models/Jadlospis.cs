using System;
using System.Collections.Generic;

namespace projekt.Models;

public partial class Jadlospis
{
    [ForeignKey("IdDania")]
    public int IdDania { get; set; }
    public Dania Dania { get; set; }
    public DateOnly Dzień { get; set; }

    [ForeignKey("IdKonta")]
    public int IdKonta { get; set; }
    public Konto Konta { get; set; }

}
