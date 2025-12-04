# PROJECT: SILENTIUM - Game Script & Design Document

**Genre:** Atmospheric Puzzle Horror
**Thema:** Isolatie, Vertrouwen, De "Unreliable Narrator"

---

## INTRODUCTIE: DE ONTWAKING

**Cutscene / Opening:**
De kou was het eerste dat hij voelde. Geen gewone kou, maar een diepe, chemische kilte die tot in zijn botten trok. **Subject 42** (de speler) hapte naar adem en viel uit de opengebroken cilinder. Hij landde op zijn knieën in een plas stroperige, groene vloeistof. Hij hoestte, zijn longen brandden.

De ruimte was groot en donker, verlicht door het ritmische, rode knipperen van een noodalarm. Om hem heen stonden tientallen andere glazen buizen. Leeg. Op de vloer lag geen tapijt, maar stalen roosters. En iets verderop, badend in het licht van een flikkerende monitor, lag een lijk. Een man in een witte jas, zijn gezicht vertrokken in een eeuwige schreeuw, zijn hand strak geklemd om een kleine zwarte doos.

**Actie:** 42 kroop ernaartoe. Hij wrikte de doos los. Een **Walkie-Talkie**. Hij drukte op de knop. Alleen ruis.
Toen, dwars door de statische sneeuw heen, een stem. Zacht, beschaafd, maar trillend van uitputting.

> **MARCUS (Via Radio):** "Hé? Is daar iemand? Oh, god... ik zag een signaal op het bord. Een actieve hartslag in Sector Boven. Leef je?"

42 probeert te praten, maar er komt slechts een droge kraak uit zijn keel. De speler klikt twee keer met de zendknop.

> **MARCUS:** "Goddank. Mijn naam is Marcus. Subject 41. Ik... ik zit vast, helemaal beneden in de Containment Facility. Ze hebben ons achtergelaten, vriend. Ze hebben de boel op slot gegooid en ons laten stikken. Ik heb je nodig. Ik kan niet lopen... mijn benen... het experiment..."

De stem klinkt menselijk. Kwetsbaar. 42 staat op. Hij moet hier weg. Hij moet Marcus helpen.

---

## LEVEL 1: ADMINISTRATIE & OBSERVATIE

**Sfeer:** De deur siste open. De gang erachter was een ravage. Dossierkasten waren omgevallen, papieren dwarrelden als droge bladeren door de tocht. Maar wat 42 de rillingen bezorgde, waren de sleepsporen. Brede, donkere vegen bloed die niet naar de uitgang leidden, maar naar de ventilatieroosters.

> **MARCUS:** "Goed zo. Je bent in de Administratie Vleugel. Het is daar stil, toch? Ik haat de stilte. Hier beneden hoor ik... dingen. In de muren."

### [GAMEPLAY: PUZZEL 1 - DE OBSERVATIE (Tutorial)]
* **Locatie:** Kantoor van de Hoofdadministrateur.
* **Doel:** De beveiligde deur naar de gang openen.
* **Probleem:** De deur is elektronisch vergrendeld en vraagt om een 4-cijferige code.
* **Omgevingshints:**
    1.  Op het bureau ligt een kalender. De datum van vandaag is omcirkeld met de tekst: *"Jubileum Dr. Aris"*.
    2.  De speler zoekt in de rondslingerende dossiers naar het personeelsbestand en vindt de startdatum van Dr. Aris: **1984**.
* **Oplossing:** Voer code `1984` in op het keypad.
* **Lore Note (Post-it op scherm):** *"Niet vergeten: Subject 41 reageert agressief op fel licht. Houd de lichten gedimd in de lagere sectoren."*

---

## LEVEL 2: DE BIOLOGISCHE LABORATORIA

**Verhaal:**
42 liep door de verlaten kantoren en kwam bij een splitsing. De weg naar de lift werd geblokkeerd door een walgelijke barrière: een dikke muur van grijs, pulserend vlees dat de hele gang had dichtgegroeid. Het leek te ademen. Het rook naar ammoniak en bedorven vlees.

