using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHide : Item
{
    public override void Use(GameObject game)
    {
        game.SetActive(false);
        GameManager.instance.OnPlayerDead();
    }
}