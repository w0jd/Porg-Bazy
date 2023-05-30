namespace projekt.Models
{
    public class LoginModel
    {
        [Required]

        public string Nazwa { get; set; }
        [Required]
        public string Haslo { get; set; }
    }
}
