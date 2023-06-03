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
        public float ? Kaloryczność { get;set; }
        [StringLength(maximumLength: 6)]
        public float ? Białko { get; set; }
        [StringLength(maximumLength: 7)]
        public float ? Tłuszcz { get; set; }
        [StringLength(maximumLength: 11)]
        public float ? Węglowodany { get; set; }
        [StringLength(maximumLength: 7)]
        public float ? Błonnik { get; set; }
       
    }
}
