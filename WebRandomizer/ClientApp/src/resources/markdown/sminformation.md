# Randomizer Information

## Changes from the vanilla game and other notes

* When using the Major/Minor split Item Placement the randomizer splits
  Major items/Reserves/E-tanks in one pool, and the ammo packs in another.
  * There is one exception: The e-tank at Hi-Jump is no longer a major item
    location, it has been moved to the right super missile pack location in
    Wrecked Ship.
* The door into the construction zone (blue brinstar ceiling e-tank room) is
  now a Blue door and will have zebes activated when you enter it so you can
  check the items there right away.
* The red tower elevator room's yellow door is blue to prevent softlocks.
* Gravity no longer protects you from environmental damage (heat, lava, acid,
  spikes). As a side effect, varia protects you from the same sources of damage
  twice as much as it normally would.
* Because of the above change, Varia will never be in Lower Norfair.
* Some cutscenes have been removed or shortened.
* Golden 4 Cutscene is eliminated, if you killed all four bosses, the way to
  Tourian is open immediately upon entering.
* Suit animation cutscenes are gone, so it doesn't place you in the middle of
  the screen, potentially soft locking you in solid blocks.
* You will no longer lose blue speed echoes while taking heat damage.
* S/T beam is disabled.
* GT Code is disabled.
* The Intro sequence and Ceres station are no longer in the game, you instead
  start directly on Zebes from a blank file.

## Quick guide to getting started

To create a new randomized game, head over to the [Generate Randomizer Game](/configure) page.

1. Select the options you want to use for the game, and then press **Generate
   Game** to generate a new game.
2. After doing so you will be redirected to the "permalink" page where you
   need to select your original Super Metroid ROM to continue.
3. There's also a few extra customization settings that can be done here such
   as changing the sprite of Samus as well as some other options.
4. When you're happy with the options, hit *Download ROM* and you'll get a new
   ROM that you can play with an emulator or SD2SNES.

If you didn't select the *Race ROM* option, there will also be a *Playthrough*
section that will list the items and their locations in case you get stuck and
need some hints.

## Game generation options

These options are available when generating a new game.

### Logic

This option selects what kind of logic to use for item placement inside Super Metroid.

* Casual - Casual logic includes only what Super Metroid teaches players itself.
  Anything that's not demonstrated in-game or by the intro cutscenes will not
  be required here. Required moves include for example:

    * Wall jumping (not including very precise ones)
    * Shinesparks in any direction (short charging for these is not expected)
    * Infinite Bomb Jumps

* Tournament - Tournament logic is based upon the "no major glitches" ruleset
  and includes most tricks that are considered minor glitches, with some
  restrictions. You'll want to be somewhat of a Super Metroid veteran for this
  logic. Required moves include (in addition to anything in Normal logic) for
  example:

    * Mockballs
    * Short charging
    * Suitless underwater navigation
    * Continuous wall jumps
    * Forced heat damage
    * Advanced damage boosting
    * Clipping through certain blocks using enemies
    * and more...

### Goal

This option currently only has the *Defeat Mother Brain* option, but will be
expanded in the future to include other goals.

### Item Placement

This decides how items are placed in the game.

* Major/Minor split - Major items, Energy Tanks and Reserve Tanks are
  randomized in one item and location pool, and ammo packs in another.\
  There is one exception: The e-tank at Hi-Jump is no longer a major item
  location, it has been moved to the right super missile pack location in
  Wrecked Ship.
* Full randomization - Any items can be at any location.

### Seed

If you want to enter a custom number to be used as the random seed you can do
so here, or leave it blank to have a random one picked for you. Numbers between
0 and 2147483647 are valid.

### Race ROM

Enabling the Race ROM option will generate your game without access to any spoiler log.

### Game Mode

Picks the game mode you want:

* Single player - Regular single player game.
* Multiworld - Creates a co-op multiplayer multiworld session and requires
  additional entry of player count and player names.

## Customization Options

These options are entered after game generation and are cosmetic or non
gameplay-altering options only.

### Play as

This lets you select the sprite to use for Samus.

For Samus there is a toggle for how you want the Screw Attack animation to look
when you don't have Space Jump equipped. If it is unchecked, the Screw Attack
animation will always include the Space Jump animation as well (as in the
original game), but if it's checked you will get the Screw Attack animation
applied on top of the regular spin jump animation.

### Energy Beep

Toggles the low health energy beep in Super Metroid.
