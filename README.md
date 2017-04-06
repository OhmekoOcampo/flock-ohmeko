Project2-Boids!
Due Date: 2 April 2017
Class: CS 596 Advance 3D Game Programming
Professor: Steve Price
Student: Ohmeko Ocampo
//////////////////////////////////////////////////////////////////////////////////

This project was late because primarily I was working on my Electrical Engineering Lab during 
Spring Break for my Senior Design Project. Sorry about the late turn in Professor Price. :)

//////////////////////////////////////////////////////////////////////////////////

Purpose: The purpose of this project is to implement and simulate the Flocking Algorithm. 
The flocking algorithm details the in unison and grouping movement that birds do. 
There are three basic concepts that govern a flock of birds as defined in the
book: "Unity AI Game Programming - Second Edition"

1) Separation: This means a bird has to maintain a distance with other neighborings birds in the flock to avoid collision.
2) Alignment: This means birds in the flock have to move in the same direction as the flock, with the same velocity.
3) Cohesion: This means to maintain a minimum distance with the flocks's center.

We were tasked as undergraduates to create at mimimum two flocking flight modes. I choose to do these two modes:

1) Lazy Flight: The flocking AI will choose a random point to fly towards within your scene. When the destination
is reached, a new target within the limits of the scene is chosen and the flock flies towards that. 

2) Follow the Leader: The Flocking AI Controller will follow a clearly marked target.

Notes: 

1) This time I have the project properly documented and commited. I will also turn in a copy via blackboard. 
2) I placed complexity analysis in my TDD word document.

Asset Folder: (There should be 8 folders in total) 
Animations
Art
Font
Prefabs
Scenes
Scripts
Sound
Textures.

Bugs:
1) The Follow the Leader can sometimes be slow. Don't know what is causing the slow down. 
