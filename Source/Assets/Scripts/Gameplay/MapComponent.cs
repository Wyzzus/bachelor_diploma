using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MapComponent : EntityComponent
{
    public DndObjectUI UIPart;
    public Transform ObjectsRoot;
    public GameObject MarkerPrefab;

    public override void Start()
    {
        
    }

    void Update()
    {

    }

    void MenuFill()
    {

    }

    public void SetMap(int index)
    {
        StopAllCoroutines();
        Location newLocation = GameManager.instance.CurrentThemePack.Locations[index];
        UIPart.ShowObject(newLocation);
        StartCoroutine(SetPlacedObjects(newLocation));
    }

    public IEnumerator SetPlacedObjects(Location location)
    {
        ClearRoot();
        float ratioX = 65.0f / 650.0f;
        float ratioZ = 45.0f / 450.0f;
        foreach(PlaceableObject obj in location.PlacedObjects)
        {
            GameObject clone = Instantiate<GameObject>(MarkerPrefab, ObjectsRoot);
            GameDataContainer container = clone.GetComponent<GameDataContainer>();
            container.UIPart.ShowObject(obj);
            container.transform.localPosition = new Vector3(obj.Position.x * ratioX, 0, obj.Position.z * ratioZ);

        }
        yield return null;
    }

    public void ClearRoot()
    {
        foreach(Transform child in ObjectsRoot)
        {
            Destroy(child.gameObject);
        }
    }
}
