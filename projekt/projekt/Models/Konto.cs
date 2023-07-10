namespace projekt.Models
{
    public class Konto
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]

        public string Nazwa { get; set; }
        [Required]
        public string Haslo { get; set; }
        public ICollection<Jadlospis> Jadlospisy { get; set; }
    }
}
