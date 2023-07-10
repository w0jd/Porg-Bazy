namespace projekt.Models;

public partial class Dania
{
    [Key]
    public int Id { get; set; }

    public string NazwaDania { get; set; } = null!;
    public ICollection<Jadlospis> Jadlospisy { get; set; }
    public ICollection<DaniaProdukty> DaniaProdukty { get; set; }
}
