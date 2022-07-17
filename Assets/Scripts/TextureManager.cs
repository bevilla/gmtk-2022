using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    public static TextureManager Instance { get; private set; }

    public Texture pirate;
    public Texture heart;
    public Texture rum;
    public Texture food;
    public Texture rudder;
    public Texture wind;
    public Texture castaway;
    public Texture wreck;
    public Texture fight;
    public Texture rats;
    public Texture doldrums;
    public Texture scurvy;
    public Texture OFNI;
    public Texture boxOfFruits;
    public Texture shark;
    public Texture storm;
    public Texture island;
    public Texture tribe;
    public Texture outpost;
    public Texture treasure;
    public Texture p10;
    public Texture p20;
    public Texture p30;
    public Texture p40;
    public Texture p50;
    public Texture p60;

    void Awake()
    {
        Instance = this;
    }
}
