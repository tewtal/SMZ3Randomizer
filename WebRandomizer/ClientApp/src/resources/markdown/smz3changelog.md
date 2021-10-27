# Randomizer Changes

## 2021-03-17 - Version 11.2

* Systems Changes
  * Keysanity support has been added as an option under the "Key Shuffle" header.
    * Enabling Keysanity will allow A Link to the Past Maps, Small Keys and Big Keys to be found anywhere.
    * It will also add a set of locked doors for Super Metroid that require new "Keycards" to be found before they can be opened.
    * More detailed information about this mode can be found at the [Information](/information) page.
  * The file select screen in A Link to the Past has been removed and Saving and Quitting will now go directly to the start location dialog.
  * Save files B and C in Super Metroid has been removed to prevent issues from selecting them since using them would cause the game to malfunction.
  * Added a lowercase font to the Super Metroid dialog boxes.
  * All Super Metroid map tiles should now be properly revealed from the start.

* Logic Changes
  * An issue with Wrecked Ship logic where it didn't properly check if bomb passages could be traversed has been fixed.
  * Forgotten Highway logic has been adjusted to allow for access through the "Toilet"
  * Accessing Crocomire from the Lower Norfair portal via reverse Lava Dive without Morph added to Normal.
  * A lot more logic changes done to account for Keysanity key door changes in Super Metroid.
    * There could still be logic issues, so if you find something that seems off please report it and we'll get it fixed as soon as we can.
  * The logic log has not yet been updated with these changes and will be updated in a future minor update.

* Technical Changes
  * Multiworld names should now preserve proper casing inside the game as well instead of being forced to CAPS.
  * The spoiler log search box now works properly with certain special characters.
  * Creating a race ROM now properly sets the race bit in the ROM.
  * The CLI tools have been updated to support generating keysanity seeds.
  * Seed generation performance has been optimized and runs quite a bit faster now.
  * The codebase has been updated from .NET 3.1 to .NET 5.0.
  * An issue where the randomizer could get stuck if someone tried to generate a multiworld seed with an excessive amount of player has been fixed.

#### Thanks to all contributors for helping out with fixes and patches for V11.2

## 2020-09-18 - Version 11.1

* Systems Changes
  * The selection of custom sprites has been extended to include 168 of the sprites (up from 9) from the ALttP randomizer
    v31.0.5, as well as 9 Samus sprites (up from 3).
  * The spoiler log has been enhanced
    * All locations are listed, grouped by area, as well as dungeon prizes and medallion requirements.
    * The log can be searched as well as downloaded.
  * ALTTP has a new text font and a more narrow spacing between letters which allow for 19 characters per line instead of 14.
  * A temporary light cone is shown on item pickup in dark rooms to make the item recieved visible.
  * The Lower Norfair energy refill room is reinstated, and the cross-game portal has been moved inside it to a new door on the right side.
  * Mother Brain phase 2 will now start faster similar to the Super Metroid randomizer.
  * It's now possible to register to multiple Multiworld sessions at the same time.

* Logic Changes
  * The top item location at West Ocean is now considered accessible without jump assist.
  * Allow for access to Wrecked Ship through Forgotten Highway with the following changes:
    * CanFly and Hi-Jump are removed from Gravity requirement through The Moat.
    * Screw Attack or defeating Draygon which enable Cactus Alley (together with Gravity) are added to access Forgotten Highway.
  * Access to Crocomire through Bubble Mountain for Normal logic was accidentally limited to Gravity through Volcano Room.
    CanPassBombPassages has been added as the alternative to Gravity to allow for the top -> bottom Bubble Mountain route.
  * The SM -> Misery Mire portal logic was missing correct logical access through Cathedral/Speedway for Hard logic.
  * The Space Jump location logic is simplified to only check if Draygon can be defeated. Hard logic mistakenly, but redundantly, had duplicate requirements.
  * The Logic Log has been updated accordingly.

* Technical Changes
  * The soft reset feature has been improved to be more safe to use. This means it's disabled where resetting could cause issues,
    such as during saving, during game intros and bootup and when the games are uploading audio data.
  * Multiworld sessions are now executing on a separate process. This will allow for most randomizer updates to not distrupt ongoing sessions.
    Any issues while using the multiworld session page *should* be simply a matter of refreshing the page and reconnecting.
  * Checks have been added to make sure the correct page is loaded depending on what randomizer (SM or SMZ3) a seed was generated with.
  * Best effort basic support for older browsers like IE/Edge have been added.
  * The multiworld instruction page has been updated.

#### Thanks to all contributors for helping out with fixes and patches for V11.1

## 2020-04-11 - Version 11.0

* Systems Changes
  * Complete rewrite from PHP to C#
  * Multiworld support (See the [Multiworld Instructions](/mwinstructions) page for more information)
    * Included Super Metroid multiworld support, and solo SM rando accessible at [Super Metroid Randomizer](https://sm.samus.link/)
  * Auto-stripping of unneeded headers from base roms
  * Added the [Logic Log](/logic) to the website that explains the Randomizer logic in detail.
  * The spoiler log currently only contains progression items (Spoiler log improvements are planned in a future update)
  * Information about the new filename structure can be found by hovering over the (i) icon next to the Download button
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
