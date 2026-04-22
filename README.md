# SoloSousChef | Unity Technical Document

A modular, highly scalable 3D kitchen simulation built in Unity. This project demonstrates clean architecture, event-driven systems, and data-driven design patterns suitable for a professional production environment.


## 🛠 Technical Architecture
The project follows **SOLID principles** to ensure the codebase remains maintainable and extendable:

* **Modular Interaction System:** Uses an `IInteractable` interface, allowing the player to interact with any station without tight coupling.
* **Data-Driven Ingredients:** `ScriptableObjects` manage ingredient properties, allowing for easy balancing and expansion.
* **Decoupled UI:** A robust `BaseScreenManager` handles state transitions between gameplay, menus, and tutorials.
* **Performance Optimized:** Targeted 60 FPS by utilizing efficient locomotion and minimizing `Update()` overhead.

## 🚀 System Breakdown

| Module | Responsibility | Key Scripts |
| :--- | :--- | :--- |
| **Player** | Locomotion, Input, & Interaction handling. | `PlayerController`, `PlayerLocomotion` |
| **Stations** | Processing logic for cooking/chopping. | `BaseStationHandler`, `CookingStationHandler` |
| **Orders** | Dynamic order generation and score calculation. | `OrderManager` |
| **UI Stack** | Screen management and player feedback. | `BaseScreenManager`, `BaseScreen` |

## ⚙️ Setup & Installation
1. Clone the repository: `git clone https://github.com/anvithaus02/SoloSousChef.git`
2. Open the project in **Unity 2021+**.
3. Load the scene: `Scenes/MainGame`.
4. Press **Play**.

## ⌨️ Controls
* **Movement:** WASD / Arrow Keys
* **Interact:** Action Button (Defined in `ButtonConfigurationSO`)
* **Pause:** Escape Key