# Multiworld Beta Instructions

This page will help you get setup and ready to play SMZ3 Multiworld.

The first step will be to download and prepare the proper tools and software, and then creating and
playing in a multiworld session.

## Required tools and software

Since the required software and other tools are different depending on if you're playing on console
with sd2snes or playing on emulator, skip ahead to the section that decribes the setup you will be
playing on.

### Console

#### SD2SNES

##### Required software:

* SD2SNES Firmware 1.10.3 [[Download](https://sd2snes.de/files/sd2snes_firmware_v1.10.3.zip)]
* USB2SNES v11 by RedGuyyyy [[Download](https://github.com/RedGuyyyy/sd2snes/releases/download/usb2snes_v11/usb2snes_v11.zip)]

##### Instructions:

1. Unzip the SD2SNES Firmware and place the "sd2snes"-folder on the SD-card (overwriting existing
   files, taking a backup of the folder before overwriting could be a good idea).
2. Unzip the USB2SNES Application and take the files from the "sd2snes"-folder and place into the
   "sd2snes" folder on the SD-card.
3. In the unzipped USB2SNES-folder, start "usb2snes.exe" to start the USB2SNES tray app that handles
   communication.
4. You should now be ready and setup to play.

### Emulators

##### Required software:

* QUSB2SNES v0.7.11 [[Download](https://github.com/Skarsnik/QUsb2snes/releases/download/v0.7.11/QUsb2Snes-v0.7.11.7z)]

#### Snes9x

If you want to play using Snes9x you'll need the following additional software:

* Snes9x-rr-1.55-multitroid2-r4 [[Download](https://drive.google.com/open?id=1xFw994dDl_yhwj0f0d1_nC9ZmX6wUFiE)]
* Multibridge_smz3_r2.lua [[Download](/lua/multibridge_smz3_r2.lua)]

##### Instructions:

1. Unzip QUSB2SNES and then run "QUSB2SNES.exe" to start the QUSB2SNES tray app.
2. Right click on the QUSB2SNES tray icon and under "Devices" select "Enable Lua bridge (snes9x-rr)"
3. Unzip Snes9x-rr and start it.
4. Go to (File > Lua scripting > New Lua script window) and load the "multibridge_smz3_r2.lua"
   script and keep the lua window open.
5. When ready, load the ROM you get from the randomizer.

#### Retroarch

If you want to play using Retroarch, you will need version 1.8.0.

* RetroArch 1.8.0 [[Download](https://www.retroarch.com/?page=platforms)]

##### Instructions:

1. Unzip QUSB2SNES and then run "QUSB2SNES.exe" to start the QUSB2SNES tray app.
2. Right click on the QUSB2SNES tray icon and under "Devices" select "Enable RetroArch virtual device"
3. In retroarch, first enable Advanced settings. (Settings -> User Interface -> Show Advanced Settings)
4. Go to (Settings -> Network) and turn on "Network commands"
5. Under Main Menu, go to (Load core > Download a Core) and install "Nintendo - SNES / Famicom
   (bsnes-mercury-balanced)"
6. When loading the multiworld ROM, make sure to load it using the bsnes-mercury-balanced core.

## Starting a multiworld session

1. To start a new multiworld session, go to the [Create randomized game](/randomizer) page and fill
   in the options you want to use for your session.
2. If you want to use a set seed (for example for races where you want to create multiple session
   using the same seed) you can fill it in, otherwise leave it blank for random.
3. When everything is setup just hit *Create Multiworld Game* and you will be redirected to a newly
   created multiworld session.
4. Copy the link of the page you got redirected to and share with the people you want to play with
   so they can join your session.

## Playing in a multiworld session

When you've just joined a new session you will first to have to register as a player by clicking
one of the *Register as this player* buttons.

After doing so, you will get a popup that prompts you to select and upload the original ROM's for
both Super Metroid and A Link to the Past (1.0 Japanese). Make sure these ROM's are unaltered and
are unheadered and that the correct ROM is uploaded to the correct slot because right now the only
way to go back and reupload is to clear your browser cache.

When the ROM's have been properly uploaded you will get the option to choose your two player
sprites as well as if you want to use separate animations for screwattack with and without
space jump in Super Metroid, and also a button to *Download ROM* which is your specific multiworld
ROM.

Download the ROM and after loading it with SD2SNES or an Emulator, make sure the USB2SNES/QUSB2SNES
software is running, any required lua scripts are running and then finally press "Connect". If
everything works as expected and you have the correct ROM loaded it'll say below "Game detected,
have fun" which means everything is ok and ready to go.
