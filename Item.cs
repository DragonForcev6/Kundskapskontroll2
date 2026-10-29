// Gör klassen public så att den går att använda överallt i projektet.
public class Item
{
    // Egenskapen tilldelas i konstruktorn, men ingen annanstans.
    public string Name { get; }
    public int Price { get; }

    //Konstruktor: körs när ett nytt Item skapas
    public Item(string name, int price)
    {
        //Kollar om namnet är null, tomt eller bara blanksteg.
        if (string.IsNullOrWhiteSpace(name))
        {
            // Avbryter o kastar ett fel.
            throw new ArgumentException("Varan måste ha ett namn.");
        }

        // Kontrollerar att priset är större än 0.
        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Priset måste vara större än 0.");
        }

        Name = name; // Körs bara om båda kontrollerna klarades
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
