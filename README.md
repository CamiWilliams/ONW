# ONW

## Description

You are a contestant on a gameshow where you have to solve puzzles in a certain amount of time. The puzzles will have both logic and physics components to test your ability to solve quickly under time pressure.

Our vertical slice of this game will feature a course the player must race through. The player will have 5 minutes to complete 4 stages within the course. At the start of each stage, they will pull a lever to begin.

### Stage 1: Memory Platforms
In this first stage, a player will approach 5 different platforms. When they pull the lever to begin the stage, the platforms will highlight in a certain order. After the highlight sequence is completed, the player will have to “jump” (teleport) onto the platforms in the correct order.

Students should learn:
* Locomotion through teleportation
* In-game animation and object interaction
* Anchoring
* Snap turning

### Stage 2: Throwing Game
For the second stage, the player will pick up balls and throw them at boat targets. They can “summon” new balls by pulling the lever.

Students should learn:
* How to implement throwing.
* Grabbing gesture mechanics

### Stage 3: Simon Says
For the third stage, the player will pull the lever and be teleported to a new world that has hand tracking enabled. When the player enters the scene, they will be notified to place their controllers down. A large hand will appear in front of them, and perform a sequence of gestures. The player then must mimic the gestures after the large hand’s sequence is completed. 

Students should learn:
* Hand tracking
* OVRPlayerController
* How to reference and recognize built-in hand gestures.

### Stage 4: Rock Climbing
At the final stage, the player will approach a wall. When the player pulls the lever, rocks will appear on the wall that they will have to use their controllers to climb. The player will race to the top of the wall using their controllers to grab rocks as they ascend.

Students should learn:
* How to implement game physics interactions with objects

Once the player has completed each of the 4 stages, they will hit the final button at the end of the course. Doing so will pause their time to mark completion and store their time as a final score.

For known TODOs, feature requests, and bugs, see open issues.
