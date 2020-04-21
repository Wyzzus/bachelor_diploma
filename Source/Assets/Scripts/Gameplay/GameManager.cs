using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : EntitySystem
{
    public SaveLoadManager saveLoadManager;

    public static GameManager instance;

    public ThemePack CurrentThemePack;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        saveLoadManager = new SaveLoadManager();
        string path = @"C:\Users\Wyzzus\Desktop\StalkerDND_2_0.hgd";
        string message = "";
        CurrentThemePack = saveLoadManager.Load(path, out message);
        Debug.Log(message);
    }
}
