# TP Lab 10
<i>To see lab 10 scripts, go to the "lab-10" branch.</i>
<br><br>

Team: <br>
- Summer Smith
- Chandler Guzman
- Colin McDaniel
- Colin Pierce
<br><br>

## Video
https://github.com/user-attachments/assets/40f15022-ab73-4df2-99cc-8d14085e1500

<br>

## Save System
The save and load system was implemented through the ISaveable interface, Binary serialization, and JSON serialization. 

SaveLoadInput handles player input: when the player presses s, SavingServices.SaveGame() is called. SaveGame finds all objects that implement ISaveable and collects their JsonData. It then serializes this data into a JSON file. When the player presses l, SavingServices.LoadGame() is called. It checks if the file exists before reading the text, finding the objects using their SaveIDs, and calling their LoadFromData() methods to load them.

Each ISaveable object determines what information will be saved by serializing values into their own JsonData. RowSaver, for example, saves enemy positions, the left end, and right end. It rebuilds the rows from its data when it is loaded. 

The score is saved and loaded in Score with a Binary file. It also updates the score UI on the screen. 

The GameManager calls Score and Row when the game starts.  Score loads its score, and Row rebuilds its rows. It also saves the score when the application is quit.

Both the Json and Binary processes use Unity's persistentDataPath to store their files.