# AnInvoiceExcercise
ASP.NET Core - COSMOS DB - DDD - DI 
Een uitwerking in ASP.NET Core, waar je volgende technieken/technologieën gebruikt:

- Dependency Injection
- Domain Driven Design
- Azure Cosmos DB (een emulator installeren)
- ASP.NET Core
 
Maak een API service (dus geen front-end, api alleen is voldoende), die het mogelijk stelt om:
4-tal aparte APIs

- Klanten aan te maken
- Een contactgegeven (email/telefoon) toe te wijzen aan een klant
- Een factuur met factuurlijnen te maken
- Een status te updaten van een factuur  
 
Het datamodel:

- Klant:

  - Voornaam
  - Achternaam
  - Adresgegevens
  - 0 of meerdere contactgegevens
    - Type (Email/GSM)
    - Waarde
  
- Factuur:

  - Datum
  - Omschrijving
  - Klant
  - Totaalbedrag
  - 1 of meerdere factuurlijnen
    - Aantal
    - Prijs per eenheid
    - Totaalprijs
