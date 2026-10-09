// One item on the shopping list.
public class Item // Gör klassen public så att den går att använda överallt i projektet.
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price) //Konstruktor: körs när ett nytt Item skapas
    {
        if (string.IsNullOrWhiteSpace(name)) //Kollar om namnet null, tomt eller bara blanksteg 
        {
            throw new ArgumentException("Varan måste vara ett namn."); /// Avbryter och kastar ett fel
        }

        if (price < 0) // Kontrolerar så det inte är negativt pris
        {
            throw new ArgumentException("Priset kan inte vara negativt.");
        }

        Name = name; // Körs bara om båda kontrollerna klarades
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