> **MARCUS:** "Ik zie het op de camera's... Bah. De organische uitbraak. Het experiment is... geëvolueerd. Je kunt daar niet doorheen hakken. Je moet het wegbranden. Er is een chemisch lab naast de deur. Maak iets sterks."

### [GAMEPLAY: PUZZEL 2 - HET MENGSEL (Makkelijk)]
* **Locatie:** Chemisch Laboratorium.
* **Doel:** Een agressief zuur mengen om de organische blokkade op te lossen.
* **Probleem:** De automatische dispenser is stuk. De speler moet handmatig chemicaliën mengen via buizen en kranen.
* **Omgevingshints:**
    * Er staan drie grote tanks: **Rood** (Zwaveldioxide), **Blauw** (Water) en **Geel** (Onbekend extract).
    * **Lab Notitie:** *"Formule X-7 is in staat om biologisch weefsel direct te necrificeren. Let op: Meng nooit Rood direct met Geel, dat veroorzaakt een explosie. Gebruik Blauw altijd als stabilisator in het begin."*
* **Oplossing:** Draai de kranen in de juiste volgorde open:
    1.  **Blauw** (Basis)
    2.  **Rood** (Mengt tot Paars)
    3.  **Geel** (Reactie tot Zuur)
* **Horror Event:** Terwijl de speler bezig is, tikt er iets tegen het raam van het lab. Als de speler kijkt, is er niets, alleen een grote, vettige afdruk van een hand... met zes vingers.

---

## LEVEL 3: ONDERHOUD & STROOMVOORZIENING

**Verhaal:**
Het zuur brandde de weg vrij. 42 bereikte de goederenlift en drukte op de knop. Dood.

> **MARCUS:** "De stroom... Natuurlijk hebben ze de hoofdstroom afgesloten. Ze wilden zeker weten dat niets uit de kelder naar boven kon komen. Je moet naar de Maintenance Room. Het is een doolhof van zekeringen. Pas op, 42. Ik hoor voetstappen op jouw verdieping. En het zijn geen menselijke voeten."

42 liep de donkere onderhoudsruimte in. Het was hier benauwd. In het midden stond een gigantische generator, maar het bedieningspaneel was een chaos.

### [GAMEPLAY: PUZZEL 3 - HET CIRCUIT (Gemiddeld)]
* **Locatie:** Maintenance Room.
* **Doel:** De generator herstarten zonder kortsluiting te veroorzaken.
* **Probleem:** Er is beperkte stroomcapaciteit. De speler moet stroom 'rerouten' van niet-essentiële systemen naar de Lift.
* **Mechanic:**
    * Je hebt **10 eenheden** stroom.
    * De Lift vereist **6 eenheden**.
    * De Deurvergrendeling vereist **4 eenheden**.
    * De Verlichting verbruikt **2 eenheden**.
* **Oplossing:** Om de lift (6) en de deur (4) tegelijk te laten werken, moet de speler de **Verlichting UITzetten**. De kamer wordt pikkedonker.
* **Horror Event:** Zodra het licht uitgaat en de liftmotor begint te zoemen, hoort de speler een zware ademhaling vlak achter zijn oor. 
    > **MARCUS (Schreeuwend):** "Ren naar de lift! NU!"

---

## LEVEL 4: SECURITY HUB (DE ONTHULLING)

**Verhaal:**
De lift daalde af, dieper de aarde in. Niveau -1... Niveau -2... Niveau -3. De lift stopte met een schok. **SECURITY HUB**.

> **MARCUS:** "We zijn er bijna. Ik zit één verdieping lager. In Containment. Maar de lift gaat niet verder zonder een stem-autorisatie. De hoofdonderzoeker, Dr. Kempler... die klootzak heeft het systeem vergrendeld op zijn eigen stemfrequentie."

