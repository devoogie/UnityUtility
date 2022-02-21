using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SnakeBodyData
{
    public int Level;
    public int Health;
    public SnakeBodyData(int health)
    {
        Level = 1;
        Health = health;
    }
}
