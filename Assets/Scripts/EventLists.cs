
public static class EventLists
{
    // SEA EVENTS
    public static IEvent[] GetGoodSeaEvents()
    {
        return new IEvent[] {
            Events.BarilOfRumFound(),
            Events.FavorableWind(),
            Events.CastawayFound(),
            Events.UsableWreck()
        };
    }

    public static IEvent[] GetBadSeaEvents()
    {
        return new IEvent[] {
            Events.FightOnBoard(),
            Events.RatsOnBoard(),
            Events.Doldrums(),
            Events.Scurvy(),
            Events.OFNI(),
        };
    }

    public static IEvent[] GetGreedSeaEvents()
    {
        return new IEvent[] {
            Events.Pirates(),
            Events.BoxOfFruit(),
            Events.SharkAttack(),
            Events.FortyKnots()
        };
    }


    // ISLAND EVENTS
    public static IEvent[] GetIslandEvents()
    {
        return new IEvent[] {
            Events.UnhabitedIsland(),
            Events.CannibalHere(),
            Events.Outpost(),
            Events.Treasure()
        };
    }
} 