42 liep de bunker binnen. Op de schermen zag hij de gangen waar hij net was geweest. Ze waren leeg. Er was geen monster achter hem aan gerend. Had hij het zich verbeeld? Op de vloer lag het lichaam van Dr. Kempler. Zijn keel was opengesneden.

> **MARCUS:** "Hij kan niet meer praten. Maar hij heeft genoeg geluidsopnames achtergelaten. Gebruik de console. Maak de zin. Laat me vrij."

### [GAMEPLAY: PUZZEL 4 - DE STEM (Moeilijk)]
* **Locatie:** Security Console.
* **Doel:** Een voice-lock openen door audiofragmenten te manipuleren.
* **Probleem:** De computer vraagt om de passphrase: *"INITIATE LOCKDOWN OVERRIDE PROTOCOL ALPHA"*.
* **Actie:** De speler moet in de database zoeken naar fragmenten van Dr. Kempler en deze knippen/plakken.
* **De Twist (Lore):** Tijdens het zoeken vindt de speler een bestand genaamd `Observation_41.wav`.
    * *Audio afspelen:* Een grommend, nat geluid dat langzaam verandert in een menselijke stem. Het zegt: *"Hallo? Is daar iemand? Mijn naam is Marcus."*
    * *Realisatie:* Het is exact de zin die Marcus tegen jou zei aan het begin van de game. Hij oefende.

---

## LEVEL 5: DE KERN (CONTAINMENT)

**Verhaal:**
De computer piepte: *"Voice Pattern Accepted."* 42 staarde naar het scherm. De rillingen liepen over zijn rug.
*"Je... je hebt geoefend,"* fluisterde 42 tegen de walkie-talkie.

Stilte.

Toen sprak Marcus. De stem was helder, luid, en kwam niet meer uit de walkie-talkie, maar uit de speakers van het hele gebouw.

> **MARCUS (Via Intercom):** "Ik leer snel, broertje. Kom naar beneden. Tijd voor de familiereünie."

De laatste deuren openden. 42 daalde af naar de **Containment Core**.
In het midden van een gigantische koepelzaal hing een glazen kooi. Binnenin was geen man in een rolstoel. Binnenin hing een nachtmerrie. Een massa van pezen, tanden en zwarte ogen, opgehangen in een web van organisch materiaal.

Het wezen drukte een bleke, misvormde hand tegen het glas. De intercom in de kooi stond aan.

> **HET MONSTER:** "Ik heb geen benen meer om te lopen. Daarom heb ik de jouwe nodig. Open de kooi, 42."

### [GAMEPLAY: PUZZEL 5 - DE KEUZE (Finale)]
Voor 42 staat een console met twee opties. Kabels lopen naar twee systemen.

**Optie A: Release (Bad Ending)**
* De speler drukt simpelweg op de knop "OPEN GATES".
* **Gevolg:** Het monster wordt vrijgelaten. Het doodt de speler off-screen en ontsnapt naar de bewoonde wereld.

**Optie B: Purge (True Ending)**
* De kabels voor de verbrandingsoven (Purge) zijn doorgebrand.
* **Actie:** De speler opent zijn inventory. Selecteert de **Walkie-Talkie**. Kiest **"Dismantle"**.
* De speler gebruikt de batterij van de walkie-talkie om het circuit van de oven te sluiten.

**Conclusie (Bij Optie B):**
Het monster krijst. Niet met de stem van Marcus, maar met zijn eigen geluid – een oerend hard gepiep. 42 drukt op de knop. Vuur vult de kooi.
Terwijl het monster brandt, hoort 42 via de intercom nog één keer de stem, smekend als een klein kind:

> **MONSTER:** "Niet doen... het is donker... ik ben bang..."

42 draait zich om. De liftdeuren achter hem gaan open naar het zonlicht. Hij stapt in, alleen.

**EINDE**
