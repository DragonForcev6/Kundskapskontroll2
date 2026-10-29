// Holds the items and takes care of loading and saving them.
public class ShoppingList
{
    // Max tak för ShoppingListan
    public const int MaxTotal = 5000;

    // readonly för både List<Item> och string path ska finnas kvar i minnet under hela 
    // programmets gång. readonly gör att kompilatorn varnar om du av misstag råkar ändra den.
    readonly List<Item> items = new List<Item>();
    readonly string path;

    // Låter Program.cs se antalet varor utan att kunna ändra i listan.
    public int Count => items.Count;

    public ShoppingList(string path)
    {
        this.path = path;
    }

    public bool Add(Item item)
    {
        // Kollar att användaren inte överskrider maxtaket.
        if (Total() + item.Price > MaxTotal)
        {
            return false;
        }

        items.Add(item);
        return true;
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public bool RemoveAt(int number)
    {
        if (0 == items.Count) // Om listan är tom returneras false.
        {
            return false;
        }
        //Kollar om användaren skriver in ett nummer som är mindre 
        // än 1 eller större än antal varor i listan.
        if (number < 1 || number > items.Count)
        {
            return false;
        }

        items.RemoveAt(number - 1);
        return true;
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        //Det här borde vara 0 annars blir inte första varan i index plockad
        for (int i = 0; i < items.Count; i++)
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
            if (item.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
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
        //Kollar om filen är skrivskyddad eller användaren är obehörig att skriva till 
        // filen. Om det är så visas ett felmeddelande.
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Listan sparades inte: du saknar behörighet.");
            return false;
        }
        //Kollar om mappen i path inte finns.
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Listan sparades inte: mappen finns inte.");
            return false;
        }
        //Kollar om filen används i ett annat program, eller disken är full.
        catch (IOException ex)
        {
            Console.WriteLine($"Listan sparades inte: {ex.Message}");
            return false;
        }
    }

    // Reads the file back into the list.
    public void Load()
    {
        //Kollar om filen finns, om den inte finns händer inget men 
        // om den finns hoppar den vidare i koden.
        if (!File.Exists(path))
        {
            return;
        }
        //Läser hela filen vid path o returnerar en string-array där varje element är 
        // en rad från filen. foreach loopar arrayen en rad i taget o sparar den i line.
        foreach (string line in File.ReadAllLines(path))
        {
            // Hoppar över raden om den är tom eller bara har blanksteg. 
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            // Delar raden vid första semikolon ;.
            string[] parts = line.Split(';', 2);

            // Hoppar över raden om semikolon saknas eller om priset inte är en siffra, 
            // annars kraschar programmet.
            if (parts.Length < 2 || !int.TryParse(parts[0], out int price))
            {
                Console.WriteLine($"En rad i filen hoppades över: \"{line}\" har fel format.");
                continue;
            }

            try
            {
                // Använder Add så att taket gäller även när filen läses in.
                if (!Add(new Item(parts[1], price)))
                {
                    Console.WriteLine($"{parts[1]} lades inte till: maxtaket på {MaxTotal} kr är nått.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"En rad i filen hoppades över: {ex.Message}");
            }
        }

    }
}
