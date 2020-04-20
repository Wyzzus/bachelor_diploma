using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackConstructor : MonoBehaviour
{
    public static PackConstructor instance;

    public ThemePack CurrentThemePack = new ThemePack();

    void Start()
    {
        instance = this;
    }
}
