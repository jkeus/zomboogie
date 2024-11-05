# The Game

In this game, you need to dodge shurikens and destroy enemies to achieve the highest score! Stay alive until the end of the song for maximum points.

I know that the name should be like FrogBoogie, but Zomboogie is too good to pass up on.

Controls are: WASD for movement | space for a dodge/dash | left click to shoot

Also, massive shoutout to [Camellia](https://www.youtube.com/channel/UCV4ggxLd_Vz-I-ePGSKfFog) for the music and [Pixel-boy](https://x.com/2Pblog1) for the assets!

If you are only interested in playing, downlaod just the builds foler and run the .exe, thank you!

## Demo Video

Watch a demo video below!

[![Watch the video](https://img.youtube.com/vi/VMxjVGqkMns/0.jpg)](https://youtu.be/VMxjVGqkMns)

# Overview

Here’s a brief description of the core C# scripts in the project. Be sure to check out the Map Editing section as well!

## AudioManager.cs
Controls background music and sound effects, handling playback, pausing, and stopping based on in-game events.

## BeatMapManager.cs
Manages the timing and spawning of enemies and shurikens based on a beat map. Reads events from JSON to control spawn times and locations.

## BoundaryManager.cs
Defines boundaries for player movement and enemy positioning to keep entities within the playable area.

## ClickEffect.cs
Displays a visual effect at the player’s click location, adding interactivity to each click.

## CustomCursor.cs
Implements a custom cursor, allowing for unique cursor images or animations in the game.

## DamageOnTouch.cs
Decreases player health when they touch certain objects, such as obstacles or projectiles, and updates health, score, and streak.

## EnemyController.cs
Controls enemy animations, click interactions, and despawn timing. Tracks successful clicks to reward the player with health after a set number of successful hits.

## GameManager.cs
Handles overall game states, including starting, pausing, and ending the game, centralizing control of the game flow.

## MainMenu.cs
Manages main menu interactions, allowing players to start the game, adjust settings, or exit.

## PlayerHealth.cs
Manages the player’s health, displays it on-screen, and handles restoration and damage. Tracks and displays the player’s score.

## PlayerInputActions.cs & PlayerInputActions.inputactions
Defines controls for actions like movement, firing, and dashing, capturing input and mapping it to gameplay actions.

## PlayerMovement.cs
Controls player movement and dashing, handling dash cooldowns, direction, and invincibility frames during dashes.

## ScoreManager.cs
Manages the player’s score, updating it for events like enemy defeats, and provides a UI update to display the score.

## ShurikenMover.cs
Currently unused for moving shurikens, as gravity is handling their movement instead. Initially planned for projectile shooting.

# Maps and Menu

The menu is currently basic. If expanded, it could include options for sorting, selecting, and inputting custom maps.

The best maps right now are `beatmap` (no shurikens) and `beatmap0` (with shurikens), both with similar spawn patterns.

# Creating Your Own Maps

Custom maps can be created with the provided Python scripts. First, choose a song in the `AudioManager`, then record where you want enemies to spawn by "clicking" during gameplay. 

After finishing, save the logs and place them in the `raw_click_input.txt` file in the `BeatHelp` directory. Repeat the process for shurikens, record only dashes, then place the logs in `raw_dodge_input.txt`.

With basic Python knowledge, you should be able to run and manage these files to generate a new beat map. You can also reuse parts of existing beat maps to create new variations.

## maker.py
Generates beat maps by parsing click and dash timings from input text files and outputs a JSON file with timed events for enemy and shuriken spawns.

## raw_click_input.txt & raw_dodge_input.txt
Input files for `maker.py`, containing timing data for clicks and dashes, which are parsed to create beat map events.

# Apologies and Future Plans

There are some unused assets and placeholder scripts in the project that are still under development. I have a lot of ideas for the main game, but it’s challenging to prioritize them all. 

A more advanced map editor and game balancing features are on the wishlist, though they’re still in early planning stages. 

Thank you for reading this far, and I hope you enjoy the game!
