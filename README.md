# ShopCompare

Projekt stanowi część pracy magisterskiej pt.:

**„Porównanie architektury mikroserwisowej z wykorzystaniem .NET Aspire i architektury monolitycznej”**

Celem projektu jest implementacja tej samej aplikacji w dwóch wariantach architektonicznych:
- modularnego monolitu
- architektury mikroserwisowej

oraz przeprowadzenie ich porównania pod kątem wydajności, złożoności oraz skalowalności.

---

## 📌 Opis aplikacji

Aplikacja jest uproszczonym systemem e-commerce, umożliwiającym realizację podstawowego procesu zakupowego:

1. przeglądanie katalogu produktów
2. dodawanie produktów do koszyka
3. składanie zamówienia
4. rezerwacja stanów magazynowych
5. przetwarzanie płatności
6. generowanie powiadomień

System został zaprojektowany w sposób umożliwiający odwzorowanie rzeczywistych scenariuszy biznesowych oraz przeprowadzanie testów wydajnościowych.

---

## 🧱 Architektura

Projekt zawiera dwa warianty architektoniczne:

### 1. Modularny monolit

- podział na moduły odpowiadające obszarom biznesowym
- komunikacja synchroniczna (in-process)
- wspólna baza danych (logiczna separacja danych)
- testy architektury kontrolujące zależności

### 2. Mikroserwisy

- każdy moduł jako niezależna usługa
- komunikacja między usługami (HTTP)
- możliwość niezależnego skalowania
- wykorzystanie .NET Aspire do orkiestracji środowiska

---

## 🧩 Moduły systemu

System składa się z następujących modułów:

- **Catalog** – zarządzanie produktami
- **Inventory** – zarządzanie stanami magazynowymi
- **Cart** – koszyk użytkownika
- **Orders** – obsługa zamówień (orchestrator)
- **Payments** – przetwarzanie płatności (symulacja IO-bound)
- **Notifications** – system powiadomień

---

## ⚙️ Technologie

- .NET (ASP.NET Core)
- Entity Framework Core
- PostgreSQL
- .NET Aspire
- Docker
- xUnit / FluentAssertions (testy)

---

## 📊 Benchmarki

Projekt został przygotowany do przeprowadzania testów wydajnościowych, obejmujących m.in.:

- pełny flow zamówienia (end-to-end)
- operacje odczytu (read-heavy)
- operacje IO-bound (płatności)
- testy współbieżności

---

## 🎓 Cel projektu

Projekt ma na celu praktyczne porównanie dwóch podejść architektonicznych poprzez:

- analizę różnic implementacyjnych
- pomiar wydajności
- ocenę skalowalności
- identyfikację kompromisów projektowych
