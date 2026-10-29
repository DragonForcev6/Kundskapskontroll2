# Inköpslistan

## Del 1 – Felrapport

### Fel 1: Programmet kraschar när items.txt saknas
**Vad hände:** När jag döpte om items.txt o startade programmet kraschade det direkt.
**Varför:** Load försökte läsa filen med File.ReadAllText utan att kolla om filen fanns, då kommer en FileNotFoundException.
**Lösning:** Jag tänkte att programmet borde kolla att filen finns innan den försöker läsa den. Så jag la till `if 
(!File.Exists(path)) return;` först i Load. Finns inte filen händer inget o programmet startar med en tom lista.

### Fel 2: Programmet kraschar på en tom rad i filen
**Vad hände:** Programmet kraschade när det startade fast items.txt fanns, med IndexOutOfRangeException.
**Varför:** Save skriver en radbrytning efter sista raden, så när Load delade upp texten blev sista raden tom. 
En tom rad har inget semikolon så parts får bara en del o parts[1] finns inte.
**Lösning:** Jag la till `if (string.IsNullOrWhiteSpace(line)) continue;` i Load. Om raden är tom eller bara har 
blanksteg hoppar den över raden.

### Fel 3: Programmet kraschar om man skriver bokstäver i stället för siffror
**Vad hände:** Skrev man bokstäver eller symboler i menyn, i priset eller i numret kraschade programmet.
**Varför:** int.Parse kraschar programmet om det man skriver inte är ett tal.
**Lösning:** Jag satte choice, price o number i if-satser o bytte Parse mot TryParse. TryParse kraschar inte, den ger 
false om det inte gick o då får användaren ett felmeddelande. I choice 1 la jag också till `string.IsNullOrWhiteSpace(name)` 
som fångar upp tomt namn o blanksteg, o `price <= 0` så att man inte kan skriva in noll eller negativt pris.

### Fel 4: Programmet kraschar när man tar bort en vara som inte finns
**Vad hände:** RemoveAt tog emot vilken siffra som helst. Skrev man t.ex. 0 eller 99 kraschade programmet.
**Varför:** Numret skickades direkt vidare till listan utan att någon kollade att det fanns en vara med det numret.
**Lösning:** Jag ändrade RemoveAt så den returnerar bool. Först kollar den om listan är tom, sen kollar den att numret 
är mellan 1 o antal varor i listan (`number < 1 || number > items.Count`). Är det fel får användaren ett felmeddelande 
o den returnerar false. När varan tas bort får användaren ett meddelande att varan tagits bort.

### Fel 5: Totalsumman blir fel
**Vad hände:** När jag räknade efter för hand stämde inte totalsumman, den var för låg.
**Varför:** Loopen i Total() började på i = 1, men listan börjar på index 0. Så första varan blev aldrig medräknad.
**Lösning:** Jag ändrade så loopen börjar på i = 0.

### Fel 6: Save döljer att något gick fel
**Vad hände:** Programmet skrev ut att listan var sparad oavsett om den blev sparad eller inte.
**Varför:** catch var tom så om något gick fel fick användaren inte veta det, o meddelandet "Listan 
är sparad" stod efter try/catch så det kom alltid ut.
**Lösning:** Jag ändrade Save så den returnerar bool. Koden som kan misslyckas ligger i try, o om 
något går fel hoppar programmet till rätt catch. Jag fångar UnauthorizedAccessException 
(filen är skrivskyddad eller man saknar behörighet), DirectoryNotFoundException (mappen finns inte) 
o IOException (t.ex. filen används i ett annat program eller disken är full). Varje catch skriver ut 
ett felmeddelande o returnerar false. Meddelandet att listan är sparad kommer bara ut om allt gick bra.

**Ordningen på catch:** Koden läser catch uppifrån o ner o tar den första som passar. 
DirectoryNotFoundException ärver från IOException, så om IOException står först passar den alltid o 
blocket under kommer aldrig i tur. Jag testade att flytta IOException först o då blev 
DirectoryNotFoundException rödmarkerad med ett meddelande om att den övre redan fångade allt. 
Så den specifika måste stå före den allmänna.

### Extra: Sökningen hittade inte varor som fanns i listan
**Varför:** Save använde \r\n men Load delade bara på \n. Då hängde \r med i varans namn, t.ex. blev 
det Mjölk\r o då kunde inte Mjölk hittas när man sökte.
**Lösning:** Jag bytte till File.ReadAllLines i Load. ReadAllText ger hela filen som en enda sträng, 
men ReadAllLines ger en array där varje rad är ett eget element, o den hanterar både \n o \r\n så \r inte 
hamnar i varans namn. I Save använder jag File.WriteAllLines.

## Del 2 – Designval

### Hur Add säger nej när taket spräcks
Jag la till MaxTotal = 5000 i ShoppingList o ändrade Add så den returnerar bool. Add kollar om 
`Total() + item.Price > MaxTotal`. Blir det över taket läggs varan inte till o Add returnerar false, 
annars läggs den till o returnerar true. Om totalen blir precis 5000 kr är det okej.

