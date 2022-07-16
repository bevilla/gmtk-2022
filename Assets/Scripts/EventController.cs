using System.Collections.Generic;
using UnityEngine;

public static class EventController
{
    public static IEvent[] SelectDefaultEventFromType(EVENT_TYPE type)
    {
        switch(type)
        {
            case EVENT_TYPE.SEA_BAD:
                return EventLists.GetBadSeaEvents();
            case EVENT_TYPE.SEA_GOOD:
                return EventLists.GetGoodSeaEvents();
            case EVENT_TYPE.SEA_GREED:
                return EventLists.GetGreedSeaEvents();
            case EVENT_TYPE.ISLAND:
                return EventLists.GetIslandEvents();
            default:
                return EventLists.GetBadSeaEvents();
        }
    }

    public static IEvent GetNewEvent(EVENT_TYPE type, int diceValue)
    {
        IEvent[] defaultList = SelectDefaultEventFromType(type);
        List<IEvent> filteredList = new();
        foreach(IEvent _event in defaultList)
        {
            if(diceValue >= _event.sortValue[0] && diceValue <= _event.sortValue[1])
            {
                filteredList.Add(_event);
            }
        }
        return filteredList[Random.Range(0, filteredList.Count)];
    }

    public static IEvent ApplyEventModifier(IEvent selectedEvent, int diceValue)
    {
        float percentage = 1 + (diceValue / 10.0f);
        selectedEvent.pv = (int)Mathf.Ceil(selectedEvent.pv * percentage);
        selectedEvent.food = (int)Mathf.Ceil(selectedEvent.food * percentage);
        selectedEvent.moral = (int)Mathf.Ceil(selectedEvent.moral * percentage);
        selectedEvent.speed = (int)Mathf.Ceil(selectedEvent.speed * percentage);
        return selectedEvent;
    }
}
