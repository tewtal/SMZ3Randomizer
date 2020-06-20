# Multiworld Instructions

This page will help you get set up and ready to play Multiworld Randomizer.

## What is Multiworld

Multiworld is an online cooperative game mode where multiple players play at
the same time where items in one player's game can be found in one of the other
players' games. And by finding items for each other you will eventually be able
to all complete the game.

For example: Player A opens a chest to find an item. This item belongs to
Player B and therefore enters Player B's inventory immediately.

## Getting started

The next step will be to download and prepare the proper tools and software,
and then creating and playing in a multiworld session.

## Required tools and software

Since the required software and other tools are different depending on if
you're playing on console with sd2snes or playing on emulator, skip ahead to
the section that describes the setup you will be playing on.

### Console

#### SD2SNES

##### Required software:

* SD2SNES Firmware 1.10.3 [[Download](https://sd2snes.de/files/sd2snes_firmware_v1.10.3.zip)]
* USB2SNES v11 by RedGuyyyy [[Download](https://github.com/RedGuyyyy/sd2snes/releases/download/usb2snes_v11/usb2snes_v11.zip)]

##### Instructions:

1. Unzip the SD2SNES Firmware and place the "sd2snes"-folder on the SD-card
   (overwriting existing files, taking a backup of the folder before
   overwriting could be a good idea).
2. Unzip the USB2SNES Application and take the files from the "sd2snes"-folder
   and place into the "sd2snes" folder on the SD-card.
3. In the unzipped USB2SNES-folder, start "usb2snes.exe" to start the USB2SNES
   tray app that handles communication.
4. You should now be ready and set up to play.

### Emulators

##### Required software:

* QUSB2SNES v0.7.16.1 [[Download](https://github.com/Skarsnik/QUsb2snes/releases/download/v0.7.16.1/QUsb2Snes-v0.7.16.1.7z)]

#### Snes9x

If you want to play using Snes9x you'll need the following additional software:

* Snes9x-rr-1.55-multitroid2-r4 [[Download](https://drive.google.com/open?id=1xFw994dDl_yhwj0f0d1_nC9ZmX6wUFiE)]
    * Combo: multibridge_smz3_r2.lua [[Download](/lua/multibridge_smz3_r2.lua)]
    * SM: The included multibridge.lua

##### Instructions:

1. Unzip QUSB2SNES and then run "QUSB2SNES.exe" to start the QUSB2SNES tray app.
2. Right click on the QUSB2SNES tray icon and under "Devices" select "Enable Lua bridge (snes9x-rr)".
3. Unzip Snes9x-rr and start it.
4. Go to File -> Lua scripting -> New Lua script window.
    * If playing Combo Randomizer, load "multibridge_smz3_r2.lua".
    * If playing Super Metroid Randomizer, load "multibridge.lua".
5. When ready, load the ROM you get from the randomizer.

#### Retroarch

If you want to play using Retroarch, you will need version 1.8.0 or newer.

* RetroArch [[Download](https://www.retroarch.com/?page=platforms)]

##### Instructions:

1. Unzip QUSB2SNES and then run "QUSB2SNES.exe" to start the QUSB2SNES tray app.
2. Right click on the QUSB2SNES tray icon and under "Devices" select "Enable RetroArch virtual device".
3. In retroarch, first enable Advanced settings. (Settings -> User Interface -> Show Advanced Settings).
4. Go to (Settings -> Network) and turn on "Network commands".
5. Under Main Menu, go to (Load core > Download a Core) and install
   "Nintendo - SNES / Famicom (bsnes-mercury-balanced)".
6. When loading the multiworld ROM, make sure to load it using the bsnes-mercury-balanced core.

## Starting a multiworld session

1. To start a new multiworld session, go to the "Generate Randomized Game" page
   and fill in the options you want to use for your session. Make sure to set
   the "Game Mode" option to "Multiworld" to create a multiworld game and to be
   able to enter the amount of players and their names.
2. If you want to use a set seed (for example for races where you want to
   create multiple session using the same seed) you can fill it in, otherwise
   leave it blank for random.
3. When everything is setup just hit *Generate Game* and you will be redirected
   to a newly created multiworld session.
4. Copy the link of the page you got redirected to and share with the people
   you want to play with so they can join your session.

## Playing in a multiworld session

When you've just joined a new session you will first to have to register as a
player by clicking one of the *Register as this player* buttons.

***Please note, you are only able to register for one session at a time per browser. Registering for a new session will lose the connection to the old session. Additionally, incognito browser registrations will be lost if you close the window. Once registration is lost for a session, you will be unable to recover it.***

After doing so, you will get a popup that prompts you to select the original
ROM(s) needed. Make sure these ROM's are unaltered and are the original
versions of the games. For the A Link to the Past ROM in a Combo Randomizer
game, make sure it is the Japanese 1.0 version.

When the ROM's have been properly uploaded you will get the option to choose
your two player sprites as well as if you want to use separate animations for
screwattack with and without space jump in Super Metroid, and also a button to
*Download ROM* which is your specific multiworld ROM.

Download the ROM and after loading it with SD2SNES or an Emulator, make sure
the USB2SNES/QUSB2SNES software is running, any required lua scripts are
running, that you have reached the File Select screen, and then finally press
"Connect". If everything works as expected and you have the correct ROM loaded
it'll say below "Game detected, have fun" which means everything is ok and
ready to go.

Hard reset, either through your console or emulator, severs the connection to
the server. Instead the ROM's provide an in-game soft reset feature which is
activated by pressing L+select+start+R.

## Softlock Edge Cases

### Tourian Point of No Return Save

If you have already completed LTTP and save at the final Tourian save point, you will be no longer be able to access either game and can perform no further item checks. If other players are dependent on an item from your world to complete their game, they will be locked out of completion. Make sure to either skip this save point, beat SM first, or wait for all players to have the items they need to finish to prevent this problem.
