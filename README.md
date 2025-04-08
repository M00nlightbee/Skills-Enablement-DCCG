<a id="readme-top"></a>

<div align="center">
  <a href="https://github.com/M00nlightbee/Skills-Enablement-DCCG">
    <img src="logo.png" alt="Logo" width="80" height="80">
  </a>
  <h3 align="center">Skills Enablement Digital Collectible Card Game</h3>
  <p align="center">
    A strategic card game to make learning engaging and fun! <br />
    <strong>Built with Unity • Backed by IBM SkillsBuild</strong>
    <br/>
    <br/>
    ▶️ <a href="https://blessingu.itch.io/ibmskillbuilddccg" target="_blank"><strong>Play the Game on Itch.io</strong></a>
  </p>
</div>

---

## Game Play Tutorial

https://github.com/user-attachments/assets/0017f195-46c4-4b11-9704-1746bf025d84

---

## 📌 Table of Contents

- [About the Project](https://github.com/M00nlightbee/Skills-Enablement-DCCG?tab=readme-ov-file#-about-the-project)
- [Key Features](https://github.com/M00nlightbee/Skills-Enablement-DCCG?tab=readme-ov-file#-key-features)
- [Development Process](https://github.com/M00nlightbee/Skills-Enablement-DCCG?tab=readme-ov-file#-development-process)
- [Wireframes & Design](https://github.com/M00nlightbee/Skills-Enablement-DCCG?tab=readme-ov-file#-wireframes--design)
- [Technologies Used](https://github.com/M00nlightbee/Skills-Enablement-DCCG?tab=readme-ov-file#-technologies-used)
- [Game Flow](https://github.com/M00nlightbee/Skills-Enablement-DCCG?tab=readme-ov-file#-game-flow)
- [Card Anatomy & Rarity](https://github.com/M00nlightbee/Skills-Enablement-DCCG?tab=readme-ov-file#-card-anatomy--rarity)
- [Setup & Contribution](https://github.com/M00nlightbee/Skills-Enablement-DCCG?tab=readme-ov-file#%EF%B8%8F-setup--contribution)
- [Special Thanks]()
- [License](https://github.com/M00nlightbee/Skills-Enablement-DCCG?tab=License-1-ov-file)

---
<div align="center">
  <img src="GamePlay.png" alt="Card Design" width="700"height="500"/>
</div>

## 🎯 About the Project

The **Skills Enablement Digital Collectible Card Game (DCCG)** is a Unity-based game that makes learning **AI, Cybersecurity, Cloud, and Data Science** fun through cards and quizzes. 🎓

Players:
- Battle using collectible cards ⚔️
- Earn cards by answering **IBM SkillsBuild** questions correctly 💡
- Level up through knowledge 📚

👉 [Project Overview](https://docs.google.com/document/d/14BMKRUqbnrAMTLlWKWsAzZeuF36g5QbhpxTpNvQdBx4/edit?usp=sharing)

<p align="right">
  <a href="https://github.com/M00nlightbee/Skills-Enablement-DCCG/blob/main/README.md#-table-of-contents">🔼 Table of Contents</a>
</p>

---

## 🔑 Key Features

- 🎴 Turn-based card battles (like Hearthstone or Star Trek Adversaries)
- 🧠 Quiz cards test player knowledge
- ⚡ Earn mana and play special cards
- 🎨 Visual feedback like glows and animations
- 🔁 Drag-and-drop mechanics
- 📊 Health & mana UI with real-time updates

<p align="right">
  <a href="https://github.com/M00nlightbee/Skills-Enablement-DCCG/blob/main/README.md#-table-of-contents">🔼 Table of Contents</a>
</p>

---

## 🔄 Development Process

We used Agile game dev practices:
- 📝 User stories and acceptance tests
- 🧪 Playtesting and feedback loops
- 🔄 Iterative Unity builds and sprints
- 📋 Clean, modular code with `GameManager`, `Card`, `DeckManager`, etc.

<p align="right">
  <a href="https://github.com/M00nlightbee/Skills-Enablement-DCCG/blob/main/README.md#-table-of-contents">🔼 Table of Contents</a>
</p>

---

## 🧪 Wireframes & Design

We planned every screen first with wireframes:
- 🎮 Menu & Game Board
- 🃏 Deck Builder and Card Info
- 📊 Victory/Defeat screens
- ❓ Integrated Quiz Scene

▶️ [View Figma Wireframes](https://www.figma.com/proto/6WOWbbwzxDDswA4TzgWmtG/Game-Prototype?page-id=0%3A1&node-id=41-977&p=f&viewport=214%2C-45%2C0.13&scaling=contain&content-scaling=fixed&starting-point-node-id=41%3A977)

<p align="right">
  <a href="https://github.com/M00nlightbee/Skills-Enablement-DCCG/blob/main/README.md#-table-of-contents">🔼 Table of Contents</a>
</p>

---

## 🧰 Technologies Used

- 🎮 **Unity Engine** – game mechanics and UI
- 🖥️ **Visual Studio** – C# scripting
- ☁️ **IBM SkillsBuild** – question content
- 🧪 **Unity Test Framework** – unit tests
- 🖼️ **Figma** – UI/UX design
- 🌐 **GitHub** – version control and project tracking

<p align="right">
  <a href="https://github.com/M00nlightbee/Skills-Enablement-DCCG/blob/main/README.md#-table-of-contents">🔼 Table of Contents</a>
</p>

---

## 🎮 Game Flow

### 🧍 Player Journey
1. Load the game and pick a category (AI, Cybersecurity, etc.)
2. Answer a quiz to earn new cards 💬
3. Use cards to battle AI opponents 🤖
4. Keep winning and collecting for a stronger deck 🏆

### 🎲 Core Mechanics
- Turn-based gameplay
- Drag to play cards: Attack, Heal, or open a Quiz
- Health system for player/opponent
- Mana required to play stronger cards
- Cards are rewarded based on quiz performance

<p align="right">
  <a href="https://github.com/M00nlightbee/Skills-Enablement-DCCG/blob/main/README.md#-table-of-contents">🔼 Table of Contents</a>
</p>

---

## 🃏 Card Anatomy & Rarity

Here’s what each part of a card means:

<div align="center">
  <img src="CardAnatomy.PNG" alt="Card Design" width="400"/>
</div>

### 🔍 Card Breakdown

- **Mana Cost (Top Left)**  
  Shows the amount of mana required to play card

- **Effect Value (Bottom Left)**  
  Effect can be attack/heal/mana value:
    - for attack the number of damage to be dealt
    - for heal number of health you can gain and
    - for mana number of mana you can gain for each question you get right(each correct answer gives 1 mana)

- **Card Name (Top Center)**  
  Every card has a cool name — like “Cyber Firewall” or “AI Strike”.

- **Card Image (Middle)**  
  The visual of the card — sometimes a hero, a shield, or a starship.

- **Card Effect Description (Bottom Center)**  
  Shows what the card does, e.g "Deal 3 Damage" ,"“Heal 2 Health" or "Gain 2 Mana".

- **Card Action Type (Sheild Icon on the image)**  
  What this card does:
  - 🔴 **Red** = Attack
  - 🟢 **Green** = Heal
  - 🔵 **Blue** = Answer a Quiz Question

- **Card Rarity (Top Right Icon Color)**  
  How rare the card is:
  - ⚪ **White** – Common  
  - 🟢 **Green** – Uncommon  
  - 🔵 **Blue** – Rare  
  - 🟣 **Purple** – Epic  
  - 🟡 **Gold** – Legendary

<p align="right">
  <a href="https://github.com/M00nlightbee/Skills-Enablement-DCCG/blob/main/README.md#-table-of-contents">🔼 Table of Contents</a>
</p>

---

## 🛠️ Setup & Contribution

```bash
# Clone the repo
git clone https://github.com/M00nlightbee/Skills-Enablement-DCCG.git

# Open in Unity (version: Unity 6 (2024) 6000.0.37f1 LTS)
# Run the main scene to play
```

<p align="right">
  <a href="https://github.com/M00nlightbee/Skills-Enablement-DCCG/blob/main/README.md#-table-of-contents">🔼 Table of Contents</a>
</p>

