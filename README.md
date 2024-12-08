# PI3DV-solo

Project Name: Terrorist hunt

Name: Jason Hassan

Student Number: 20233894

Link to Project: [https://github.com/KR3MIT/PI3DV-solo](https://github.com/KR3MIT/PI3DV-solo) 

# Overview of the Game:

The game is a crappy version of either call of duty, or cs:go operations missions. The player is placed in a linear level, with one way forward. The world consists of a series of combat areas, with corridors and platforming connecting them. It is a first-person shooter that uses the mouse to look around, and WASD to move. The goal is to get to the end. It really is not that challenging at all, it’s more of a tech demo. There are enemies, simple goons and a heavy variant, the only difference being the amount of health it has and damage.

The main parts of the game are:

* Playercontroller:   
  * Moves with the keyboard WASD  
  * rotates around with the mouse. only hipfire, like in cs.  
* Limited ammo and health.   
* Enemies:   
  * goon, has a moderate amout of health, ok damage  
  * Heavy goon, large health pool and high damage.  
  * Difficulty, the longer the player is in line of sight, the better the accuracy, this pushes the player to find cover.   
* Epic level with unique scenarios.  
* You only have 1 life, but 130 health. (you cant restart the game, have to alt f4 to quit.)

Game features:

* ok weapon mechanics and effects.  
* a unique experience  
* Unity 6 HDRP visuals.

# How were the Different Parts of the Course Utilized:

The player controller was not made from scratch, it was shamelessly taken from Atil's fpscontroller (see resources.) The weapon mechanics and visuals were all home made, the shooting is raycasts, with the camera as its origin, the weapon stats come from a scriptable object that contains all sorts of information, such as fire rate, damage, weapon world model, and weapon view model. When the player collides with the weapon world model, it is picked up and placed in the player's inventory, a script then takes the weapon data and instantiates the view model for the player to see. The strafing and firing animations are made procedurally using animation curves and vector3 with lerp. When the player drops a weapon, the world model becomes visible and is given a force forward and with a rigidbody it falls to the ground, like when you drop a weapon is CS.

The enemies use AI nav mesh to navigate and move towards the player. The AI’s operate using a state machine, starting with idle, where they just stand still, when the player enters the AI’s sight range they start moving towards the player, then when the player is within firing range, they start attacking, as mentioned before, They don't have perfect accuracy. When the AI has or less than 0 health they enter the dead state where they ragdoll on the ground and stay that way. The spawning of the enemies is based on trigger boxes and waves. So they only spawn when the player enters a certain area. 

All the environment and AI visuals were taken from elsewhere, the environment is from Quixel megascans on Fab.com, and the AI animations and model are from Adobe Mixamo.com.

# Project Parts:

* Scripts:  
  * FPS controller  
  * Player GUI \- handles all player gui such as health and ammo.  
  * Weapon behavior \- handles the weapons behavior, that includes fire rate, damage and mag size etc.  
  * Player Anim Controller \- handles the view model animation, instantiation, and VFXs.  
  * Player Inventory \- handles what weapons the player currently has in them, and on hand.  
  * Player Info \- handles health.  
  * Weapon Item \- makes the weapon pickup-able and contains the weapon data (scriptable object).   
  * Trigger box \- used to trigger the next wave in the spawner manager.  
  * Spawner Manager \- enables the enemies in a specific wave.   
  * Enemy \- handles the enemy movement, state, animations, stats and navigation.   
* Models & Prefabs:  
  * Weapon model made and animated in Blender.   
  * Enemies models and animations from Mixamo.  
  * Environment models from Quixel.  
* Materials:  
  * Unity HDRP/Lit.  
  * Quixel Megascan materials.  
* Scenes:  
  * Game consists of one scene.

# Time Management

| Task | Time it Took (in hours) |
| :---- | :---- |
| Setting up Unity, making a project in GitHub | 0.5 |
| Research and conceptualization of game idea | 0.3 |
| Searching for 3D models | 3 |
| Modeling and animating a mp5 and arms.  | 12 |
| Implementing camera and movement controls and code | 0.5 |
| Inventory system | 12 |
| weapon animations system | 12 |
| weapon behavior and data system | 12 |
| enemy behavior | 15 |
| Main Level | 10 |
| UI | 3 |
| figuring our probuilder  | 2 |
| Baking lighting | 4 |
| Code documentation | 2 |
| Making readme/report | 2 |
| **All** | 90.3 |

over about a month. might be totally off. 

# Used Resources

* First Person Controller for Unity (Q3-inspired first person controller) \- [https://github.com/atil/fpscontroller](https://github.com/atil/fpscontroller) 
* Fab.com \- [https://www.fab.com/sellers/Quixel](https://www.fab.com/sellers/Quixel)   
* Mixamo.com \- [https://www.mixamo.com/\#/](https://www.mixamo.com/#/) 
