

Vad jag vill ändra i Program.cs

int choice = int.Parse(Console.ReadLine()); 
Får programmet att krascha om man inte använder siffror
Sätter hela choice i en if satts och använder TryParse istället för Pars som inte kraschar programet
om man skriver bokstäver eller symboler.
I Choice 1 o Choice 2 sätter Sätter TryParse istället för en Pars i en if sats som kollar så användaren 
skriver siffror,om bokstäver eller symboler används kommer det upp ett fel meddelande.
 if (!int.TryParse(Console.ReadLine(), out int choice)) om användaren skriver siffror eller symboler 
 fångas det upp o de får ett felmeddelande.
 I Choice 1: Lagt till if (string.IsNullOrWhiteSpace(name)) för att fånga upp null, 
 tom sträng "" o blanksteg eller andra osynliga tecken som tabb o radbrytning "  ", 
 vid dessa tillfällen fångas de upp med ett felmeddelande.
 if (!int.TryParse(Console.ReadLine(), out int price) || price <= 0) Kontrollerar att användaren skriver 
 in giltigt pris o inget negativt pris eller noll. Annars får användaren ett felmeddelande.
 list.Add(new Item(name, price)); La till så användaren får upp ett meddelande när varan läggs till.
 Choich 2: Lagt i en if sats där användaren får ett felmeddelande om den inte skriver med sifforor.
 Choice 3: Användaren får ett medelande att Shoppinglistan är sparad.

Claude sa att jag skulle lägga till <ItemGroup> i csproj för att filen items.txt ska ingå i projektet o 
bli kopierad till byggmappen (bin\Debug\net10.0).

ShoppingList.cs
readonly List<Item> items = new List<Item>(); 
readonly string path;
Hindra framtida misstag så man inte råkar skriva över items eller path, 
ändrar till public så Program kan nå båda

File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n"); i save använder \r\n medan 
Load bara använder \n det medför att \r kommer hänga med vissa variablar ibland.
string text = File.ReadAllText(path);
string[] lines = text.Split('\n'); Denna koden gör att problemet uppstår o då varunamnat blir 
Mjölk\r kan inte varan Mjölk hittas.

Jag fick lite tips ifrån Claud att ändra i Load.
jag tänkte att programet borde kolla att filen finns redan innan den 
försöker ladda upp den, läsa allt som finns där eller inte finns där om det finns null eller blanksteg.
Och Claude mena på att det finns kod som löser alla tre problemen. Så med File.Exists löste även 
kraschen som uppstod när filen saknades. IsNullOrWhiteSpace hoppar över tomma rader, jag såg att File.
ReadAllText inte går att använda med arrey så bytte till File.ReadAllLine den hanterar oxå \n o \r\n så \r 
inte hamnar i varans namn.

Save sparar inte men just nu skriver ut att den sparas oavsett.
Så ändrar Save till en public boolian.Frågat Claud som föreslog att lägga 
allt i en try loop där koden som kan misslyckas, om något går fel hoppar programet till
rätt catch. File.WriteAllLines(path, lines); skriver alla raderi filen, 
sparar filen om den inte finns o skriver över den om den finns.
meddelandet "Listan sparades" kommer bara ut om allt gick bra.
Ordningen mellan IOException o DirectoryNotFoundException är viktig i koden om man skriver den som <-- 
jag skrev nu kommer inte koden att läsas uppifrån o ner o ta det som passar bäst, står IOException först 
kommer den alltid att passa bäst o det spesifika blocket under kommer aldrig i tur.

RemovAt tar emot vilken siffra som helst o ger inget felmeddelande när användaren anger ett nummer som 
inte finns i listen.
Har ändrat så RemoveAt har koll på om listan är tom eller användaren knappar in nummer som är stämmer 
överens med antal varor i listan.
 

Item.cs
Får ett kopileringsfel "Inconsistent accessibility" (CS0050/CS0051) enligt Claude är det för att 
Find retunerar en Item Claudes förslag är att jag skriver public class Item.

 

