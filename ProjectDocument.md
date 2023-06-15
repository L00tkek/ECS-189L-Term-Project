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
I was responsible for implementing many of the simple foundational concepts for our game, such as implementing
player movement with WASD and the arrow keys.
I also wrote code to implement a counter of spoons that counts down over time, as well as the code to
display this quantity in a text box on screen. Additionally, I created the task prefab and wrote the initial logic for 
handling tasks, which was that tasks simply disappear upon contact with the player. I added the bed 
and the sleeping mechanic, where touching the bed provides the player with more spoons at the cost of spawning more 
tasks. I built the room that the game takes place in to provide a simple environment that would facilitate gameplay. 
Lastly for these foundational aspects, I wrote code to handle when the player
loses the game by accumulating 100 knives. This entailed adding the fade-to-black into the "you lose" message, and also
writing code to allow the player to restart the game after losing. Part of this involved causing the game to start out in
a loss state, which furthers the themes of being trapped in a cycle that one can only lose.

In addition to those, I implemented several features that add polish and help to flesh out the game's themes.
I was responsible for finding and adding all assets to the game except for the player sprite (which was 
made by Lucy). These were primarily just placeholders as we were pressed for time, but it helped the game look more
professional. I cooperated with Lucy to design the fatigue mechanic, where having fewer spoons causes various effects
that are supposed to show how the player's fatigue is affecting them. Lucy and I jointly decided that fatigue would be
applied on a square root curve, and would not be applied when the player's spoons are positive. This makes
the transition from positive to negative spoons much sharper and more noticeable, and also makes it feel like
the player is slipping into fatigue more quickly the closer they get to 100 knives. I specifically implemented
the dark red overlay that fades to black once the player reaches 100 knives. I helped Lucy with the minigame mechanic
by adding keyboard controls so that players could type the numbers instead of having to click them, which was
suggested by our playtesters. Lastly, I wrote the README for our repository, which contains information about the game
and credit for all of the assets used. 

## Lucy's Contributions
Sage and I both discussed the metaphors and themes of the game and how to
design the mechanics in a way that complements those themes and metaphors.
I copied over the position lock camera controller from exercise 2. I copied
over the player sprite from a previous project and wrote the logic to
animate it. I wrote the task manager to randomly spawn tasks and to spawn tasks
in random places as well as display the current number of tasks. I also did
the logic for slowing down the player according to the number of spoons/knives
the player has. I also created a vignette effect for when the player gets into
knives. I also implemented the memory minigame and integrated it into the rest
of the game with some help from Sage to get the layers right.
