using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.SMZ3.Regions.Zelda;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Text {

    class Texts {

        public static string SahasrahlaReveal(Region dungeon) {
            return "Want something\nfor free? Go\nearn the green\npendant in\n<dungeon>\nand I'll give\nyou something."
                .Replace("<dungeon>", dungeon.Area);
        }

        public static string BombShopReveal(IEnumerable<Region> dungeons) {
            var (first, second, _) = dungeons;
            return "Bring me the\ncrystals from\n<first>\nand\n<second>\nso I can make\na big bomb!"
                .Replace("<first>", first.Area)
                .Replace("<second>", second.Area);
        }

        public static string Blind(Random rnd) {
            return RandomLine(rnd, new[] {
                "I hate insect\npuns, they\nreally bug me.",
                "I haven't seen\nthe eye doctor\nin years.",
                "I don't see\nyou having a\nbright future",
                "Are you doing\na blind run\nof this game?",
                "Pizza joke? No\nI think it's a\nbit too cheesy",
                "A novice skier\noften jumps to\ncontusions.",
                "The beach?\nI'm not shore\nI can make it.",
                "Rental agents\noffer quarters\nfor dollars.",
                "I got my tires\nfixed for a\nflat rate.",
                "New lightbulb\ninvented?\nEnlighten me.",
                "A baker's job\nis a piece of\ncake.",
                "My optometrist\nsaid I have\nvision!",
                "When you're a\nbaker, don't\nloaf around",
                "Mire requires\nether quake,\nor bombos",
                "Broken pencils\nare pointless.",
                "The food they\nserve guards\nlasts sentries",
                "Being crushed\nby big objects\nis depressing.",
                "A tap dancer's\nroutine runs\nhot and cold.",
                "A weeknight\nis a tiny\nnobleman",
                "The chimney\nsweep wore a\nsoot and tye.",
                "Gardeners like\nto spring into\naction.",
                "Bad at nuclear\nphysics. I Got\nno fission.",
            });
        }

        public static string TavernMan(Random rnd) {
            return RandomLine(rnd, new[] {
                "What do you\ncall a blind\ndinosaur?\nAdoyouthink-\nhesaurus\n",
                "A blind man\nwalks into\na bar.\nAnd a table.\nAnd a chair.\n",
                "What do ducks\nlike to eat?\n\nQuackers!\n",
                "How do you\nset up a party\nin space?\n\nYou planet!\n",
                "I'm glad I\nknow sign\nlanguage,\nit's pretty\nhandy.\n",
                "What did Zelda\nsay to Link at\na secure door?\n\nTRIFORCE!\n",
                "I am on a\nseafood diet.\n\nEvery time\nI see food,\nI eat it.",
                "I've decided\nto sell my\nvacuum.\nIt was just\ngathering\ndust.",
                "Whats the best\ntime to go to\nthe dentist?\n\nTooth-hurtie!\n",
                "Why can't a\nbike stand on\nits own?\n\nIt's two-tired!\n",
                "If you haven't\nfound Quake\nyet…\nit's not your\nfault.",
                "Why is Peter\nPan always\nflying?\nBecause he\nNeverlands!",
                "I once told a\njoke to Armos.\n\nBut he\nremained\nstone-faced!",
                "Lanmola was\nlate to our\ndinner party.\nHe just came\nfor the desert",
                "Moldorm is\nsuch a\nprankster.\nAnd I fall for\nit every time!",
                "Helmasaur is\nthrowing a\nparty.\nI hope it's\na masquerade!",
                "I'd like to\nknow Arrghus\nbetter.\nBut he won't\ncome out of\nhis shell!",
                "Mothula didn't\nhave much fun\nat the party.\nHe's immune to\nspiked punch!",
                "Don't set me\nup with that\nchick from\nSteve's Town.\n\n\nI'm not\ninterested in\na Blind date!",
                "Kholdstare is\nafraid to go\nto the circus.\nHungry kids\nthought he was\ncotton candy!",
                "I asked who\nVitreous' best\nfriends are.\nHe said,\n'Me, Myself,\nand Eye!'",
                "Trinexx can be\na hothead or\nhe can be an\nice guy. In\nthe end, he's\na solid\nindividual!",
                "Bari thought I\nhad moved out\nof town.\nHe was shocked\nto see me!",
                "I can only get\nWeetabix\naround here.\nI have to go\nto Steve's\nTown for Count\nChocula!",
                "Don't argue\nwith a frozen\nDeadrock.\nHe'll never\nchange his\nposition!",
                "I offered a\ndrink to a\nself-loathing\nGhini.\nHe said he\ndidn't like\nspirits!",
                "I was supposed\nto meet Gibdo\nfor lunch.\nBut he got\nwrapped up in\nsomething!",
                "Goriya sure\nhas changed\nin this game.\nI hope he\ncomes back\naround!",
                "Hinox actually\nwants to be a\nlawyer.\nToo bad he\nbombed the\nBar exam!",
                "I'm surprised\nMoblin's tusks\nare so gross.\nHe always has\nhis Trident\nwith him!",
                "Don’t tell\nStalfos I’m\nhere.\nHe has a bone\nto pick with\nme!",
                "I got\nWallmaster to\nhelp me move\nfurniture.\nHe was really\nhandy!",
                "Wizzrobe was\njust here.\nHe always\nvanishes right\nbefore we get\nthe check!",
                "I shouldn't\nhave picked up\nZora's tab.\nThat guy\ndrinks like\na fish!",
                "I was sharing\na drink with\nPoe.\nFor no reason,\nhe left in a\nheartbeat!",
                "Don’t trust\nhorsemen on\nDeath Mountain\nThey’re Lynel\nthe time!",
                "Today's\nspecial is\nbattered bat.\nGot slapped\nfor offering a\nlady a Keese!",
                "Don’t walk\nunder\npropellered\npineapples.\nYou may end up\nwearing\na pee hat!",
                "My girlfriend\nburrowed under\nthe sand.\nSo I decided\nto Leever!",
                "Geldman wants\nto be a\nBroadway star.\nHe’s always\npracticing\nJazz Hands!",
                "Octoballoon\nmust be mad\nat me.\nHe blows up\nat the sight\nof me!",
                "Toppo is a\ntotal pothead.\n\nHe hates it\nwhen you take\naway his grass",
                "I lost my\nshield by\nthat house.\nWhy did they\nput up a\nPikit fence?!",
                "Know that fox\nin Steve’s\nTown?\nHe’ll Pikku\npockets if you\naren't careful",
                "Dash through\nDark World\nbushes.\nYou’ll see\nGanon is tryin\nto Stal you!",
                "Eyegore!\n\nYou gore!\nWe all gore\nthose jerks\nwith arrows!",
                "I like my\nwhiskey neat.\n\nSome prefer it\nOctoroks!",
                "I consoled\nFreezor over a\ncup of coffee.\nHis problems\njust seemed to\nmelt away!",
                "Magic droplets\nof water don’t\nshut up.\nThey just\nKyameron!",
                "I bought hot\nwings for\nSluggula.\nThey gave him\nexplosive\ndiarrhea!",
                "Hardhat Beetle\nwon’t\nLet It Be?\nTell it to Get\nBack or give\nit a Ticket to\nRide down\na hole!",
            });
        }

        public static string GanonFirstPhase(Random rnd) {
            return RandomLine(rnd, new[] {
                "Start your day\nsmiling with a\ndelicious\nwholegrain\nbreakfast\ncreated for\nyour\nincredible\ninsides.",
                "You drove\naway my other\nself, Agahnim\ntwo times…\nBut, I won't\ngive you the\nTriforce.\nI'll defeat\nyou!",
                "Impa says that\nthe mark on\nyour hand\nmeans that you\nare the hero\nchosen to\nawaken Zelda.\nyour blood can\nresurrect me.",
                "Don't stand,\n\ndon't stand so\nDon't stand so\n\nclose to me\nDon't stand so\nclose to me\nback off buddy",
                "So ya\nThought ya\nMight like to\ngo to the show\nTo feel the\nwarm thrill of\nconfusion\nThat space\ncadet glow.",
                "Like other\npulmonate land\ngastropods,\nthe majority\nof land slugs\nhave two pairs\nof 'feelers'\nor tentacles\non their head.",
                "If you were a\nburrito, what\nkind of a\nburrito would\nyou be?\nMe, I fancy I\nwould be a\nspicy barbacoa\nburrito.",
                "I am your\nfather's\nbrother's\nnephew's\ncousin's\nformer\nroommate. What\ndoes that make\nus, you ask?",
                "I'll be more\neager about\nencouraging\nthinking\noutside the\nbox when there\nis evidence of\nany thinking\ninside it.",
                "If we're not\nmeant to have\nmidnight\nsnacks, then\nwhy is there\na light in the\nfridge?\n",
                "I feel like we\nkeep ending up\nhere.\n\nDon't you?\n\nIt's like\ndeja vu\nall over again",
                "Did you know?\nThe biggest\nand heaviest\ncheese ever\nproduced\nweighed\n57,518 pounds\nand was 32\nfeet long.",
                "Now there was\na time, When\nyou loved me\nso. I couldn't\ndo wrong,\nAnd now you\nneed to know.\nSo How you\nlike me now?",
                "Did you know?\nNutrition\nexperts\nrecommend that\nat least half\nof our daily\ngrains come\nfrom whole\ngrain products",
                "The Hemiptera\nor true bugs\nare an order\nof insects\ncovering 50k\nto 80k species\nlike aphids,\ncicadas, and\nshield bugs.",
                "Thanks for\ndropping in,\nthe first\npassengers\nin a hot\nair balloon.\nwere a duck,\na sheep,\nand a rooster.",
                "You think you\nare so smart?\n\nI bet you\ndidn't know\nYou can't hum\nwhile holding\nyour nose\nclosed.",
                "Grumble,\n\ngrumble…\ngrumble,\n\ngrumble…\nSeriously you\nwere supposed\nto bring food",
                "Join me hero,\nand I shall\nmake your face\nthe greatest\nin the dark\nworld!\n\nOr else you\nwill die!",
            });
        }

        public static string GanonThirdPhaseSingle(Location silvers) {
            if (silvers.Region is GanonsTower)
                return "Did you find\nthe arrows in\nmy tower?";
            return $"Did you find\nthe arrows in\n{silvers.Region.Area}";
        }

        public static string GanonThirdPhaseMulti(Location silvers, World myWorld) {
            if (silvers.Region.World == myWorld)
                return "Seek the\narrows in\nthis world";
            var player = silvers.Region.World.Player;
            player = player.PadLeft(7 + player.Length / 2);
            return $"Seek the sage\n{player}\nfor the arrows";
        }

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
                    "Time for a\nchange of\nclothes?",
                ProgressiveShield =>
                    "Have a better\ndefense in\nfront of you",
                ProgressiveSword =>
                    "A better copy\nof your sword\nfor your time",
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
                    "You can turn\nanti-faeries\ninto faeries",
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
                    "Stop!\nHammer time!",
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
                    "A way to lift\nheavier things",
                Flippers =>
                    "Fancy a swim?",
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
                    "A lonely arrow\nsits here.",
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
                    "Increase bomb\nstorage, low\nlow price",
                var type when new[] { ArrowUpgrade5, ArrowUpgrade10 }.Contains(type) =>
                    "Increase arrow\nstorage, low\nlow price",
                Missile =>
                    "Some kind of\nflying bomb?",
                Super =>
                    "A really big\nflying bomb!",
                PowerBomb =>
                    "Big bada boom!",
                Grapple =>
                    "Some kind of\nfuturistic\nhookshot?",
                XRay =>
                    "THIS LENS OF\nTRUTH IS MADE\nIN ZEBES!",
                ETank =>
                    "A heart from\nthe future?",
                ReserveTank =>
                    "A fairy from\nthe future?",
                Charge =>
                    "IM'A CHARGIN\nMA LAZER!",
                Ice =>
                    "Some kind of\nice rod for\naliens?",
                Wave =>
                    "Trigonometry gun.",
                Spazer =>
                    "Even space\nlasers can\nbe sucky.",
                Plasma =>
                    "Some kind of\nfire rod for\naliens?",
                Varia =>
                    "Alien armor?",
                Gravity =>
                    "No more water\nphysics.",
                Morph =>
                    "Why can't\nMetroid crawl?",
                Bombs =>
                    "Bombs from\nthe future.",
                SpringBall =>
                    "Bouncy bouncy\nbouncy bouncy\nbounce.",
                ScrewAttack =>
                    "U spin me right\nround baby\nright round",
                HiJump =>
                    "This would be\ngreat if I\ncould jump.",
                SpaceJump =>
                    "I believe\nI can fly.",
                SpeedBooster =>
                    "THE GREEN\nBOOMERANG IS\nTHE FASTEST!",
                _ =>
                    "Don't waste\nyour time!",
            };
        }

        static string RandomLine(Random rnd, IList<string> lines) {
            return lines[rnd.Next(lines.Count)];
        }

    }

}
