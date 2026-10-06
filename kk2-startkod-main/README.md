(Skriv om o Markdown!)

Vad jag vill ändra i Program.cs
int choice = int.Parse(Console.ReadLine()); 
Får programmet att krascha om man inte använder siffror
Sätter hela choice i en if satts och använder TryParse istället för Pars som inte kraschar programet
om man skriver bokstäver eller symboler.
I Choice 1 o Choice 2 sätter Sätter TryParse istället för en Pars i en if sats som kollar så användaren 
skriver siffror,om bokstäver eller symboler används kommer det upp ett fel meddelande.

Claude sa att jag skulle lägga till <ItemGroup> i csproj för att filen items.txt ska ingå i projektet o 
bli kopierad till byggmappen (bin\Debug\net10.0).

Vad jag vill ändra i ShoppingList.cs
File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n"); i save använder \r\n medan 
Load bara använder \n det medför att \r kommer hänga med vissa variablar ibland.

Fick hjälp utav Claude med Load, jag tänkte att programet borde kolla att filen finns redan innan den 
försöker ladda upp den, läsa allt som finns där eller inte finns där om det finns null eller blanksteg.
Och Claude mena på att det finns kod som löser alla tre problemen. Så med File.Exists löste även kraschen som uppstod 
när filen saknades. IsNullOrWhiteSpace hoppar över tomma rader, jag såg att File.ReadAllText inte går att använda med arrey 
så bytte till File.ReadAllLine den hanterar oxå \n o \r\n så \r inte hamnar i varans namn.


så det har jag ändrat under Load.



 

