// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    public ShoppingList(string path)
    {
        this.path = path;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number) 
    {
        items.RemoveAt(number - 1);
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 1; i < items.Count; i++) //Det här borde vara 0 annars blir inte första varan i index plockad
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
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n"); 
        }
        catch
        {
            
        }

        Console.WriteLine("Listan är sparad."); // Skriver ut att listan är sparad även om den kanske inte är det
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
            if (string.IsNullOrWhiteSpace(line)) // Om raden skulle vara null eller ha blanksteg hoppar den över raden.
            {
                continue; 
            }
            string[] parts = line.Split(';'); //Splitar linjerna med ett semikolon ;

             items.Add(new Item(parts[1], int.Parse(parts[0])));
        }
       
           
       //

        
       
    }
}
