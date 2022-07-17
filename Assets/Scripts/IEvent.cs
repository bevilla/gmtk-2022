using UnityEngine;

public struct IEvent
{
    public string title;
    public string description;
    public float timer;
    public int speed;
    public int moral;
    public int food;
    public int pv;
    public int[] sortValue;
    public AudioClip sound;
}

public enum EVENT_TYPE
{
    SEA_GOOD = 1,
    SEA_BAD = 2,
    SEA_GREED = 3,
    ISLAND = 4
}