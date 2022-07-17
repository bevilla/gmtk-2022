

public static class Events
{

    // GOOD EVENTS
    public static IEvent BarilOfRumFound()
    {
        IEvent value = new();
        value.timer = float.PositiveInfinity;
        value.title = "Rum in sight! ";
        value.description = "An abbandoned baril full of rum was just fish out by your guys!";
        value.treasure = 10;
        value.sortValue = new int[2] { 1, 2 };
        value.sound = SoundManager.Instance.woodBox;
        value.icon = TextureManager.Instance.rum;
        return value;
    }

    public static IEvent FavorableWind()
    {
        IEvent value = new();
        value.title = "Favorable Wind";
        value.description = "The wind is with you, you gain a bonus of speed";
        value.speed = 5;
        value.timer = 20;
        value.sortValue = new int[2]{ 3, 4};
        value.sound = SoundManager.Instance.wind;
        value.icon = TextureManager.Instance.wind;
        return value;
    }

    public static IEvent CastawayFound()
    {
        IEvent value = new();
        value.title = "You found a Castaway";
        value.description = "You just found a poor Castaway, gain a bonus of moral!";
        value.treasure = 10;
        value.pv = 10;
        value.timer = float.PositiveInfinity;
        value.sound = SoundManager.Instance.humanCry;
        value.icon = TextureManager.Instance.castaway;
        value.sortValue = new int[2] { 4, 5 };
        return value;
    }


    public static IEvent UsableWreck()
    {
        IEvent value = new();
        value.title = "A Wreck!";
        value.description = "An old Wreck is in sight! you can get some good materials for your boat";
        value.pv = 10;
        value.timer = float.PositiveInfinity;
        value.sound = SoundManager.Instance.woodWreck;
        value.icon = TextureManager.Instance.wreck;
        value.sortValue = new int[2] { 5, 6 };
        return value;
    }

    // BAD EVENTS
    public static IEvent FightOnBoard()
    {
        IEvent value = new();
        value.title = "FIIIIIIGHT";
        value.description = "2 of your marins just started a fight... after few minutes the dual became a all in fight. stupid marin!";
        value.speed = -5;
        value.pv = -10;
        value.timer = 20;
        value.sound = SoundManager.Instance.sword;
        value.icon = TextureManager.Instance.fight;
        value.sortValue = new int[2] { 1, 1 };
        return value;
    }

    public static IEvent RatsOnBoard()
    {
        IEvent value = new();
        value.title = "Rats just invade";
        value.description = "During your night you felt something nibbling your toes. what was that ? food is also missing... damn rat!";
        value.food = -20;
        value.timer = float.PositiveInfinity;
        value.sound = SoundManager.Instance.ratStep;
        value.icon = TextureManager.Instance.rats;
        value.sortValue = new int[2] { 1, 2 };
        return value;
    }

    public static IEvent Doldrums()
    {
        IEvent value = new();
        value.title = "Captain, we're stuck!";
        value.description = "You just enter in the Doldrums, no wind, no wave, nothing... finger cross you will be able to move on soon!";
        value.speed = -10;
        value.timer = 20;
        value.sound = SoundManager.Instance.Doldrums;
        value.icon = TextureManager.Instance.doldrums;
        value.sortValue = new int[2] { 2, 3 };
        return value;
    }

    public static IEvent Scurvy()
    {
        IEvent value = new();
        value.title = "Scurvy is here...";
        value.description = "Damn it, some of your guys just got Scurvy... Maybe they will found some fruits soon";
        value.pv = -30;
        value.timer = float.PositiveInfinity;
        value.sound = SoundManager.Instance.sickHuman;
        value.icon = TextureManager.Instance.scurvy;
        value.sortValue = new int[2] { 3, 4 };
        return value;
    }

    public static IEvent OFNI()
    {
        IEvent value = new();
        value.title = "Scratch!";
        value.description = "You can hear a big and loud noise from the front of your boat. You just crash into something undefined. your boat is not on good condition but you will not sink";
        value.pv = -20;
        value.timer = float.PositiveInfinity;
        value.sound = SoundManager.Instance.ofniCrash;
        value.icon = TextureManager.Instance.OFNI;
        value.sortValue = new int[2] { 5, 6 };
        return value;
    }

