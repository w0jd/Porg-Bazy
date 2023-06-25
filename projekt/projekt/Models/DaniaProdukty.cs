using System;
using System.Collections.Generic;

namespace projekt.Models;
[Keyless]
public partial class DaniaProdukty
{
    
    [ForeignKey("IdProduktu")]
    public int IdProduktu { get; set; }
    public Produkt Produkty { get; set; }

    [ForeignKey("IdDania")]
    public int IdDania { get; set; }
    public Dania Dania { get; set; }

    public int Ilość { get; set; }

}
