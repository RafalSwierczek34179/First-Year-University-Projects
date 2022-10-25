Gravitate 

In this 2D game the player is stuck in a cave labyrinth deep underground, their main objective is to find a way out alive. Stopping them will be different types of enemies, this will allow me to use ideas from object orientated programming such as polymorphism to implement the enemies. The player will control their character using a custom controller which they must rotate around the z axis to tilt the in-game world, this will result in the player’s character falling through the map. Rotation around the x axis will determine the player’s falling speed. For example, holding the controller up right will result in the player falling at max speed, whereas holding it parallel to the ground will make the player float in place. The velocity at which the player hits an enemy will determine the amount of damage the enemy receives, so the higher the velocity the more damage an enemy takes and vice versa. 

Constraints: 

This controller will not copy any conventional controller designs, it will however use a sensor which may appear in other controllers, a gyroscope. 

This controller will not feature a single button, instead it will use a gyroscope to measure z and x rotation of the controller and that will be the sole input for the game. The design will also allow the user to plug in a mouse and keyboard to the raspberry pi to make it convenient to use. 

I will 3D print a rectangular shaped case to house the raspberry pi, gyroscope and mini display along with a circular handlebar for the users to hold onto whilst they use the controller.  

I will allow the game to take input from the arrow keys on a keyboard to tilt the game world to test game functionality at my pc before I build the actual controller. 

Design: 

 ![image](https://media.github.falmouth.ac.uk/user/829/files/f5616d20-92e7-4b8d-9951-f310efa2d3e9)


The design will feature a center piece which houses a mini display, raspberry pi and gyroscope. The back will feature a hatch to allow me to take things in and out of the case. The case will also feature cut out holes which allow the user to plug in a mouse/keyboard/power supply to the raspberry pi, allowing them to also use this as a minicomputer. The case will also be attached to a soft circular piece that will be used as the point where the player holds the controller, this will allow them to easily rotate the controller around the z axis.  

Key Components: 

Raspberry pi 4 

Mini display (4.3" DSI Capacitive Touchscreen Display) 

Gyroscope (MPU 6050) 

USB power supply (The official Raspberry pi power supply) 

3.5mm mini speaker 
