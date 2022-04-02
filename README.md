# Bool Pgia Game

## Description and general background:
Development of a window application of the Bool Pgia game.
  - The app is divided into two: Logic and UI.
  - 

### Start Window:
* The app will first ask the user for the desired maximum number of guesses (number of lines).
  - The top button will function as a "counter". Each click on it will increase the number that appears on it by 1 (starting with 4 and a maximum of 10 and repeating).
   ![image](https://user-images.githubusercontent.com/76681385/161393701-a8f9da74-ee63-4155-9c76-2391f7d5e8d8.png)

### The Board:
* The initial state will be a board that will contain:
  - 4 black buttons.
  - X rows of colorless buttons according to the maximum number of guesses the user requested in the first window.
  - To the right of each line will be a button with which the user can decide that he has finished selecting the current guess and wants to display the result of a           guess. (Arrow buttons start in "Inactive" mode)
  - A column of four buttons. These buttons will show the user the result of each guess.
  - ![image](https://user-images.githubusercontent.com/76681385/161393941-454827b4-f37d-4a01-9da4-444fc91bc563.png)

### The Game:
* Clicking on any of the buttons will show the user another small window with which the user can choose one of 8 colors.
  - The user can change the color selection in his guess as long as he does not press the arrow button.
  - You cannot select the same color twice.
* After the user presses the button with the arrow, a result will appear when: 
  - The buttons on the right will be colored black for each "Bool" and yellow for each "Pgia".
  - ![image](https://user-images.githubusercontent.com/76681385/161394083-8b935747-df3c-4735-afab-181b53c249bb.png)

### The end of the game:
* if the user guessed correctly or if the guessing user ran out, then the correct sequence will be revealed in the four buttons in the top row.
  - ![image](https://user-images.githubusercontent.com/76681385/161393272-6c46a028-79aa-43c0-99a7-8ccf27b93578.png)