**Varför jag valde bool:** Att man försöker lägga till något som blir för dyrt är något som kan hända 
när man använder programmet helt normalt, det är inget fel i programmet. Det är som i bankexemplet när 
saldot inte räcker. Jag valde också bool för att det stämmer med hur RemoveAt o Save fungerar. Program.cs 
behöver bara en if-sats: blir det true skrivs det ut att varan lades till, blir det false får användaren 
ett felmeddelande att max taket är nått.

### Varför Item kastar undantag
Item kastar ArgumentException om namnet är tomt o ArgumentOutOfRangeException om priset är 0 eller 
negativt. ArgumentOutOfRangeException ärver från ArgumentException, så samma catch fångar båda, men 
anroparen kan skilja fallen åt om det behövs. En konstruktor kan inte 
returnera false så därför kastar den ett undantag. Program.cs kollar redan namn o pris innan varan 
skapas, men Load skapar varor direkt från filen utan att kolla. Därför la jag try/catch med 
ArgumentException i Load, så om någon rad i filen är fel hoppas den över med ett meddelande i stället 
för att programmet kraschar. Jag testade med raden -5;Test i filen.
Program.cs har också en try/catch runt new Item(...) så att användaren får Items felmeddelande om något 
ändå slinker igenom. I Load använder jag TryParse och kollar att raden har ett semikolon, så att trasiga 
rader hoppas över i stället för att krascha. Load använder också Add, så taket gäller även när filen läses in.

## Andra ändringar
- items o path är readonly. Det gör att man inte kan råka byta ut listan eller path mot något 
annat efter konstruktorn, men man kan fortfarande lägga till o ta bort varor i listan.
- ShoppingList o Item är public. När jag gjorde ShoppingList public fick jag kompileringsfelet 
"Inconsistent accessibility" eftersom Find returnerar ett Item, då måste Item också vara public.
- I csproj kopieras items.txt till bin\Debug\net10.0, o det är den filen programmet läser 
o sparar i.

## Klassdiagram

```
+------------------------------+
| Program                      |
+------------------------------+
| menyloop (val 1–5)           |
+------------------------------+
               |
               | använder
               v
+------------------------------+
| ShoppingList                 |
+------------------------------+
| + MaxTotal : int = 5000      |
| + Count : int  (read-only)   |
| - items : List<Item>         |
| - path : string              |
+------------------------------+
| + Add(Item) : bool           |
| + RemoveAt(int) : bool       |
| + Total() : int              |
| + Find(string) : Item        |
| + Print() : void             |
| + Save() : bool              |
| + Load() : void              |
+------------------------------+
               |
               | 1 har 0..*
               v
+------------------------------+
| Item                         |
+------------------------------+
| + Name : string  (read-only) |
| + Price : int    (read-only) |
+------------------------------+
| + Item(string, int)          |
|   kastar ArgumentException   |
|   o ArgumentOutOfRange-      |
|   Exception                  |
| + ToString() : string        |
+------------------------------+
```

## Hjälp
Jag har använt Claude (AI) som handledare under uppgiften. Den har gett mig ledtrådar 
o frågor om var felen kan sitta o hur jag kan tänka, o hjälpt mig att sortera o formulera 
README utifrån mina egna anteckningar. Ändringarna i koden har jag gjort o testat själv.

## Omtenta 1
- Item har bara `{ get; }` på Name o Price. Innan hade de `{ get; set; }`, då kunde man skriva `ost.Price = -5` efter konstruktorn o gå förbi kontrollen. Nu kan de bara sättas i konstruktorn, så en Item kan aldrig få ogiltiga värden. Jag testade med `ost.Price = -5` o då blev raden röd.
- Skriver man ett tal som inte finns i menyn, t.ex. 7, får man nu ett felmeddelande. Innan händeinget. Jag la till ett `else` sist i if-kedjan som fångar alla tal som inte är 1–5.
- Load använder `Split(';', 2)` så den bara delar vid första semikolonet. Innan blev en vara som hette `Ost;Brie` bara `Ost` när filen lästes in, eftersom resten hamnade i parts[2].
- Find använder `Contains` med `StringComparison.OrdinalIgnoreCase`. Då spelar stora o små bokstäver ingen roll o man kan söka på en del av namnet, t.ex. `brie` hittar `Ost;Brie`. Find returnerar den första varan som matchar. Program.cs kollar att sökningen inte är tom, annars skulle `""` matcha första varan i listan.
- RemoveAt skriver inte ut något längre, den returnerar bara true eller false precis som Add. Meddelandena ligger i Program.cs. ShoppingList har fått `Count` så Program.cs kan se om listan är tom eller om numret var fel, utan att kunna ändra i listan. Save o Load skriver fortfarande ut själva eftersom de har flera olika felmeddelanden, det valde jag att låta vara.