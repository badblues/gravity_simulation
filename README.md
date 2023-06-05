# Solar System Simulation
This is a simulation of the solar system gravity between celestial bodies.

## Controls
The simulation can be controlled using the following keyboard shortcuts:

* Escape: Close the simulation.  
* Space: Toggle the visibility of trails left by the celestial bodies.  
* Up/Down Arrow: Increase or decrease the simulation speed, respectively.  
* Scroll Wheel: Zoom in or out of the simulation.  
* Left Mouse Button: Click and drag to move around the simulation.

## Data Format
Celestial bodies are defined in JSON format in the objects.json file. 

- Name: A string representing the name of the celestial body.
- Type: A string representing the type of celestial body (e.g. "Star", "Planet", "Moon", etc.).
- Mass: A number representing the mass of the celestial body in kilograms.
- Radius: A number representing the radius of the celestial body in meters.
- Coordinates: A number representing the initial x and y coordinates of the celestial body in meters.
- Color: An object representing the RGB color of the celestial body, with four properties.
- Velocity (optional): An object representing the initial velocity of the celestial body in m/s.

