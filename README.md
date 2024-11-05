#The Game

In the game you need to dodge sherikans while destroying enemies in order to achieve the higest score!

## Demo Video

Bellow is a link to a youtube video!

[![Watch the video](https://img.youtube.com/vi/VMxjVGqkMns/0.jpg)](https://youtu.be/VMxjVGqkMns)

# Overview

## AudioManager.cs
Handles background music and sound effects for the game. It plays, pauses, and stops audio based on in-game events.

## BeatMapManager.cs
Manages the spawning of enemies and shurikens based on a beat map. Reads events from JSON and spawns enemies or projectiles at specified times and locations.

## BoundaryManager.cs
Defines boundaries within the game scene to restrict player movement and enemy positioning. Used to keep entities within a set area.

## ClickEffect.cs
Displays a visual effect when the player clicks on the screen, such as showing a small sprite over the player character.

## CustomCursor.cs
Implements a custom cursor for the game. Allows a customized image or animation to appear as the cursor during gameplay.

## DamageOnTouch.cs
Reduces player health when they touch an object with this script, such as certain obstacles or enemy projectiles.

## EnemyController.cs
Controls enemy behavior, including animations, click interactions, and timing for despawn. Tracks clicks to give the player health rewards after a set number of successful hits.

## GameManager.cs
Manages overall game states, including starting, pausing, and ending the game. Centralizes control of game flow.

## MainMenu.cs
Handles navigation and interactions on the main menu screen, allowing players to start the game, adjust settings, or exit.

## PlayerHealth.cs
Tracks the player's health, displays it on-screen, and manages health restoration and damage, including visual updates for the health bar.

## PlayerInputActions.cs & PlayerInputActions.inputactions
Defines player input controls for various actions, including movement, firing, and dashing. Used to capture input and map it to player actions.

## PlayerMovement.cs
Controls player movement, dashing, and position clamping. Handles dash cooldown, dash direction, and invincibility frames during dashes.

## ScoreManager.cs
Manages the player’s score, updating it based on events such as defeating enemies. Provides UI updates to display the score on-screen.

## ShurikenMover.cs
Handles the movement and lifetime of shurikens (projectiles). Moves shurikens based on a set speed and direction, then despawns them after a set time.

---

## maker.py
Python script for generating beat maps by parsing click and dash timings from input text files. Outputs a JSON file with timed events for enemy and shuriken spawns.

## raw_click_input.txt & raw_dodge_input.txt
Input text files for maker.py, containing timing data for clicks and dashes, which are parsed to create beat map events.

# Maps and Menu

The menu is very basic right now, if this were to expand,
I would have a system to sort, select, and input

The best two maps in my opinion are beatmap and beatmap0

beatmap has no shurikens while 0 has the shurikens, both have the same spawn

## Making your own maps

I made some python scripts that help make beat maps,

to use, first chose the song in the AudioManagerm

then record yourself "clicking" where you want and finish the song

take the logs and put them into the raw_click_input file for in the BeatHelp directory

do the same but for shurikens you want to drop

with basic python knowledge you shoudl be able to run and handle it

you can also use parts of existing beat maps to create new ones

# Apologize and Moving Forward

There are some unused assets and scripts that are kinda empty, I had plans but they are still in development

I tried to keep it to a mininum, regardless I apologize

Thank you for reading this far if you have!
