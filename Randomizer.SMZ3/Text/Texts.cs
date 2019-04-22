using System;
using System.Linq;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Text {

    class Texts {

        public static string ItemTextbox(Item item) {
            return item.Type switch {
                BigKeyEP => "The big key\nof the east",
                BigKeyDP => "Sand spills\nout of this\nbig key",
                BigKeyTH => "The big key\nto moldorm's\nheart",
                BigKeyPD => "Hammeryump\nwith this\nbig key",
                BigKeySP => "The big key\nto the swamp",
                BigKeySW => "The big key\nof the dark\nforest",
                BigKeyTT => "The big key\nof rogues",
                BigKeyIP => "A frozen\nbig key\nrests here",
                BigKeyMM => "The big key\nto Vitreous",
                BigKeyTR => "The big key\nof terrapins",
                BigKeyGT => "The big key\nof evil's bane",

                KeyHC => "The key to\nthe castle",
                KeyCT => "Agahanim\nhalfway\nunlocked",
                KeyDP => "Sand spills\nout of this\nsmall key",
                KeyTH => "The key\nto moldorm's\nbasement",
                KeyPD => "A small key\nthat steals\nlight",
                KeySP => "Access to\nthe swamp\nis granted",
                KeySW => "The small key\nof the dark\nforest",
                KeyTT => "The small key\nof rogues",
                KeyIP => "A frozen\nsmall key\nrests here",
                KeyMM => "The small key\nto Vitreous",
                KeyTR => "The small key\nof terrapins",
                KeyGT => "The small key\nof evil's bane",

                _ when item.IsMap => "You can now\nfind your way\nhome!",
                _ when item.IsCompass => "Now you know\nwhere the boss\nhides!",

                ProgressiveTunic =>
                    "time for a\nchange of\nclothes?",
                ProgressiveShield =>
                    "have a better\ndefense in\nfront of you",
                ProgressiveSword =>
                    "a better copy\nof your sword\nfor your time",
                Bow =>
                    "You have\nchosen the\narcher class.",
                SilverArrows =>
                    "Do you fancy\nsilver tipped\narrows?",
                BlueBoomerang =>
                    "No matter what\nyou do, blue\nreturns to you",
                RedBoomerang =>
                    "No matter what\nyou do, red\nreturns to you",
                Hookshot =>
                    "BOING!!!\nBOING!!!\nBOING!!!",
                Mushroom =>
                    "I'm a fun guy!\n\nI'm a funghi!",
                Powder =>
                    "you can turn\nanti-faeries\ninto faeries",
                Firerod =>
                    "I'm the hot\nrod. I make\nthings burn!",
                Icerod =>
                    "I'm the cold\nrod. I make\nthings freeze!",
                Bombos =>
                    "Burn, baby,\nburn! Fear my\nring of fire!",
                Ether =>
                    "This magic\ncoin freezes\neverything!",
                Quake =>
                    "Maxing out the\nRichter scale\nis what I do!",
                Lamp =>
                    "Baby, baby,\nbaby.\nLight my way!",
                Hammer =>
                    "stop\nhammer time!",
                Shovel =>
                    "Can\n   You\n      Dig it?",
                Flute =>
                    "Save the duck\nand fly to\nfreedom!",
                Bugnet =>
                    "Let's catch\nsome bees and\nfaeries!",
                Book =>
                    "This is a\nparadox?!",
                Bottle =>
                    "Now you can\nstore potions\nand stuff!",
                BottleWithRedPotion =>
                    "You see red\ngoo in a\nbottle?",
                BottleWithGreenPotion =>
                    "You see green\ngoo in a\nbottle?",
                BottleWithBluePotion =>
                    "You see blue\ngoo in a\nbottle?",
                var type when new[] { BottleWithGoldBee, BottleWithBee }.Contains(type) =>
                    "Release me\nso I can go\nbzzzzz!",
                BottleWithFairy =>
                    "If you die\nI will revive\nyou!",
                Somaria =>
                    "I make blocks\nto hold down\nswitches!",
                Byrna =>
                    "Use this to\nbecome\ninvincible!",
                Cape =>
                    "Wear this to\nbecome\ninvisible!",
                Mirror =>
                    "Isn't your\nreflection so\npretty?",
                Boots =>
                    "Gotta go fast!",
                ProgressiveGlove =>
                    "a way to lift\nheavier things",
                Flippers =>
                    "fancy a swim?",
                MoonPearl =>
                    "  Bunny Link\n      be\n     gone!",
                HalfMagic =>
                    "Your magic\npower has been\ndoubled!",
                HeartPiece =>
                    "Just a little\npiece of love!",
                var type when new[] { HeartContainerRefill, HeartContainer }.Contains(type) =>
                    "Maximum health\nincreased!\nYeah!",
                ThreeBombs =>
                    "I make things\ngo triple\nBOOM!!!",
                Arrow =>
                    "a lonely arrow\nsits here.",
                TenArrows =>
                    "This will give\nyou ten shots\nwith your bow!",
                var type when new[] { OneRupee, FiveRupees }.Contains(type) =>
                    "Just pocket\nchange. Move\nright along.",
                var type when new[] { TwentyRupees, TwentyRupees2, FiftyRupees }.Contains(type) =>
                    "Just couch\ncash. Move\nright along.",
                OneHundredRupees =>
                    "A rupee stash!\nHell yeah!",
                ThreeHundredRupees =>
                    "A rupee hoard!\nHell yeah!",
                var type when new[] { BombUpgrade5, BombUpgrade10 }.Contains(type) =>
                    "increase bomb\nstorage, low\nlow price",
                var type when new[] { ArrowUpgrade5, ArrowUpgrade10 }.Contains(type) =>
                    "increase arrow\nstorage, low\nlow price",
                Missile =>
                    "some kind of\nflying bomb?",
                Super =>
                    "a really big\nflying bomb!",
                PowerBomb =>
                    "Big bada boom!",
                Grapple =>
                    "Some kind of\nfuturistic\nhookshot?",
                XRay =>
                    "THIS LENS OF\nTRUTH IS MADE\nIN ZEBES!",
                ETank =>
                    "a heart from\nthe future?",
                ReserveTank =>
                    "a fairy from\nthe future?",
                Charge =>
                    "IM'A CHARGIN\nMA LAZER!",
                Ice =>
                    "some kind of\nice rod for\naliens?",
                Wave =>
                    "Trigonometry gun.",
                Spazer =>
                    "even space\nlasers can\nbe sucky.",
                Plasma =>
                    "some kind of\nfire rod for\naliens?",
                Varia =>
                    "Alien armor?",
                Gravity =>
                    "No more water\nphysics.",
                Morph =>
                    "Why can't\nMetroid crawl?",
                Bombs =>
                    "bombs from\nthe future.",
                SpringBall =>
                    "Bouncy bouncy\nbouncy bouncy\nbounce.",
                ScrewAttack =>
                    "U spin me right\nround baby\nright round",
                HiJump =>
                    "this would be\ngreat if I\ncould jump.",
                SpaceJump =>
                    "I believe\nI can fly.",
                SpeedBooster =>
                    "THE GREEN\nBOOMERANG IS\nTHE FASTEST!",
                _ =>
                    "Don't waste\nyour time!",
            };
        }

    }

}
