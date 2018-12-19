# Game-Engines-1-Assignment-Procedural-Audio-Visuals
This is an assignment for Game Engines 1 (Final year) Dublin Institute of Technology

# Project Overview/Proposal :
The requirnments for this project are to create a procedural generation system in Unity3D.
My plan is to create 3D visuals that respond to a sound source. The sound source will be analysed with code and the environment will procedurally build and respond based on the sound source that is playing in realtime. I plan to create beautiful visuals that engage the visual senses and enchance the listening experience, and somewhat represent the music visually.

Some inspiration for what I am going for are posted bellow. I would like to generate a mesh terrain that reacts to the  different frequency ranges from the sound source (split the sound into bands). Also would like to have some sort of procedural mesh generation based off the sound source and values derived. This will create beautiful objects and formation of meshes (potenitally some creatures that dancce to the music (Procedurall animations)). Color changing and texture modification based on the music. 

Best case scenario the application will work on any sound source provided and will work and create beautiful music to any music genre. Slower beat per minute music and higher beat per minute music will be represented respectevly.

# Research Plan & Implementation plan
- Interpreting sound from a sound source in Unity
- Applying sound interpretation to objects and meshes in Unity
- Mesh building (Research how to manually build meshes in Unity)
- Mesh deformation in Unity
- Mathematical algorithms to create beatiful structures
- Procedural animations
- Post processing

# Some inspiration for the project :

Bubble Sound | Pre-rendered 3D Music Visualizer
https://www.youtube.com/watch?v=naFzc92hY9A

Particle tests (15) 3D Music Visualizer - Full HD
https://www.youtube.com/watch?v=fpViZkhpPHk

NEW 2016 Psychedelic Psychill 3D Visual Progressive Music Mix
https://www.youtube.com/watch?v=8t3XYNxnUBs

Music Visualizer - 3D audio spectrum visualizer made with Unity3D
https://www.youtube.com/watch?v=GcddK4RMk_0

Real 3D Music Visualizer - Review
https://www.youtube.com/watch?v=JHMOtdITPQE

# FINAL VERSION PROJECT DESCRIPTION

# Description :
This project is a modular music visualisation system. The project allows a user/player to play with this Asset Pack and conviniently set up their own Visualizations on the fly. Each module(GameObject) is a procedural system that the user can tailor to a song of choice and make infinite combination of visualization systems. The visualization systems rely on the Phylloytaxis algorithm, shaders, trail renderers, gravity effects , scaling and more. All of this is programmed to be effected by the song that is currently playing. The systems can be configured individually to set up what parts of the song each element of the visualizer will react to. More will be explained in detail below :

# Music In Code : 
The music is extracted using the unity GetSpectrumData utility. The system and class I used for this was implemented similar to the one we learned about in class. The reason I used this system, is because it does exactly what I need and more or less. The music data is split into bands that essentially cluster together frequency ranges. This is done to have a set amount of usable values, that can be used throughout the whole program.

# Phyllotaxis Algorithm :
The phyllotaxis algorithm is used to control the different shapes and movement of the all of the different objects in the scene. This algorithm is described here : http://algorithmicbotany.org/papers/abop/abop-ch4.pdf. It is a really cool algorithm that I was inspired by. After doing some research I have found sever Videos on youtube that have implemented this algorithm and came out with some impressive results. Such as : https://www.youtube.com/watch?v=Mn5O732Y5nw,   https://www.youtube.com/watch?v=ZK2ACC6scrw

I decided to make similar systems, but add extensive modularity and customizability so the User can use my Assets to create their own visualizers quickly through the Unity Editor. All of the scalers, values of the systems are exposed to the user. This creates and infinite amount of possibilites and creations. 

# Customizability :
Between all of the various elements in the system the user has access to many customization options. Most of the phyllotaxis visualisers I have seen are regular one class systems. How ever used a hierarchy of classes to make a dynamic system that can instantiate objects and modify them based on users preferences. The user can customize every part of the algorithm, such as degree, scale, iteration start number and step size. The user can also change the amoutn of objects spawned, the size of them, speed of scalling and many more features that can be observed in the unity editor. The user can create many 2D elements, 3D elements for the visualizer. There is also a procedurally genrating terrain that is generated and effected by the music that the user can use to make an infinite 3D visualizer.

# Procedural Generated Mesh
I have created custom code to make a procedurally generated mesh. This mesh grows as the camera moves forward and the surface of the mesh is deformed using the combination of an auioband  and perlin noise. The user can set the lenght and the width of this mesh . The shader on the mesh also changes to the music. This is the emission value of the shader.


https://www.youtube.com/watch?v=Mn5O732Y5nw
