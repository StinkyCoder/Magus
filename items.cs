
namespace Magus
{

    public class objectInfo
    {
        public string name;
        public short kind;
        public short weight;
        public short requires;
        public short icon;
        public short value;
        public short price;

        public objectInfo(string name, short kind, short weight, short requires,
                          short icon, short value, short price)
        {
            this.name = name;
            this.kind = kind;
            this.weight = weight;
            this.requires = requires;
            this.icon = icon;
            this.value = value;
            this.price = price;
        }
    };

    public class item
    {
        public short id;
        public short modifier;
        public char locked;
        public item next;
    };

    public class thing
    {
        public short x;
        public short y;
        public item items;
        public short backGround;
        public thing next;
    };


    public class items
    {
        public const int MAXOBJECTS = 91;

        public const int O_CLASS_WEAPON = 1;
        public const int O_CLASS_THROWINGWEAPON = 2;
        public const int O_CLASS_RANGEDWEAPON = 3;
        public const int O_CLASS_ARMOR = 4;
        public const int O_CLASS_SPELL = 5;
        public const int O_CLASS_MIXEDWEAPON = 6;
        public const int O_CLASS_GADGET = 7;
        public const int O_CLASS_TRINKET = 8;
        public const int O_CLASS_ARROWS = 9;
        public const int O_CLASS_SPECIAL = 10;
        public const int O_CLASS_POTION = 11;

        public const int O_REQ_NOTHING = 0;
        public const int O_REQ_ONEHAND = 1;
        public const int O_REQ_TWOHANDS = 2;
        public const int O_REQ_HEAD = 4;
        public const int O_REQ_BODY = 8;
        public const int O_REQ_HANDS = 16;
        public const int O_REQ_NECK = 32;
        public const int O_REQ_FINGER = 64;
        public const int O_REQ_SHOULDERS = 128;

        public const int O_SWORD = 1;
        public const int O_TWOHANDEDSWORD = 2;
        public const int O_AXE = 3;
        public const int O_DAGGER = 4;
        public const int O_BOW = 5;
        public const int O_CROSSBOW = 6;
        public const int O_THROWINGSTAR = 7;
        public const int O_CONFUSIONSPELL = 8;
        public const int O_FIREBLADE = 9;
        public const int O_HELMET = 10;
        public const int O_SHIELD = 11;
        public const int O_LEATHER = 12;
        public const int O_ARMOR = 13;
        public const int O_GREENAMULET = 14;
        public const int O_GREENRING = 15;
        public const int O_ARROWS = 16;
        public const int O_STUDDEDLEATHER = 17;
        public const int O_CHAINMAIL = 18;
        public const int O_LIGHTNINGSPELL = 19;
        public const int O_FIREBALLSPELL = 20;
        public const int O_TELEPORTSPELL = 21;
        public const int O_AIRSPELL = 22;
        public const int O_FIRESPELL = 23;
        public const int O_WATERSPELL = 24;
        public const int O_EARTHSPELL = 25;
        public const int O_SKELETONSPELL = 26;
        public const int O_ZOMBIESPELL = 27;
        public const int O_VISIONSPELL = 28;
        public const int O_PHANTOMSPELL = 29;
        public const int O_SLEEPSPELL = 30;
        public const int O_SLAYERSWORD = 31;
        public const int O_SPEEDSPELL = 32;
        public const int O_HEALINGSPELL = 33;
        public const int O_STONEAXE = 34;
        public const int O_GLOVES = 35;
        public const int O_DARKNESSSPELL = 36;
        public const int O_CUTLASS = 37;
        public const int O_STAFF = 38;
        public const int O_CHAOSSPELL = 39;
        public const int O_DEMONSPELL = 40;
        public const int O_CLOAK = 41;
        public const int O_LARGESHIELD = 42;
        public const int O_LARGEHELMET = 43;
        public const int O_BLUERING = 44;
        public const int O_MACE = 45;
        public const int O_YELLOWAMULET = 46;
        public const int O_BLUEAMULET = 47;
        public const int O_FREEZESPELL = 48;
        public const int O_ENERGYSPELL = 49;
        public const int O_DRAGONSTOOTH = 50;
        public const int O_FIREAXE = 51;
        public const int O_SILVERBOW = 52;
        public const int O_BUBBLE = 53;
        public const int O_FAITHFULARROW = 54;
        public const int O_SUNBOW = 55;
        public const int O_CHOCKHAMMER = 56;
        public const int O_BLOODTASTEAXE = 57;
        public const int O_GOLDENARMOR = 58;
        public const int O_SHADOWCLOAK = 59;
        public const int O_ELVENCLOAK = 60;
        public const int O_REDGLOVES = 61;
        public const int O_TERRORSPELL = 62;
        public const int O_BERZERKSPELL = 63;
        public const int O_WARDSPELL = 64;
        public const int O_LEADBALL = 65;
        public const int O_WOODENSHIELD = 66;
        public const int O_WAKIZASHI = 67;
        public const int O_GOLDENHELMET = 68;
        public const int O_GOLDENSHIELD = 69;
        public const int O_STORMSPELL = 70;
        public const int O_HYPERSPACESPELL = 71;
        public const int O_PANICSPELL = 72;
        public const int O_WISHINGSPELL = 73;
        public const int O_INFERNOSPELL = 74;
        public const int O_NEGATOR = 75;
        public const int O_CHAINANDBALL = 76;
        public const int O_GOLDENGLOVES = 77;
        public const int O_SLOWSPELL = 78;
        public const int O_GHOSTBLADE = 79;
        public const int O_ENCHANTSPELL = 80;
        public const int O_PURIFYSPELL = 81;
        public const int O_BLUEPOTION = 82;
        public const int O_GREENPOTION = 83;
        public const int O_CLEARPOTION = 84;
        public const int O_YELLOWPOTION = 85;
        public const int O_REDPOTION = 86;
        public const int O_GRAYPOTION = 87;
        public const int O_WHITEPOTION = 88;
        public const int O_PURPLEPOTION = 89;
        public const int O_DARKPOTION = 90;
        public objectInfo[] gObjectData;

