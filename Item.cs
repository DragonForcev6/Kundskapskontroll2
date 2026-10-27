// One item on the shopping list.
public class Item // Gör klassen public så att den går att använda överallt i projektet.
{
    public string Name { get; } // Egenskapen tilldelas i konstruktorn, men ingen annanstans.
    public int Price { get; }

    public Item(string name, int price) //Konstruktor: körs när ett nytt Item skapas
    {
        if (string.IsNullOrWhiteSpace(name)) //Kollar om namnet är null, tomt eller bara blanksteg 
        {
            throw new ArgumentException("Varan måste ha ett namn."); /// Avbryter och kastar ett fel
        }

        if (price <= 0) // Kontrollerar att priset är större än 0, stoppar även 0,
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
