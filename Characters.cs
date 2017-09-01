namespace Magus
{
    public class CharacterClass
    {
        public string name;
        public short speed;
        public short strength;
        public short wisdom;
        public short skill;
        public short health;
        public short power;
        public short canWalk;
        public short maleIcon;
        public short femaleIcon;
        public short lifeSpan;
        public short rank;
        public int[] inventory; //short[MAXINVENTORY]

        public CharacterClass(string name, short speed, short strength, short wisdom, short skill,
                              short health, short power, short canWalk, short maleIcon, short femaleIcon,
                              short lifeSpan, short rank, int[] inventory)
        {
            this.name = name;
            this.speed = speed;
            this.strength = strength;
            this.wisdom = wisdom;
            this.skill = skill;
            this.health = health;
            this.power = power;
            this.canWalk = canWalk;
            this.maleIcon = maleIcon;
            this.femaleIcon = femaleIcon;
            this.lifeSpan = lifeSpan;
            this.rank = rank;
            this.inventory = inventory;
        }
    };

    public class Characters
    {
        public const int MAXCLASSES = 43;
        public const int SELECTABLECLASSES = 9;
        public const int MAXINVENTORY = 10;
        public const int MAXLEVEL = 11;
        public const int PERSISTENT = 4711;
        public string[] gLevelNames =
        { "Nobody",
        "Amateur",
        "Novice",
        "Apprentice",
        "Trained",
        "Good",
        "Experienced",
        "Expert",
        "Elite",
        "Master",
        "ArchMaster",
        "Legend"
        };

        public int[] gLevelLimits =
        { 50,
        100,
        200,
        300,
        400,
        500,
        700,
        900,
        1200,
        1500,
        2000,
        2000
        };

        public CharacterClass[] gClassData;

        public Characters(items items)
        {

            gClassData = new CharacterClass[]{
  new CharacterClass( "Shaman", 6, 9, 15, 7, 30, 40, 0x59DB, 26, 27, 0, PERSISTENT,
  new int[]{ items.O_ELVENCLOAK, items.O_STAFF, items.O_HEALINGSPELL, items.O_PHANTOMSPELL, items.O_SPEEDSPELL, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Wizard", 4, 12, 12, 10, 50, 50, 0x51CB, 23, 31, 0, PERSISTENT,
   new int[]{  items.O_CLOAK, items.O_DAGGER, items.O_FIREBALLSPELL, items.O_TELEPORTSPELL, items.O_VISIONSPELL, 0, 0, 0, 0, 0 }),
   new CharacterClass( "Elf", 6, 14, 0, 15, 90, 0, 0x59DB, 116, 117, 0, PERSISTENT,
   new int[]{  60, 4, 5, 16, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass("Barbarian", 4, 17, 0, 15, 120, 0, 0x51CB, 19, 35, 0, PERSISTENT,
   new int[]{  45, 17, 5, 16, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Knight", 4, 24, 0, 11, 90, 0, 0x51CB, 25, 47, 0, PERSISTENT,
   new int[]{  1, 18, 11, 10, 0, 0, 0, 0, 0, 0 }),
   new CharacterClass( "Duck", 5, 14, 0, 17, 100, 0, 0x51DF, 108, 108, 0, PERSISTENT,
   new int[]{  items.O_MACE, items.O_LEATHER, items.O_GLOVES, items.O_BOW, items.O_ARROWS, 0, 0, 0, 0, 0 }),
   new CharacterClass( "Dwarf", 4, 20, 0, 12, 120, 0, 0x51CB, 126, 174, 0, PERSISTENT,
   new int[]{  3, 17, 35, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Druid", 4, 14, 10, 10, 80, 20, 0x51CB, 127, 175, 0, PERSISTENT,
   new int[]{  items.O_CLOAK, items.O_MACE, items.O_FIRESPELL, items.O_AIRSPELL, items.O_WATERSPELL, items.O_EARTHSPELL, 0, 0, 0, 0 } ),
   new CharacterClass( "Duck mage", 5, 10, 12, 9, 40, 60, 0x51DF, 141, 141, 0, PERSISTENT,
   new int[]{  items.O_CLOAK, items.O_DAGGER, items.O_CONFUSIONSPELL, items.O_CHAOSSPELL, items.O_FREEZESPELL, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Orch", 4, 14, 0, 9, 50, 0, 0x51CB, 42, 42, 0, 40,
   new int[]{  12, 37, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Orch", 4, 15, 0, 13, 60, 0, 0x51CB, 43, 43, 0, 80,
   new int[]{  18, 3, 11, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Orch", 4, 12, 12, 5, 40, 20, 0x51CB, 44, 44, 0, 80,
    new int[]{  20, 30, 26, 27, 37, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "ChaosKnight", 4, 22, 0, 16, 100, 0, 0x51CB, 36, 36, 0, 100,
   new int[]{  43, 13, 2, 35, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Spirit", 5, 50, 15, 15, 40, 10, 0x7FFF, 24, 24, 0, 120,
   new int[]{ 0x1000 | items.O_CHAOSSPELL, 0x1000 | items.O_GHOSTBLADE, 0x1000 | items.O_SLEEPSPELL,      0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Ghost", 4, 10, 10, 15, 20, 10, 0x7FFF, 33, 33, 0, 100,
    new int[]{  0x1000 | items.O_CONFUSIONSPELL, 0x1000 | items.O_CHAINANDBALL, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "DeathKnight", 4, 40, 20, 20, 200, 20, 0x51CB, 37, 37, 0, 170,
    new int[]{  42, 3, 13, 10, 20, 35, 75, 0, 0, 0 } ),
   new CharacterClass( "Skeleton", 4, 7, 0, 7, 30, 0, 0x51CB, 32, 32, 0, 0,
    new int[]{  items.O_CHAINANDBALL, 0, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Zombie", 4, 12, 0, 10, 10, 0, 0x51CB, 20, 20, 0, 0,
    new int[]{  items.O_CUTLASS, 0, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Djinn", 14, 1, 0, 0, 1, 0, 0x51DF, 18, 18, 8, PERSISTENT,
    new int[]{  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Fenix", 5, 10, 19, 17, 40, 5, 0x51C3, 34, 34, 2, PERSISTENT,
    new int[]{  0x1000 | items.O_FIREBLADE, 0, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Undine", 6, 20, 0, 15, 30, 0, 0x51DF, 29, 29, 6, PERSISTENT,
    new int[]{  0x1000 | items.O_CUTLASS, 0, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Golem", 3, 60, 0, 10, 80, 0, 0x51CB, 40, 40, 5, PERSISTENT,
    new int[]{  0x1000 | items.O_STONEAXE, 0, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Demon", 6, 50, 10, 17, 200, 20, 0x7FFF, 39, 39, 9, PERSISTENT,
    new int[]{  0x1000 | items.O_FIREBALLSPELL, 0x1000 | items.O_FIREBLADE, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Sorcerer", 5, 25, 20, 20, 150, 150, 0x51CB, 41, 41, 0, 120,
    new int[]{
    items.O_CHAINMAIL, items.O_GREENRING, items.O_FIRESPELL, items.O_DEMONSPELL, items.O_ENERGYSPELL,
      items.O_SLEEPSPELL, items.O_LIGHTNINGSPELL, items.O_TWOHANDEDSWORD, items.O_NEGATOR, items.O_FREEZESPELL } ),
   new CharacterClass( "Hobgoblin", 5, 10, 0, 9, 10, 0, 0x59DB, 115, 115, 0, 10,
    new int[]{  7, 4, 4, 41, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Minotaur", 4, 20, 0, 12, 80, 0, 0x51CB, 109, 109, 0, 100,
    new int[]{  17, 45, 42, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Witchlord", 5, 20, 20, 20, 100, 100, 0x51CB, 118, 118, 0, 140,
    new int[]{
    items.O_LIGHTNINGSPELL, items.O_FIRESPELL, items.O_SHADOWCLOAK, items.O_FIREBLADE, items.O_FIREBALLSPELL,
      items.O_CONFUSIONSPELL, items.O_GREENRING, items.O_NEGATOR, 0, 0 } ),
   new CharacterClass( "Magician", 4, 12, 10, 10, 50, 30, 0x51CB, 22, 22, 0, 50,
    new int[]{  6, 36, 8, 41, 16, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Shadowbeast", 5, 20, 0, 10, 200, 0, 0x51DF, 28, 28, 3, PERSISTENT,
    new int[]{  0x1000 | items.O_MACE, 0, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Lightning", 8, 4, 14, 0, 1, 10, 0x51DF, 21, 21, 1, PERSISTENT,
    new int[]{  0x1000 | items.O_CHOCKHAMMER, 0, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Undead", 5, 17, 16, 20, 70, 15, 0x51CB, 38, 38, 0, 100,
    new int[]{  20, 27, 45, 17, 15, 26, 0, 0, 0, 0 } ),
   new CharacterClass( "Troll", 3, 20, 0, 7, 100, 0, 0x51CB, 17, 17, 0, 20,
    new int[]{  items.O_STONEAXE, 0, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Monk", 4, 14, 11, 16, 60, 20, 0x51CB, 125, 125, 0, 60,
    new int[]{  12, 38, 25, 24, 78, 23, 0, 0, 0, 0 } ),
   new CharacterClass( "Goblin", 4, 8, 0, 14, 30, 0, 0x51CB, 128, 128, 0, 10,
    new int[]{  items.O_SWORD, items.O_LEATHER, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Warrior", 4, 12, 0, 14, 80, 0, 0x51CB, 135, 135, 0, 60,
    new int[]{  items.O_MACE, items.O_CHAINMAIL, 7, 7, 7, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "ChaosWarrior", 4, 30, 0, 19, 300, 0, 0x51CB, 136, 136, 0, 200,
    new int[]{  items.O_LARGEHELMET, items.O_TWOHANDEDSWORD, items.O_GLOVES, items.O_ARMOR, items.O_CLOAK, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "DemonPrince", 6, 30, 0, 20, 500, 0, 0x51CB, 137, 137, 0, PERSISTENT,
   new int[]{
    items.O_GOLDENARMOR, items.O_BLOODTASTEAXE,items. O_GOLDENHELMET, items.O_GOLDENGLOVES,
      items.O_CLOAK, items.O_NEGATOR, items.O_GREENRING, items.O_GHOSTBLADE, 0, 0 } ),
   new CharacterClass("Orch", 4, 13, 0, 10, 50, 0, 0x51CB, 30, 30, 0, 40,
    new int[]{ items.O_BOW, items.O_ARROWS, 0, 0, 0, 0, 0, 0, 0, 0 } ),
   new CharacterClass( "Guardian", 5, 22, 0, 19, 200, 0, 0x51CB, 155, 155, 0, 200,
    new int[]{
    items.O_SILVERBOW, items.O_ARMOR, items.O_LARGEHELMET, items.O_GLOVES, items.O_FAITHFULARROW,
      items.O_TWOHANDEDSWORD, 0, 0, 0, 0 } ),
   new CharacterClass( "Dragon", 8, 60, 25, 25, 600, 300, 0x51DF, 156, 156, 0, 250,
    new int[]{
    0x1000 | items.O_INFERNOSPELL, 0x1000 | items.O_DRAGONSTOOTH, items.O_DRAGONSTOOTH,
      0x1000 | items.O_GOLDENARMOR, 0x1000 | items.O_NEGATOR, 0x1000 | items.O_GREENRING, 0, 0, 0, 0 } ),
   new CharacterClass( "DeathLord", 5, 25, 20, 20, 200, 100, 0x7FFF, 113, 113, 0, 230,
   new int[]{
    items.O_LARGEHELMET, items.O_ARMOR, items.O_GLOVES,items. O_LIGHTNINGSPELL, items.O_SLAYERSWORD,
      items.O_DEMONSPELL, 0, 0, 0, 0 } ),
   new CharacterClass( "The Dark One", 7, 80, 25, 25, 500, 1000, 0x7FFF, 105, 105, 0, PERSISTENT,
    new int[]{
    items.O_GOLDENARMOR, items.O_GOLDENHELMET, items.O_NEGATOR, items.O_BLOODTASTEAXE, items.O_ENERGYSPELL,
      items.O_GREENRING, items.O_STORMSPELL, items.O_DEMONSPELL, items.O_INFERNOSPELL, items.O_CHAOSSPELL } ),
   new CharacterClass( "Small one", 8, 5, 0, 0, 10, 0, 0x59DB, 45, 46, 0, 1200,
    new int[]{  0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } )
};
        }

    }
}