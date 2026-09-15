# EinnahmeAusgabe

## Über die App

Web-App zur Verwaltung von Einnahmen und Ausgaben.

### Funktionen

* Einnahmen und Ausgaben verwalten
* Kategorien verwalten
* Kategorien mit Farben
* Suche und Filter
* Sortierung
* Statistiken
* Diagramme

## Technologien

* C#
* ASP.NET Core MVC
* Entity Framework Core
* SQL-Datenbank
* Razor / HTML
* CSS
* JavaScript / Chart.js

## Aufbau

**Model**

* Daten der App
* `Transaktion`
* `Kategorie`

**View**

* Anzeige der Daten

**Controller**

* Verarbeitung der Anfragen

## Datenbank

**Kategorie 1 : n Transaktion**

* Eine Kategorie → 0 bis viele Transaktionen
* Eine Transaktion → 0 oder 1 Kategorie
* `KategorieId` → Fremdschlüssel
* KategorieId kann null sein → Keine Kategorie.

Beim Löschen einer Kategorie bleiben die Transaktionen erhalten und bekommen „Keine Kategorie“.

## Statistik

* Einnahmen und Ausgaben
* Top 5
* Summe
* Durchschnitt
* Diagramme
