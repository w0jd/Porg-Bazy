using projekt.Models;

namespace projekt.ViewModels
{
    public class DanieViewModel
    {
        [Required(ErrorMessage = "Nazwa dania jest wymagana.")]
        public string? NazwaDania { get; set; }

        public List<Produkt> Produkty { get; set; }
        
        public List<Decimal>? Ilosc { get; set; }
    }

}
