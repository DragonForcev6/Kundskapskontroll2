ShoppingList list = new ShoppingList("items.txt");
list.Load();

while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj:");

    if (!int.TryParse(Console.ReadLine(), out int choice)) //Sätter in hela choice i en if-sats och TryParse så att
    // inte programmet kraschar om någon skriver in en bokstav eller tecken.
    {
        Console.WriteLine("Skriv gärna en siffra mellan 1 och 5."); //Skriver ut felmeddelande om användaren 
        continue;
    }

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name)) // Kollar att användaren skriver in ett namn annars visas ett felmeddelande.
        {
            Console.WriteLine("Varan sparades inte: du måste skriva ett namn.");
            continue;
        }

        Console.Write("Pris: ");
        if (!int.TryParse(Console.ReadLine(), out int price) || price <= 0) //if-sats med TryParse så att inte programmet kraschar, 
        //om användaren skriver in något annat än siffror. || price <= 0 kollar att priset är större än 0.
        {
            Console.WriteLine("Varan sparades inte: du måste skriva ett giltigt pris.");
            continue;
        }
        try
        {
            if (list.Add(new Item(name, price)))
            {
                Console.WriteLine($"Lade till {name} för {price} kr i ShoppingListan."); //Skriver ut vad som lagts till i listan.

            }
            else
            {
                Console.WriteLine($"Varan lades inte till: du har nått maxtaket på {ShoppingList.MaxTotal} kr.");
                // Felmeddelandet visas om maxtaket 5000 överskrids.
            }

        }

        catch (ArgumentException ex) // Om Item säger nej (tomt namn eller fel pris) får användaren veta varför.
        {
            Console.WriteLine($"Varan lades inte till: {ex.Message}");
        }
    }

    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        if (int.TryParse(Console.ReadLine(), out int number)) //Kollar att användaren skrivit siffror.
        {

            if (list.RemoveAt(number)) //När varan tas bort får användaren veta det.
            {
                Console.WriteLine("Varan togs bort.");
            }
            else
            {
                if (list.Count == 0)
                {
                    Console.WriteLine("Listan är redan tom.");
                }
                else
                {
                    Console.WriteLine($"Ogiltigt nummer, skriv in ett nummer mellan 1 och {list.Count}.");
                }
            }
        }
        else
        {
            Console.WriteLine("Nu blev det fel, försök att skriva med siffror."); //skriver ut 
                                                                                  //felmeddelande om användaren skriver något annat än siffror.
        }

    }
    else if (choice == 3)
    {
        if (list.Save())
        {
            Console.WriteLine("ShoppingListan är sparad."); //Skriver ut att listan är sparad.
        }
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(wanted))
        {
            Console.WriteLine("Skriv vad du söker efter.");
            continue;
        }

        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
    else
    {
        //Hit kommer vi bara om choice inte var 1, 2, 3, 4, eller 5
        Console.WriteLine("Skriv gärna en siffra mellan 1 och 5.");
    }
}
