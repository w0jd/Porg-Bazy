namespace projekt.Models
{
    public class Produkt
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [StringLength(maximumLength: 61)]
        public string ?Nazwa { get; set; }
        [StringLength(maximumLength: 12)]
        public string ? Kaloryczność { get;set; }
        [StringLength(maximumLength: 6)]
        public string ? Białko { get; set; }
        [StringLength(maximumLength: 7)]
        public string ? Tłuszcz { get; set; }
        [StringLength(maximumLength: 11)]
        public string ? Węglowodany { get; set; }
        [StringLength(maximumLength: 7)]
        public string ? Błonnik { get; set; }
       
    }
}
