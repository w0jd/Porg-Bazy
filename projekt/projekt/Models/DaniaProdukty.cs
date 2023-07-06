using System;
using System.Collections.Generic;

namespace projekt.Models;

public partial class DaniaProdukty
{
    
    [ForeignKey("IdProduktu")]
    public int IdProduktu { get; set; }
    public Produkt Produkty { get; set; }

    [ForeignKey("IdDania")]
    public int IdDania { get; set; }
    public Dania Dania { get; set; }

    [Range(1, 500, ErrorMessage = "Numer musi być w zakresie od 1 do 500.")]
    public int Ilość { get; set; }

    


}
