## Skład grupy
Kamil Husak, Gabriela Maślanka, Karolina Gajewska

### Struktura projektu
---
#### Warstwa Aplikacji

Zawiera interfejsy serwisów, serwisy dla wszystkich klas, mappery oraz validatory.

#### Warstwa Infrastruktury

Zawiera pliki konfiguracyjne dla bazy danych, repozytoria, data seeder oraz klasy potrzebne do uruchomienia bazy danych.

#### Warstwa Logiki Domenowej

Zawiera następujące foldery:

- **Contracts**: Interfejsy repozytoriów.
- **Exceptions**: Wyjątki używane podczas obsługi bazy danych.
- **Helpers**: Klasy pomocnicze do stronicowania, sortowania i filtrowania.
- **Models**: Modele używane podczas tworzenia aplikacji.

#### Warstwa Prezentacji WebAPI

Zawiera controllery dla każdej z klas oraz exception middleware.

#### Warstwa Prezentacji Blazor Server

Część odpowiedzialna za widok aplikacji od strony administratora.

#### Warstwa Prezentacji Blazor User

Część odpowiedzialna za widok aplikacji od strony użytkownika.

#### Projekt typu SharedKernel

Zawiera obiekty DTO dla każdej klasy.

---

### Dane w bazie danych

W bazie danych zostały zaimplementowane następujące dane:

- 3 filmy
- 3 aktorów
- 3 recenzje
- 3 użytkowników
