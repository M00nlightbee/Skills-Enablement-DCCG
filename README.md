<a id="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="#">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>
  <h3 align="center">Skills Enablement Digital Collectible Card Game</h3>
  <p align="center">
    A strategic card game to make learning engaging and fun!
    <br />
    <a href="#"><strong>Explore the docs »</strong></a>
    <br />
    <br />
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#about-the-project">About The Project</a></li>
    <li><a href="#key-features">Key Features</a></li>
    <li><a href="#wireframes">Wireframes</a></li>
    <li><a href="#technologies">Technologies</a></li>
    <li><a href="#game-process">Game Process</a></li>
    <li><a href="#contributing-access-project">Contributing / Access Project</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

The **Skills Enablement Digital Collectible Card Game (DCCG)** is designed to enhance learning through **interactive and strategic gameplay**. Players collect, trade, and battle cards by answering **IBM SkillsBuild** questions covering AI, Cybersecurity, Cloud Computing, and Data Science. The game fosters engagement, making skill-building fun and competitive.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Key Features
- 🎮 **Turn-based strategy card battles**.
- 📚 **Earn cards by answering IBM SkillsBuild questions**.
- 🏆 **Progress and rank up through challenges and leaderboards**.
- 🌍 **Single-player functionality with player-AI interaction**.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Wireframes
Game design includes:
- 🎴 **Card Deck & Upgrades**
- 🏁 **Game Board & Turn-based Play**
- 🏆 **Victory & Defeat Screens**

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Technologies Used
- **Game Engine:** Unity
- **Development Environment:** Visual Studio

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Game Process
### User Journey to Play the Game
1. **Register or Sign in**
2. **Display Menu**
3. **Player chooses a badge they want to learn/play**
4. **Choose Play**
   - Player chooses their user Icon
   - Spawn cards from the database for player and enemy

### Display Game Board
- **Player Information**
  - Health
  - Shields
  - Mana
- **Opponent Information**
  - Health
  - Shields
  - Mana
- **Game assigns a random user icon to opponent**
- **Display cards on the board**

### Assign Cards to Player & Opponent
- **Attack, Defend, Multiplier Cards**
  - **[Attack]** - Select opponent or opponent’s cards to attack.
  - **[Defend]** - Select player or player’s cards to defend.
  - **Multiplier Card Clicked** - Outcome may result in a reward.

### Turn Implementation
- Opponent should be able to make moves equivalent to the player.
- Display screen for questions between turns.

### Question-Based Levels
- **Level 1:** Questions from module one.
- **Level 2:** Questions from module two.
- **Level 3:** Questions from module three.
- **Level 4:** Questions from module four.
- **Level 5:** Questions from module five.
- **Level 6:** Questions from module six.

### Additional Features
- **Display timer for each turn**
- **Display number of turns left**
  - Update turns left after a card is played.

### Win Condition
- Game ends when **opponent’s health reaches zero**.
- Game ends when **player’s health reaches zero**.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Contributing / Access Project
1. Fork the Project
2. Create your Feature Branch 
3. Commit your Changes 
4. Push to the Branch 
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## License
Distributed under the MIT License.

<p align="right">(<a href="#readme-top">back to top</a>)</p>