        public items(){
        

            gObjectData = new  objectInfo[]{
                 new objectInfo("Dummy", 0, 0, 0, 0, 0, 0),
            new objectInfo("Sword", O_CLASS_WEAPON, 4, O_REQ_ONEHAND, 76, 9, 20),
            new objectInfo("2H-Sword", O_CLASS_WEAPON, 8, O_REQ_TWOHANDS, 74, 15, 75),
            new objectInfo("Axe", O_CLASS_WEAPON, 5, O_REQ_ONEHAND, 85, 10, 20),
            new objectInfo("Dagger", O_CLASS_MIXEDWEAPON, 1, O_REQ_ONEHAND, 88, 6, 10),
            new objectInfo("Bow", O_CLASS_RANGEDWEAPON, 3, O_REQ_TWOHANDS, 83, 9, 20),
            new objectInfo("Crossbow", O_CLASS_RANGEDWEAPON, 4, O_REQ_TWOHANDS, 82, 12, 20),
            new objectInfo("Throwing star", O_CLASS_THROWINGWEAPON, 1, O_REQ_ONEHAND, 89, 9, 5),
            new objectInfo("Confusion", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 93, 5, 0),
            new objectInfo("Fireblade", O_CLASS_SPECIAL, 2, O_REQ_ONEHAND, 173, 0, 150),
            new objectInfo("Helmet", O_CLASS_ARMOR, 3, O_REQ_HEAD, 91, 2, 20),
            new objectInfo("Shield", O_CLASS_ARMOR, 3, O_REQ_ONEHAND, 78, 4, 20),
            new objectInfo("Leather", O_CLASS_ARMOR, 3, O_REQ_BODY, 79, 2, 20),
            new objectInfo("Armour", O_CLASS_ARMOR, 7, O_REQ_BODY, 80, 6, 75),
            new objectInfo("Emerald", O_CLASS_GADGET, 1, O_REQ_NECK, 81, 0, 100),
            new objectInfo("Emerald ring", O_CLASS_GADGET, 0, O_REQ_FINGER, 90, 0, 150),
            new objectInfo("Arrows", O_CLASS_ARROWS, 0, O_REQ_NOTHING, 84, 0, 10),
            new objectInfo("Studded leather", O_CLASS_ARMOR, 4, O_REQ_BODY, 77, 3, 20),
            new objectInfo("Chainmail", O_CLASS_ARMOR, 6, O_REQ_BODY, 75, 5, 60),
            new objectInfo("Lightning bolt", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 55, 10, 0),
            new objectInfo("FireBall", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 60, 5, 0),
            new objectInfo("Portal", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 56, 7, 0),
            new objectInfo("Air", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 72, 7, 0),
            new objectInfo("Fire", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 70, 10, 0),
            new objectInfo("Water", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 68, 8, 0),
            new objectInfo("Earth", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 67, 8, 0),
            new objectInfo("Skeleton", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 64, 15, 0),
            new objectInfo("Zombie", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 65, 15, 0),
            new objectInfo("Vision", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 69, 2, 0),
            new objectInfo("Phantom", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 73, 3, 0),
            new objectInfo("Sleep", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 59, 8, 0),
            new objectInfo("Slayer", O_CLASS_WEAPON, 8, O_REQ_TWOHANDS, 122, 17, 150),
            new objectInfo("FastFeet", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 54, 8, 0),
            new objectInfo("Heal", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 53, 5, 20),
            new objectInfo("StoneAxe", O_CLASS_WEAPON, 10, O_REQ_TWOHANDS, 107, 15, 0),
            new objectInfo("Gloves", O_CLASS_ARMOR, 1, O_REQ_HANDS, 58, 1, 20),
            new objectInfo("Darkness", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 57, 10, 0),
            new objectInfo("Sabre", O_CLASS_WEAPON, 7, O_REQ_ONEHAND, 86, 10, 0),
            new objectInfo("Staff", O_CLASS_WEAPON, 3, O_REQ_TWOHANDS, 87, 8, 20),
            new objectInfo("Chaos", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 66, 20, 0),
            new objectInfo("Demon", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 63, 25, 0),
            new objectInfo("Cloak", O_CLASS_ARMOR, 1, O_REQ_SHOULDERS, 94, 1, 20),
            new objectInfo("Big shield", O_CLASS_ARMOR, 6, O_REQ_ONEHAND, 110, 6, 20),
            new objectInfo("Big helmet", O_CLASS_ARMOR, 4, O_REQ_HEAD, 111, 3, 20),
            new objectInfo("Opal ring", O_CLASS_GADGET, 0, O_REQ_FINGER, 97, 0, 150),
            new objectInfo("Club", O_CLASS_WEAPON, 5, O_REQ_ONEHAND, 119, 11, 20),
            new objectInfo("Topaz", O_CLASS_GADGET, 1, O_REQ_NECK, 123, 0, 300),
            new objectInfo("Opal", O_CLASS_GADGET, 1, O_REQ_NECK, 124, 0, 150),
            new objectInfo("Stonefoot", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 129, 6, 0),
            new objectInfo("Lightning", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 71, 15, 0),
            new objectInfo("DragonTooth", O_CLASS_WEAPON, 4, O_REQ_ONEHAND, 132, 13, 150),
            new objectInfo("Sun's Edge", O_CLASS_WEAPON, 5, O_REQ_ONEHAND, 133, 13, 150),
            new objectInfo("SilverBow", O_CLASS_RANGEDWEAPON, 3, O_REQ_TWOHANDS, 134, 11, 100),
            new objectInfo("Bubble", O_CLASS_TRINKET, 0, O_REQ_NOTHING, 92, 0, 150),
            new objectInfo("Faithful", O_CLASS_ARROWS, 0, O_REQ_NOTHING, 142, 0, 50),
            new objectInfo("SunBow", O_CLASS_SPECIAL, 3, O_REQ_TWOHANDS, 150, 11, 150),
            new objectInfo("Chock", O_CLASS_SPECIAL, 4, O_REQ_ONEHAND, 143, 2, 150),
            new objectInfo("BloodTaste", O_CLASS_WEAPON, 9, O_REQ_TWOHANDS, 144, 19, 200),
            new objectInfo("SunArmour", O_CLASS_ARMOR, 4, O_REQ_BODY, 149, 6, 150),
            new objectInfo("ShadowCloak", O_CLASS_ARMOR, 3, O_REQ_BODY | O_REQ_SHOULDERS, 148, 7, 20),
            new objectInfo("Elven cloak", O_CLASS_ARMOR, 1, O_REQ_SHOULDERS, 147, 3, 50),
            new objectInfo("Focus", O_CLASS_GADGET, 1, O_REQ_HANDS, 146, 0, 150),
            new objectInfo("Terror", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 153, 7, 0),
            new objectInfo("Berzerk", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 152, 3, 0),
            new objectInfo("Protection", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 151, 2, 0),
            new objectInfo("Leadball", O_CLASS_THROWINGWEAPON, 1, O_REQ_ONEHAND, 145, 8, 10),
            new objectInfo("Wooden shield", O_CLASS_ARMOR, 2, O_REQ_ONEHAND, 163, 3, 10),
            new objectInfo("Wakizashi", O_CLASS_WEAPON, 2, O_REQ_ONEHAND, 164, 9, 20),
            new objectInfo("SunHelmet", O_CLASS_ARMOR, 2, O_REQ_HEAD, 157, 3, 70),
            new objectInfo("SunShield", O_CLASS_ARMOR, 3, O_REQ_ONEHAND, 158, 6, 70),
            new objectInfo("ThunderStorm", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 159, 25, 0),
            new objectInfo("HyperSpace", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 160, 20, 0),
            new objectInfo("Panic", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 161, 20, 0),
            new objectInfo("Shooting star", O_CLASS_SPECIAL, 0, O_REQ_ONEHAND, 162, 0, 300),
            new objectInfo("Inferno", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 165, 12, 0),
            new objectInfo("Negator", O_CLASS_TRINKET, 3, O_REQ_NOTHING, 114, 0, 150),
            new objectInfo("Chain'n'ball", O_CLASS_WEAPON, 5, O_REQ_TWOHANDS, 170, 10, 0),
            new objectInfo("SunGloves", O_CLASS_ARMOR, 1, O_REQ_HANDS, 171, 2, 20),
            new objectInfo("Delay", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 172, 2, 0),
            new objectInfo("Ghostblade", O_CLASS_SPECIAL, 4, O_REQ_ONEHAND, 140, 9, 150),
            new objectInfo("Enchant", O_CLASS_SPECIAL, 0, O_REQ_ONEHAND, 178, 0, 100),
            new objectInfo("Purify", O_CLASS_SPELL, 0, O_REQ_ONEHAND, 179, 20, 0),
            new objectInfo("Blue", O_CLASS_POTION, 1, O_REQ_ONEHAND, 180, 0, 100),
            new objectInfo("Green", O_CLASS_POTION, 1, O_REQ_ONEHAND, 180, 0, 100),
            new objectInfo("Yellow", O_CLASS_POTION, 1, O_REQ_ONEHAND, 180, 0, 100),
            new objectInfo("Brown", O_CLASS_POTION, 1, O_REQ_ONEHAND, 180, 0, 100),
            new objectInfo("Red", O_CLASS_POTION, 1, O_REQ_ONEHAND, 180, 0, 100),
            new objectInfo("Grey", O_CLASS_POTION, 1, O_REQ_ONEHAND, 180, 0, 100),
            new objectInfo("White", O_CLASS_POTION, 1, O_REQ_ONEHAND, 180, 0, 100),
            new objectInfo("Purple", O_CLASS_POTION, 1, O_REQ_ONEHAND, 180, 0, 100),
            new objectInfo("Black", O_CLASS_POTION, 1, O_REQ_ONEHAND, 180, 0, 100) 
            };

        }
        public string GetItemDescription(item i, string s)
        {
            if (i is null) return "";

            if (i.id == O_ARROWS)
                return string.Format("%s [%d], weight %d", gObjectData[i.id].name, i.modifier, i.modifier * gObjectData[i.id].weight);
            else if (i.modifier == 0 || gObjectData[i.id].kind == O_CLASS_POTION || i.id == O_ENCHANTSPELL)
                return string.Format("%s, weight %d", gObjectData[i.id].name, gObjectData[i.id].weight);
            else
                return string.Format("%s %+d, weight %d", gObjectData[i.id].name, i.modifier, gObjectData[i.id].weight);
        }

    }
}