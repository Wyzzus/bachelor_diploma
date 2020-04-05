using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour
{
    public Dropdown SkinSelectMenu;
    SpriteRenderer spriteRenderer;
    public string[] SkinsList;
    public Texture2D[] textures;
    public RawImage skin;
    public PlayerData playerData;

    void Start()
    {
        textures = Resources.LoadAll<Texture2D>("TestSkins/");
        spriteRenderer = GetComponent<SpriteRenderer>();
        MenuFill();

    }

    public void SetupPlayerData(PlayerData data)
    {
        playerData = data;
    }

    void MenuFill()
    {
        SkinsList = Directory.GetFiles("Assets\\Resources\\TestSkins", "*.jpg");
        SkinSelectMenu.options.Clear();
        foreach (string option in SkinsList)
        {

            SkinSelectMenu.options.Add(new Dropdown.OptionData(option.Substring(option.LastIndexOf(@"\") + 1)));
        }
    }

    public void SkinChange()
    {
        skin.texture = textures[SkinSelectMenu.value];
        playerData.skinID = SkinSelectMenu.value;
    }

    public void SetupName()
    {
        playerData.Name = GameObject.Find("nickname").GetComponent<Text>().text;
    }
}
