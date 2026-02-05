# Memory Game (Console, C#)

A console-based **Memory Game** built in **C#/.NET**. Play **Human vs Human** or **Human vs Computer** and try to find all matching pairs.

## Download (Published Build)
Download the latest version from GitHub Releases:
https://github.com/dorhaboosha/Memory-Game/releases/latest

### How to run the published version
1. Download the `.zip` file from the latest release
2. Extract the zip
3. Run the `.exe`

## Features
- Human vs Human / Human vs Computer
- OOP-based design (Board, Player, MemoryCard, GameRules, GameManager)
- Input validation with clear error messages
- Score tracking and turn-based gameplay
- Console screen is cleared before each board redraw

## How to Run (From Source)
1. Clone the repository:
   ```bash
   git clone https://github.com/dorhaboosha/Memory-Game.git
   ```
2. Open `MemoryGame.sln` in **Visual Studio**
3. Run the project (**Ctrl + F5**) or (**F5**) to debug

## Game Flow (Based on Assignment Instructions)
- **Player names:** no spaces, max **20** characters.
- **Opponent:** choose to play against the **computer** or another **human**.
- **Board size:** choose a board between **4×4** and **6×6**. The board must have an **even** number of cells (e.g., **5×5 is illegal**).
- **Each turn:** the player selects a cell to reveal, then selects a second cell to try to find the matching pair.
- **Match:** the pair stays revealed until the end of the game, the player gains **1 point**, and keeps the turn.
- **No match:** the two cards remain revealed for about **2 seconds**, then hide again and the turn passes to the opponent.
- **Game over:** when all pairs are revealed, the winner (highest score) is declared.
- **Play again:** after the game ends, the user can start a new round or quit.

### Quitting
From the move-selection stage, the user can quit at any time by typing **`Q`**.

## Input Validation
The game validates each input and prompts the user until a legal move is entered. It differentiates between:
- **Syntax-invalid** inputs (e.g., typing a number instead of a letter)
- **Logical-invalid** inputs (e.g., selecting an out-of-range column like `Y`, or choosing a non-empty cell)

## Project Structure
- `Program.cs` — Entry point and game startup
- `GameManager.cs` — Main game flow (turns, mode handling, win condition)
- `GameRules.cs` — Validations and rules (moves, limits, etc.)
- `Board.cs` — Board state and operations
- `MemoryCard.cs` — Card model (value, reveal/match state)
- `Player.cs` — Player data (name, score, type)
