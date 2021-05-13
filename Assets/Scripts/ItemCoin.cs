using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : Item
{
    public override void Use(GameObject game)
    {
        GameManager.instance.AddScore(100);
    }
}