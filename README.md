# PuzzleBoobleClone #

A clone of the puzzle booble game in XNA.

### Feature list : ###

- Two Leveled-game : if you win in the first level, you go to the 2nd level; if you win the 2nd level, you go back to the 1st level. If you lose a level, you restart the same level

- Ball Animations when they "Pop", "Load", "Fall" or when the player loses.


## Run Instructions ##

- Unzip the package
- Open the PuzzleBoobleClone.sln file with Visual Studio
- Go to Debug -> Start Without Debugging

## Known Issues ##

- Sometimes the ball falls even if it is attached to another ball.
- Controls are not locked when the player loses or wins.

## Code Comments ##

- Created an interface called GameElements.cs that hold a Update() and Draw() Method.
- GameElementsRepository.cs class holds all instances implementing the GameElements interface.
- HangingBalls.cs holds all the Balls that are present on the balls grid and contain all the game logic and rules.
- BallsRepository holds the current ball fired, the next ball to be fired, and triggers interaction with HangingBalls.
- Bounds.cs tracks the current upper wall limit that goes down every 15 seconds.
- BallAnimationHelper manages a Ball.cs state and determines what sprite to draw depending on the that state.