# Project FLYING V

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Kai Gidwani
-   Section: 02

## Game Design

-   Camera Orientation: Top down
-   Camera Movement: Camera stays still as enemies come to you
-   Player Health: Set amount of lives
-   End Condition: Defeating all the enemies on the screen
-   Scoring: Blasting enemies
-   How I delayed firing:
    -   Player: Used the given context variable of the OnFire method to only create a projectile once per press.
    -   Enemy: Created a class to store all the information. Used a cooldown time on top of random firing with adjustable intervals for both.
-   Enemy types:
    -   Trumpet: Not very wide but decently tall. Average speed. Shoots small, thin musical note projectiles.
    -   Accordion: Wide and short. Slow speed. Shoots wide and big, but slow, musical note projectiles.

### Game Description

You are a magical Flying V Guitar set on spreading the love of music to all those who will hear you out. By force. Your goal is to destroy all other instruments and prove through the power of music and incredible violence that the Flying V Guitar is the best instrument ever.

### Controls

-   Movement
    -   Up: W
    -   Down: S
    -   Left: A
    -   Right: D
-   Fire: Space

## Your Additions

I hand made all the art assets myself!

## Sources

-   I made all the assets in this game by myself.

## Known Issues

-    It is know that objects offscreen do not get culled.

### Requirements not completed

-    None

