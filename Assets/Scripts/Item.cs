﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public  virtual void Use(GameObject game)
    {
        Debug.Log("it's empty item");
    }
}