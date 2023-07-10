using projekt.Models;

namespace projekt.ViewModels
{
    public class DanieViewModel
    {
        [Required(ErrorMessage = "Nazwa dania jest wymagana.")]
        public string? NazwaDania { get; set; }

        public List<Produkt> Produkty { get; set; } = new List<Produkt>();
        public List<int>? IdProduktu { get; set; }

        [Range(1, 1000, ErrorMessage = "Wartość powinna być od 1 do 1000")]
        public List<Decimal>? Ilosc { get; set; } 

        [NotMapped]
        public bool IsDeleted { get; set; } = false;
    }

}
