# CodingTracker.m1chael888
This is a simple C# console application enabling the user to create and manage coding sessions. Sessions are saved in an SQLite database.

# Requirements
* Use the Spectre.Console library for data display
* Classes must be seperated into different files
* Tell the user what format to use for date inputs and dont accept different formats
* Create and read from a json file for config
* Populate a list of session models when reading from db, no anonymous objects
* Duration of a session must be calculated by the program and supplied by user
* User will be able to manually supply start and end times of a session
* Use Dapper ORM for data access
* Avoid code repetition
* Include a read me file discussing the app and its development

# How it works
* After opening the app the main menu will be called up. The arrow keys are used to navigate the menu options. Press enter to select one

* Selecting AddSession will prompt you to select a session type. New sessions will be timed live via stopwatch, and Past sessions will require manual input. The new session screen will notify the you that you are being timed and ask you to press any key to cancel the timer and save the session. When entering a past, manual session, you will need to follow the date/time format displayed, and will be prompted to try again until the correct format is used

* Selecting ViewSessions will display a simple list of saved sessions

* Both EditSession and DeleteSession will display the same list as ViewSessions, but will allow the you to use the arrow keys to select one. If editing, the app will prompt the you to enter a new start and end date for the selected session, and if deleting, the selected session will just be deleted. In any menu, before returning to the main menu, the app will wait for a key press to acknowledge the completed operation

* Selecting ExitApp will close the application

# Thoughts/Challenges
While working on this project I learned many new concepts and ideas and used various new technologies. 

* I used this app to learn about dependancy injection and di containers, promoting seperation of concerns and resulting in loosely coupled code which will benefit future testing. This also gave structure to the code base, making adding or changing code easier
* Spectre.Console is a useful library for console app development, I enjoy the selection prompt class which makes navigation much more intuitive for the user. General stylization like coloring text is also made much easier compared to vanilla console apps
* It was also my first time using Dapper, which I found more enjoyable to use for data access than ADO.NET
* In future I plan to continue to learn about software architecture and code organization by looking into clean architecture and implementing the new ideas and concepts I learn about
