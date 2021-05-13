using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDouble : Item
{
    public override void Use(GameObject game)
    {
        GameManager.instance.AddScore(200);
    }
}