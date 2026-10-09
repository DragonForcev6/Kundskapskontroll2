// Holds the items and takes care of loading and saving them.
public class ShoppingList
{
    readonly List<Item> items = new List<Item>(); // readonly för både List<Item> och string path ska finnas kvar
    //  i minnet under hela programmets gång. readonly gör att kompliatorn varnar om du av misstag råkar ändra den.
    readonly string path;

    public ShoppingList(string path)
    {
        this.path = path;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public bool RemoveAt(int number) 
    {
        if (0 == items.Count) // Om listan är tom kommer ett felmeddelande.
        {
            Console.WriteLine("Listan är redan tom.");
            return false;
        }

        if (number < 1 || number > items.Count) //Kollar om användaren skriver in ett nummer som är mindre än 1 eller
        // större än antal varor i listan.
       
        {
            Console.WriteLine($"Ogiltigt nummer skriv in ett nummer mellan 1 och {items.Count}.");
            return false;
        }

        items.RemoveAt(number - 1);
        return true;
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++) //Det här borde vara 0 annars blir inte första varan i index plockad
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public bool Save()
    {
    try
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }
        
        File.WriteAllLines(path, lines);
        return true;

    }
        catch (UnauthorizedAccessException) //Kollar om filen är skrivskyddad 
        //eller användaren är obehörig att skriva till filen. Om det är så visas ett felmeddelande.
        {
           Console.WriteLine("Listan sparades inte: du saknar behörighet.");
             return false;
        }
           
        catch (DirectoryNotFoundException) //Kollar om mappen i path finns inte.
        {
           Console.WriteLine("Kunde inte sparas: då mappen inte finns.");
           return false;
        }
    
           catch (IOException ex) //Kollar om filen används i ett annat program, eller disken är full.
        {
           Console.WriteLine($"Listan sparades inte: {ex.Message}");
           return false;
        } 
    }

    // Reads the file back into the list.
    public void Load() 
    {
        if (!File.Exists(path)) //Kollar om filen finns, om den inte finns händer 
        //inget men om den finns hoppar den vidare i koden.
        {
            return;
        }
        foreach (string line in File.ReadAllLines(path)) //Läser hela filen vid path o retunerar en string arreyer
        //där varje element är en rad från filen. Foreachloopar arreyen en rad i taget o sparar den i line.
        {
            if (string.IsNullOrWhiteSpace(line)) // Hoppar över raden om den är tom eller bara har blanksteg. 
            {  
                continue; 
            }
            string[] parts = line.Split(';'); //Splitar linjerna med ett semikolon ;

             items.Add(new Item(parts[1], int.Parse(parts[0])));
        }
  
    }
}
