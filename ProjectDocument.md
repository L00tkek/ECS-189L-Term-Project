# Game Basic Information #

## Summary ##  

You have tasks you need to do, but you're disabled and have a limited amount of
spoons (energy) to do those tasks. You can rest to recover spoons, but you'll
get more tasks. You must find a balance in order to complete all your tasks
without running out of spoons. How long can you survive?

## Gameplay Explanation ##
### Controls
- WASD or arrow keys to move
- 0-9 number keys to complete tasks 
    - (alternatively, you can click on the GUI)
- Backspace or Escape to cancel tasks
    - (alternatively, you can click on the GUI)
 
### Strategy
- have you tried getting a planner?
- time management
- prioritize!
- if you lose, you're clearly not trying hard enough. 

# Contributions
Both of us were originally part of the data team in the social proxemics research project, but most of the work for that project ended up getting done
by one or two people, leaving us without a significant amount of work to use for our final project. Because of that, we collaborated to create this game
in just five days so that we would have something to show as our final project. Because of this, we didn't really follow the main role/sub-role structure,
and both of us tended to work on different parts of the same systems. Our contributions are below.

## Sage's Contributions
- found and added all assets except for the player sprite
- created the task prefab and wrote code to make the player destroy tasks on contact
- made the player track a number of spoons that counts down over time (and displayed this quantity)
- created the game environment (just a single room)
- added the bed and wrote code to "start a new day" (give the player spoons and spawn more tasks) 
  on contact with it
- implemented the mechanic where lack of spoons adds a darkening overlay onto the screen
- wrote the code to handle when the player loses the game by getting to 100 knives or by reaching 25
  pending tasks
- added keyboard controls for the number-matching minigame
- wrote the README

## Lucy's Contributions
I copied over the position lock camera controller from exercise 2. I copied
over the player sprite from a previous project and wrote the logic to
animate it. I wrote the task manager to randomly spawn tasks and to spawn tasks
in random places as well as display the current number of tasks. I also did
the logic for slowing down the player according to the number of spoons/knives
the player has. I also created a vignette effect for when the player gets into
knives. I also implemented the memory minigame and integrated it into the rest
of the game with some help from Sage to get the layers right.
