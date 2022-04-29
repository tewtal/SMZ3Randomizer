# Randomizer Changes

## 2022-05-01 - Version 3.1

* Systems Changes
  * The web site has a new favicon!
  * Multiworld network stability have been improved together with the ability
    to unregister, forfeit, and resend items. Currently forfeiting distributes
    others items in that player's world. However, this is planned to change in
    a later version.
  * The downloadable spoiler log has been improved and is now in a more human-readable
    YAML format and has areas sorted by "logical" progression.
  * A new set of Super Metroid custom sprites have been added.

* Logic Changes
  * The logic for the two "Alpha PB" item locations were swapped.
  * The "Hi-Jump Boots" location were missing a CanPassBombPassages.

* Technical Changes
  * Both randomizers now respect the multiworld setting over the player count.
    Previously SM only would force a player count of 1 to be a single world seed.

## 2020-03-16 - Version 3.0.1

* Moat logic has been updated to include options with Gravity + (Hi Jump or
  Bombs), as well as removing some logic requirement from Moat Missiles.

## 2020-04-11 - Version 3.0
* This randomizer is based on the [Super Metroid Item Randomizer 2.10](https://itemrando.supermetroid.run/).
  It has the *Casual* and *Tournament* logic levels implemented with the same
  logic as the version 2.10 randomizer.
* In addition it has the following features:
  * Multiworld support (See the [Multiworld Instructions](/mwinstructions) page
    for more information).
  * Custom selection of Samus sprite.
  * Low energy alarm toggle.
  * Full / Split item filling separate from Logic levels.
  * Spoiler log available for non-race seeds.
  * The ability to link permalinks of generated seeds for others to play.
