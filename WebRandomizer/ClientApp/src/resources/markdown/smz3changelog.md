# Randomizer Changes

## 2020-04-11 - Version 11.0
* Systems Changes
  * Complete rewrite from PHP to C#
  * Multiworld support (See the [Multiworld Instructions](/mwinstructions) page for more information)
    * Included Super Metroid multiworld support, and solo SM rando accessible at [Super Metroid Randomizer](https://sm.samus.link/)
  * Auto-stripping of unneeded headers from base roms
  * Added the [Logic Log](/logic) to the website that explains the Randomizer logic in detail.
  * New custom title screen
  * Soft reset (L+R+Select+Start) added to both games
  * Options for "early" morph and/or sword. (Early meaning a location accessible from the start of the game)
  * Option for Uncle Assured sword
  * Custom sprite support (a limited selection currently, but this will be expanded in a future minor release soon)
  * Custom heart colors 
  * Adjustable low heart beep speed
  * Low energy alarm toggle

* Gameplay/Logic Changes
  * Reserve tanks are now counted as progression items for logic.
  * Removed +10 bomb/arrow capacity upgrades
  * Removed 2x each +5 bomb/arrow capacity upgrades
  * Removed 10 bomb pickup
  * Replaced with 3x 10 arrow and 4x 3 bomb pickups
  * All bottles are empty when found
  * Waterfall/Pyramid Fairies only give green potions
  * Removed Charge requirement for Ridley/Mother Brain
  * Fixed Botwoon access in Hard logic, requiring Gravity Suit to use Speed Booster
  * Fixed Outer Maridia access via Maridia Portal, requiring the ability to break bomb blocks in Aqueduct (all logics) or climb Mainstreet after Green Gate Glitch (Hard logic)
  * Fixed Mama Turtle items to require the ability to open red doors
  * Fixed key logic in Ice Palace, preventing right side lockout 
  * Multiworld specific logic changes:
    * All "Progression pool" items banned from Ganon's Tower in multiworld seeds, even if not required. (Also items not your own)
    * Innate "early" bias for morph (first 20% of available checks) and moon pearl (first 40%) in multiworld seeds

#### Huge thanks to all contributors to Version 11
  * HalfARebel - A lot of work on the code rewrite and implementing custom sprite support.
  * Artheu - Creating the tools and code to enable custom sprites for Super Metroid.
  * qwertymodo - New great-looking custom title screen.
  * Lenophis. Osse101, Rushlight and TarThoron - Logic updates and fixes
  * All the beta testers for both single player and multiworld.
  * Everyone in the SMZ3 community for supporting and playing this randomizer.
  

## 2019-02-17 - Version 10.2
* This version fixes an issue with item placement where progression items ended up being significantly more likely to show up in ALTTP dungeons than would be expected.
* The Super Metroid map should now correctly mark visited areas as visited.
* The Super Metroid map has some graphical changes that indicates portals to A Link to the Past
* The item graphics for ALTTP items in Super Metroid has been overhauled to correct palette errors, add SM-like flashing and also includes color-blind friendly versions of otherwise similar items.
* Max ammo indicators have been added to Super Metroid.
* The music is now properly started when entering Maridia through the portal.
* The Special Beam Attacks are now properly tracked in the credits.
* Power Bombs and Bombs used are now properly tracked in the credits.
* The following changes has been done to SM Normal logic:
  * Jumping over the moat before Wrecked Ship with Spring Ball is no longer in logic.
  * Using Gravity Suit and either IBJ or Hi-Jump to get past the moat before Wrecked Ship is now in logic.

#### Huge thanks to all contributors to Version 10.2
  * Lenophis - New map graphics and porting Personitis max ammo display patch
  * qwertymodo - Fixing map corruption when teleporting between games
  * Natalie - New ALTTP item graphics in SM
  * NDIAZ - Fixing maridia portal music issue

## 2018-11-25 - Version 10.1
* Checkerboard cave now correctly has a requirement for gloves when accessed through the Lower Norfair to Misery Mire portal.
* Suitless mama turtle now always requires high-jump. (Hard mode)
* Right side upper norfair hellruns is now in logic with only speedbooster. (Hard mode)
* Golden Torizo has its logic fixed to require charge beam or super missiles to fight him.
* The logic no longer requires a short charge to escape Golden Torizo. (Normal mode)
* Ice beam is now an additional requirement to access suitless springball. (Hard mode)
* Some additional fixes has been done to the item pool allocations that could cause some item placements not to be possible.
* A seed identifier has been added to the file select screen before starting a new game.
* The bomb blocks in the "climb room" during the escape will now automatically be cleared preventing a potential softlock.

#### Huge thanks to all contributors for helping out with fixes and patches for V10.1

## 2018-07-27 - Version 10
* Dynamic text is now added back in, which means that checking pedestal and tables will now give a hint to what items is at those locations.
* Progressive items in the same room in SM now works properly, although the second item will show incorrect graphics.
* A bunch of general logic bugfixes that should fix a few weird possible edge cases.
* Super Metroid logic has been renamed and is now "Normal" and "Hard", where "Normal" is the old "Casual" and "Hard" is the old "Tournament"
* Normal logic has been reworked and should now be a bit more consistent in terms of required techniques.
* Hard logic has had a major rework and now includes a few new required tricks, details below:
  * The cross-game portal in maridia is now better implemented in logic, so there's now a chance for suitless maridia through the portal using only Morph and Hi-Jump or Spring Ball.
  * This also means that access to Wrecked Ship without Power Bombs is possible through the maridia portal and the "Forgotten Highway".
  * The green brinstar missiles furthest in behind the Reserve tank is now in logic with only Morph and Screw Attack.
  * Spring ball jumps have been added into logic in many new places, for example Red Tower, X-Ray room and more.
  * Hi-Jump missile back is now in logic with only Morph (Make sure to not enter the Hi-Jump room in this case or you will softlock)
  * Getting to the green door leading to Norfair reserve now assumes only the damage boost on a waver to get to it.
  * Lower Norfair item restrictions are now slightly reduced due to better use of the cross-game portal.
  * Snail clipping to the Missile and Super packs in Aqueduct is now in logic.
  * Spring Ball is now in logic suitless, assuming Hi-Jump, Space Jump, Spring Ball and Grapple.
  * All sand pit items are now in logic suitless with Hi-Jump, with the left sandpit items also requiring Space Jump or Spring Ball and the right sandpit PB's requiring Spring Ball.
* Back of desert in ALTTP can now be in logic without gloves using the Lower Norfair cross-game portal and Mirror.
* Checkerboard cave now correctly checks for access through the cross-game portal as well.

#### A recommended guide to the new V10 logic has been created by WildAnaconda69 and can be found here: https://www.twitch.tv/videos/286489494.

#### Huge thanks to all contributors for helping out with fixes and patches for V10.