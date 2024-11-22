# Devlog Entry - [Nov 13, 2024]
## Introducing the team 

### Tools Lead - Dexter Hoang
### Engine Lead - Alan Lu
### Design Leads - Grace Herman, Yingting Huang

## Tools and Materials
### 1. For our final project, we are considering using Unity, Deno, Godot, or Phaser. We chose Unity because it's easy to learn and a majority of us have experience with working on Unity. We chose Deno since it is the bulk of what we've been learning in this course so we all have a good amount of experience with Deno. Another option was Phaser since we have all taken CMPM120 which means we all have used Phaser for a fair amount. Lastly, we chose Godot because of its ability to support similar langauges as Unity and we've seen other classmates use it with great success.

### 2. The programming languages our team chose was C#, Typescript, and JavaScript. If we plan on using Unity, we will expect to use C# since that is the language used for the program. If we use Deno, we expect to use TypeScript since that's the language we've been using with Deno in this course. If we use Phaser, we expect to use Javascript and possibly JSON since that is the language we used in CMPM120.

### 3. We plan on using VSCode for writing our code because it is easy for us to collaborate and has tools to easily commit and push changes to our repository. If we have to create visual assets, we will use Pixilart or MagicaVoxel for 3d assets. We chose these tools since most of us have experience using these from a previous class. We are also the most comfortable using a more pixelated style of art for our visual assets. 

### 4. For our alternate platform we decided to pick Godot. This is because Godot has similar elements to Unity, namely the primary language. While the engine and framework will be vastly different from Unity, the primary language being similar should hopefully allow us to smoothly transfer over to Godot should the need arise. 

## Outlook 
### Our team hopes to complete this final project in a timely manner without having to rush before the due date. The hardest part of this project would be if we have to make a switch to a different platform/engine. This will not only impact our time, but will require us to transfer our work to an entirely new engine with may differ from our original platform. We are hoping to learn more about our platform and feel more comfortable using it for the future. 

# Devlog Entry - [Nov 22, 2024]

## How we satisfied the software requirements

### [F0.a] The player is able to move over a generated 2D grid using either WASD or arrow keys.
### [F0.b] Player can click on a 'Next Day' button to go on to the next day/turn to progress through the game. 
### [F0.c] When the player is close enough to the grid cell, they are able to click onto which pops up a menu in the upper left hand corner of the screen which allows them to reap or sow one of three plants. 
### [F0.d] Each grid cell starts off with 0 water and 0 sun. After each day, each cell gets a random amount of water and sun. Sunlight will change from day to day but the water will accumulate over time. 
### [F0.e] Our three plants, mushrooms, flowers, and herbs, all have 3 distinct stages of growth which the player can visually see as they grow.
### [F0.f] When the player plants something on a tile, the neighboring tiles are checked to see if there are plants under the same species as them. If they are present, the amount of water and sun level in order for the plant to grow is decreased. The decreased amount is determined on the number of similar plants around the selected tile.
### [F0.g] When the player successfully grows 10 plants fully, a text will show that says "You Win". 

## Reflection

### After completing this part of the project, we realized that we have to split the workload more evenly with each other. The amount of work was greater than we thought which led us to chrunching on time and staying up late. As a result, we will focus more on our team organization and planning so that everyone does their fair share of work and not a majority of the work gets pushed onto a member(s).