    // GREED EVENTS
    public static IEvent Pirates()
    {
        IEvent value = new();
        value.title = "Pirate on sight !!";
        value.description = "On the telescope you can see a black flag with a skull drown... Is it the end of your travel?";
        value.food = 20;
        value.speed = -40;
        value.timer = 20;
        value.sound = SoundManager.Instance.fightPirate;
        value.icon = TextureManager.Instance.pirate;
        value.sortValue = new int[2] { 1, 2 };
        return value;
    }

    public static IEvent BoxOfFruit()
    {
        IEvent value = new();
        value.title = "Fruits found!";
        value.description = "your marins just found a box of found in the middle of the sea. after a small detour for catching it you continue your travel";
        value.food = 10;
        value.speed = -10;
        value.timer = 20;
        value.sound = SoundManager.Instance.woodBox;
        value.icon = TextureManager.Instance.boxOfFruits;
        value.sortValue = new int[2] { 2, 3 };
        return value;
    }

    public static IEvent SharkAttack()
    {
        IEvent value = new();
        value.title = "A big squale is running on you !!";
        value.description = "So the legends are true, we can find some of these legendary creature in the middle of the sea. killing him can be good for you... but irreversible damage can happen";
        value.food = 40;
        value.treasure = 40;
        value.speed = -8;
        value.pv= -20;
        value.timer = float.PositiveInfinity;
        value.sound = SoundManager.Instance.sharkDeepSounds;
        value.icon = TextureManager.Instance.shark;
        value.sortValue = new int[2] { 4, 5 };
        return value;
    }


    public static IEvent FortyKnots()
    {
        IEvent value = new();
        value.title = "40 knots!!";
        value.description = "The wind is here... Maybe a little bit too much. you're now surfing on the sea but your boat is not ready for this situation";
        value.pv = -20;
        value.speed = 10;
        value.timer = 30;
        value.sound = SoundManager.Instance.thunderWind;
        value.icon = TextureManager.Instance.storm;
        value.sortValue = new int[2] { 5, 6 };
        return value;
    }

    // ISLAND EVENT

    public static IEvent UnhabitedIsland()
    {
        IEvent value = new();
        value.title = "Nobody here";
        value.description = "You just arrived in a inhabited island. Nobody is here except pigs, snakesand coconuts... One of your marins just got bitten by a snake. You leave the island with more food!";
        value.pv = -10;
        value.food = 30;
        value.sound = SoundManager.Instance.island3;
        value.icon = TextureManager.Instance.island;
        value.sortValue = new int[2] { 3, 4 };
        return value;
    }

    public static IEvent CannibalHere()
    {
        IEvent value = new();
        value.title = "A cannibal tribe attack you";
        value.description ="your raft just touched the beach and you can heard some tribal cry coming from the forest. You are not alone. After 30 minutes the tribe attack you! your guys try to take with them every food supply they found and you let go of the ropes.";
        value.pv = -15;
        value.food = 20;
        value.sound = SoundManager.Instance.island2;
        value.icon = TextureManager.Instance.tribe;
        value.sortValue = new int[2] { 1, 2 };
        return value;
    }

    public static IEvent Outpost()
    {
        IEvent value = new();
        value.title = "Outpost on the sight!";
        value.description = "You finally arrive to a outpost. you and your guys will be able to have some rest, restock your supply and continue your travel";
        value.pv = 20;
        value.food = 30;
        value.sound = SoundManager.Instance.island1;
        value.icon = TextureManager.Instance.outpost;
        value.sortValue = new int[2] { 5, 6 };
        return value;
    }

    public static IEvent Treasure()
    {
        IEvent value = new();
        value.title = "Treasure!";
        value.description = "You just docked to this island. your guys start to explore it. After few hours they come back with a big chest! Found in a cave on the middle of the island, it contains golds and gems!";
        value.treasure = 40;
        value.sound = SoundManager.Instance.island4;
        value.icon = TextureManager.Instance.treasure;
        value.sortValue = new int[2] { 6, 6 };
        return value;
    }
}
