using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MapComponent : EntityComponent
{
    public Dropdown MapSelectMenu;
    SpriteRenderer spriteRenderer;
    BoxCollider boxCollider;
    public string[] MapsList;
    public Texture2D[] textures;

    public override void Start()
    {
        base.Start();
        textures = Resources.LoadAll<Texture2D>("TestMaps/");
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = spriteRenderer.sprite.bounds.size;
        MenuFill();
    }

    void Update()
    {
        if (spriteRenderer.sprite == null) spriteRenderer.sprite = Sprite.Create(new Texture2D(100, 100), new Rect(0, 0, 100, 100), new Vector2(0, 0));
        boxCollider.size = spriteRenderer.sprite.bounds.size;

    }

    void MenuFill()
    {
        MapsList = Directory.GetFiles("Assets\\Resources\\TestMaps", "*.jpg");
        MapSelectMenu.options.Clear();
        foreach (string option in MapsList)
        {
            
            MapSelectMenu.options.Add(new Dropdown.OptionData(option.Substring(option.LastIndexOf(@"\") + 1)));
        }
    }

    public void MapChange()
    {
        Debug.Log(MapsList[MapSelectMenu.value].LastIndexOf(@"\")+1);
        int lastIndex = MapsList[MapSelectMenu.value].LastIndexOf(@"\") + 1;
        Debug.Log(@"TestMaps/" + MapsList[MapSelectMenu.value].Substring(lastIndex));
        string spritePath = "TestMaps/" + MapsList[MapSelectMenu.value].Substring(lastIndex);
        Debug.Log(MapSelectMenu.value);
        spriteRenderer.sprite = Sprite.Create(textures[MapSelectMenu.value], new Rect(0, 0, textures[MapSelectMenu.value].width, textures[MapSelectMenu.value].height), new Vector2(.5f, .5f)); //Resources.Load<Sprite>(spritePath.Substring(0,spritePath.Length-4));
    }
}
