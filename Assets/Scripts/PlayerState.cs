using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private int m_pv;
    private int m_speed;
    private int m_moral;
    private int m_food;
    private List<IEvent> m_currentEvents;
    private List<IEvent> m_pastEvents;

    private float m_internalTimer;
    private const int FOOD_CONSUMPTION = 5;
    private const int MORAL_CONSUMPTION = 5;
    private const int TIMER_CONSUMPTION = 5;

    public void InitState(int pv, int speed, int moral, int food)
    {
        m_pv = pv;
        m_speed = speed;
        m_moral = moral;
        m_food = food;
        m_currentEvents = new();
        m_pastEvents = new();
        m_internalTimer = 0;
    }


    public int GetPv()
    {
        return m_pv;
    }

    public int GetMorals()
    {
        return m_moral;
    }

    public int GetSpeed()
    {
        int baseSpeed= m_speed;
        foreach (IEvent _event in m_currentEvents)
        {
            baseSpeed+= _event.speed;
        }
        return baseSpeed;
    }

    public int GetFood()
    {
        return m_food;
    }

    public void AddEvent(IEvent _event)
    {
        SoundManager.Instance.PlayAudioClip(_event.sound);
        if(_event.timer == float.PositiveInfinity)
        {
            Debug.Log("Event " + _event.title + " is added to historic");
            m_food += _event.food;
            m_moral += _event.moral;
            m_pv += _event.pv;
            m_pastEvents.Add(_event);
        }
        else
        {
            Debug.Log("Event " + _event.title + " is added to current event with timer");
            m_currentEvents.Add(_event);
        }
    }

    private void UpdateEventList()
    {
        for (int i = 0; i < m_currentEvents.Count; i++)
        {
            IEvent selectedEvent = m_currentEvents[i];
            if (selectedEvent.timer != float.PositiveInfinity)
            {
                selectedEvent.timer -= Time.deltaTime;
                m_currentEvents[i] = selectedEvent;
            }
        }
        for (int i = 0; i < m_currentEvents.Count; i++)
        {
            if (m_currentEvents[i].timer <= 0)
            {
                Debug.Log("Event " + m_currentEvents[i].title + " is removed");
                m_currentEvents.Remove(m_currentEvents[i]);
            }
        }
    }

    private void UpdateConsummedValue()
    {
        m_internalTimer += Time.deltaTime;
        if(m_internalTimer >= TIMER_CONSUMPTION)
        {
            Debug.Log("FOOD AND MORAL CONSUMED " + m_moral + " " + m_food);
            m_moral -= MORAL_CONSUMPTION;
            m_food -= FOOD_CONSUMPTION;
            m_internalTimer = 0;
        }
    }

    private void Update()
    {
        UpdateEventList();
        UpdateConsummedValue();
    }

}
