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
    public Button MarkButton;
    public bool MarkAddMode;
    public Vector3 MarkPosition = Vector3.zero;
    public LayerMask layerMask;
    public GameObject MarkPrefab;
    public  List<GameObject> MarkLists;

    public override void Start()
    {
        base.Start();
        textures = Resources.LoadAll<Texture2D>("TestMaps/");
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = spriteRenderer.sprite.bounds.size;
        MarkAddMode = false;
        MenuFill();
    }

    void Update()
    {
        if (spriteRenderer.sprite == null) spriteRenderer.sprite = Sprite.Create(new Texture2D(100, 100), new Rect(0, 0, 100, 100), new Vector2(0, 0));
        boxCollider.size = spriteRenderer.sprite.bounds.size;

        if (MarkAddMode == true)
            AddMark();
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

    public void ChangeMarkMode()
    {
        MarkAddMode = !MarkAddMode;
    }

    public void AddMark()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Добавление марки на карту");
            Debug.Log("ЛКМ нажата");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, layerMask))
            {
                MarkPosition = new Vector3(hit.point.x, 0, hit.point.z);
                Debug.Log(MarkPosition);
                GameObject obj = Instantiate(MarkPrefab, MarkPosition, Quaternion.identity) as GameObject;
                MarkAddMode = false;
                obj.transform.SetParent(GameObject.Find("Area").transform);
                MarkLists.Add(obj);
                MarkComponent markComponent = obj.GetComponent<MarkComponent>();
                markComponent.SetupID(MarkLists.IndexOf(obj));
            }
        }
    }

    public void DestroyMark(GameObject obj)
    {
        MarkLists.Remove(obj);
        Destroy(obj);
    }
}
