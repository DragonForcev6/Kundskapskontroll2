// One item on the shopping list.
public class Item // Gör klassen public så att den kan användas i ShoppingList.cs och Program.cs
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
