

public static class Events
{

    // GOOD EVENTS
    public static IEvent BarilOfRumFound()
    {
        IEvent value = new();
        value.title = "Rum in sight! ";
        value.description = "An abbandoned baril full of rum was just fish out by your guys!";
        value.moral = 5;
        value.sortValue = new int[2] { 1, 2 };
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
        return value;
    }

    public static IEvent CastawayFound()
    {
        IEvent value = new();
        value.title = "You found a Castaway";
        value.description = "You just found a poor Castaway, gain a bonus of moral!";
        value.moral = 10;
        value.sortValue = new int[2] { 4, 5 };
        return value;
    }


    public static IEvent UsableWreck()
    {
        IEvent value = new();
        value.title = "A Wreck!";
        value.description = "An old Wreck is in sight! you can get some good materials for your boat";
        value.pv = 10;
        value.sortValue = new int[2] { 5, 6 };
        return value;
    }

    // BAD EVENTS
    public static IEvent FightOnBoard()
    {
        IEvent value = new();
        value.title = "FIIIIIIGHT";
        value.description = "2 of your marins just started a fight... after few minutes the dual became a all in fight. stupid marin!";
        value.moral = -20;
        value.speed = -15;
        value.pv = -10;
        value.timer = 20;
        value.sortValue = new int[2] { 1, 1 };
        return value;
    }

    public static IEvent RatsOnBoard()
    {
        IEvent value = new();
        value.title = "Rats just invade";
        value.description = "During your night you felt something nibbling your toes. what was that ? food is also missing... damn rat!";
        value.food = -20;
        value.sortValue = new int[2] { 1, 2 };
        return value;
    }

    public static IEvent Doldrums()
    {
        IEvent value = new();
        value.title = "Captain, we're stuck!";
        value.description = "You just enter in the Doldrums, no wind, no wave, nothing... finger cross you will be able to move on soon!";
        value.speed = -50;
        value.timer = 20;
        value.sortValue = new int[2] { 2, 3 };
        return value;
    }

    public static IEvent Scurvy()
    {
        IEvent value = new();
        value.title = "Scurvy is here...";
        value.description = "Damn it, some of your guys just got Scurvy... Maybe they will found some fruits soon";
        value.pv = -30;
        value.sortValue = new int[2] { 3, 4 };
        return value;
    }

    public static IEvent OFNI()
    {
        IEvent value = new();
        value.title = "Scratch!";
        value.description = "You can hear a big and loud noise from the front of your boat. You just crash into something undefined. your boat is not on good condition but you will not sink";
        value.pv = -20;
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
        value.sortValue = new int[2] { 2, 3 };
        return value;
    }

    public static IEvent SharkAttack()
    {
        IEvent value = new();
        value.title = "A big squale is running on you !!";
        value.description = "So the legends are true, we can find some of these legendary creature in the middle of the sea. killing him can be good for you... but irreversible damage can happen";
        value.food = 40;
        value.moral = 20;
        value.speed = -40;
        value.pv= -20;
        value.sortValue = new int[2] { 4, 5 };
        return value;
    }


    public static IEvent FortyKnots()
    {
        IEvent value = new();
        value.title = "40 knots!!";
        value.description = "The wind is here... Maybe a little bit too much. you're now surfing on the sea but your boat is not ready for this situation";
        value.pv = -20;
        value.speed = +40;
        value.moral = +10;
        value.timer = 30;
        value.sortValue = new int[2] { 5, 6 };
        return value;
    }
}
