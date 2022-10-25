# COMP140 Project
Base repositry for the COMP140 - Creative Computing Project

Please use the folder structure as provided. If you end up developing using another board, engine or framework than Arduino or Unity just rename the folder to reflect this.

How to Play:
The goal of Gravitate is to find a way out of the caves, along the way the player will stumble across dead ends and a bunch of different enemies. To defeat enemies the player must knock into them as quickly as possible(at least 6m/s) to do damage, otherwise the enemies will damage the player. There are 3 types of enemies, generic red enemies that are 1 hit kill and will follow the player around until they're out of range, purple slime enemies which are just bigger red generic enemies with more health but upon death they release a bunch of smaller and quicker versions of themselves (pretty much minecraft slimes), and light blue gun enemies which wont damage you upon contact but will definately shoot you,the bullets are slow and dont do much damage but they push you back a decent amount so the closer you are to a gun enemy the harder it is to hit them with enough speed. Good Luck and Have Fun! 

p.s. it's possible to escape without killing the enemies at all though it may be hard

Controls:
Keyboard - Use left and right arrow keys or "a" and "d" to rotate the character 
Mouse/Touchscreen - Click and hold to the left or right of the player to rotate the player in that direction

How to Start the game:
Open up "Web server for chrome" app on the controller
If this is your first time playing this game on the system, make sure to choose the folder where the game is stored
To start the game up, click on the web server URL
![web server for chrome screenshot](https://media.github.falmouth.ac.uk/user/829/files/c20c510a-c3ae-4fcf-a512-57ba18f06a88)

Issues(29/03/2022): 
The game was meant to be played by physically turning the controller. The controller is a self contained unit consisting a touchscreen display, raspberry pi, and a mpu6050 gyroscope. I've encountered many difficulties so far, for example getting a Unity game to run on the Pi. The way I decided to run the game on the Pi is to export the game build using webGL and running it on a local web server using the "Web server for chrome" app. Unfortunately, I didn't realise that browser games (or just anything on the browser) are blocked from running executable files on your pc. The way I've been reading gyro data so far from the gyroscope is using a python script which has the correct mpu6050 library. I thought I could start a process which would run the python script from within Unity, this technically worked within editor but as soon as I ran the game on the browser it stopped working and produced errors which literally have no fixes as they're intentionally there to stop browser applications from running exe files. I exhausted a few options to try and bypass this problem like for example downloading a pre release python extension for Unity but nothing works. I have one more option I can think of, and that is to install a library which lets me access mpu5060 data in c# and therefore from within Unity itself. I have doubts about this option and I'm slowly approaching the deadline for this project so I'm going to focus on making the game utilising the touchscreen instead as it's much quicker and easier. I'm basically hoping that once I finish making the core gameplay the best it can be, I'll have enough time to implement the gyroscopes functionality. My thinking is that I'd rather lose marks on the hardware side rather than software side as I'm more comfortable with software so I can maximise my marks there.
