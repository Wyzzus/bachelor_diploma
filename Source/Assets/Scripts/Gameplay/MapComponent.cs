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

    public List<GameDataContainer> Markers = new List<GameDataContainer>();
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
        ClearRoot();
        StopAllCoroutines();
        Location newLocation = GameManager.instance.CurrentThemePack.Locations[index];
        UIPart.ShowObject(newLocation);
        StartCoroutine(SetPlacedObjects(newLocation));
    }

    public IEnumerator SetPlacedObjects(Location location)
    {
        float ratioX = 65.0f / (650.0f / 1.3f);
        float ratioZ = 45.0f / (450.0f / 1.3f);
        foreach(PlaceableObject obj in location.PlacedObjects)
        {
            Vector3 pos = new Vector3(obj.Position.x * ratioX, 0, obj.Position.z * ratioZ); ;
            AddMarker(obj, pos);

        }
        yield return null;
    }

    public void AddMarker(int id, Vector3 position)
    {
        PlaceableObject obj = GameManager.instance.CurrentThemePack.Objects[id];
        GameObject clone = Instantiate<GameObject>(MarkerPrefab);
        GameDataContainer container = clone.GetComponent<GameDataContainer>();
        container.UIPart.ShowObject(obj);
        container.transform.position = position;
        container.transform.SetParent(ObjectsRoot);
        Markers.Add(container);
    }

    public void AddMarker(PlaceableObject obj, Vector3 position)
    {
        GameObject clone = Instantiate<GameObject>(MarkerPrefab, ObjectsRoot);
        GameDataContainer container = clone.GetComponent<GameDataContainer>();
        container.UIPart.ShowObject(obj);
        container.transform.localPosition = position;
        Markers.Add(container);
    }

    public void RemoveMarker(int id)
    {
        GameDataContainer tmp = Markers[id];
        Markers.Remove(tmp);
        Destroy(tmp.gameObject);
    }

    public void ClearRoot()
    {
        Markers.Clear();
        foreach(Transform child in ObjectsRoot)
        {
            Destroy(child.gameObject);
        }
    }
}
