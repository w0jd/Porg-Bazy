using System;
using System.Collections.Generic;

namespace projekt.Models;

public partial class DaniaProdukty
{
    [ForeignKey("IdProduktu")]
    public int IdProduktu { get; set; }

    [ForeignKey("IdDania")]
    public int IdDania { get; set; }

    public int Ilość { get; set; }

}
