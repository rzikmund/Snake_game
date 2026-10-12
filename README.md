<div align="center">

```
 ____  _   _    _    _  _______
/ ___|| \ | |  / \  | |/ / ____|
\___ \|  \| | / _ \ | ' /|  _|
 ___) | |\  |/ ___ \| . \| |___
|____/|_| \_/_/   \_\_|\_\_____|
```

**The classic Snake game, right in your terminal, written in C#**
<img width="1107" height="576" alt="Screenshot 2026-10-11 175041" src="https://github.com/user-attachments/assets/d6e893bc-1e19-4136-a34e-31ab4d3e7c50" />
<img width="1107" height="591" alt="Screenshot 2026-10-11 174931" src="https://github.com/user-attachments/assets/31bf6dca-3ba6-415b-a496-70a54dd70647" />


</div>

---

## About

This is the classic **Snake** game built as a C# console application. You steer the snake, collect apples and try to eat every last one of them. The whole game runs in the terminal.

## Controls

| Action      | Arrow keys | Keys |
|:------------|:----------:|:----:|
| Up          | `↑`        | `W`  |
| Down        | `↓`        | `S`  |
| Left        | `←`        | `A`  |
| Right       | `→`        | `D`  |

## Goal

Eat all the apples on the board. Once the last apple is gone, the game ends.

## Getting Started

**Requirements:** [.NET SDK](https://dotnet.microsoft.com/download)

```bash
git clone https://github.com/rzikmund/Snake_game.git
cd Snake_game
dotnet run
```

> **Note:** Sounds played with `Console.Beep` at a set frequency only work on Windows. On other systems the game may run without sound.

---

## What I Learned

This is my **second project in C#**, so the code is still a bit rough around the edges. Building it helped me improve in a few areas:

- **Sound in the console** – playing tones with `Console.Beep`
- **Arrays** – a better understanding of how to work with them and use them to store game state
- **ASCII art** – what it is and how to build it into a console application

---

<div align="center">

Made as a learning project while studying C#

</div>
