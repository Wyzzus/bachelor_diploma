using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationEditor : DndEditor
{
    public Location CurrentEditObject;

    public PlaceableObject CurrentPlaceableObject = null;

    public List<PlaceableObject> Objects;
    public List<PlaceableObject> LocationObjects;

    public RectTransform LocationObjectsLayer;

    public GameObject LocationObjectContainerPrefab;

    public override void Start()
    {
        base.Start();
        Objects = PackConstructor.instance.CurrentThemePack.Objects;
        CurrentPlaceableObject = Objects[0];
        UpdateView();
    }

    public void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            AddObjectOnMap(Input.mousePosition);
        }
    }

    public void AddObjectOnMap(Vector2 pos)
    {
        GameObject clone = Instantiate<GameObject>(LocationObjectContainerPrefab, LocationObjectsLayer);
        LocationObjectContainer container = LocationObjectContainerPrefab.GetComponent<LocationObjectContainer>();
        container.Setup(CurrentPlaceableObject, this);
        container.SetPosition(pos);
    }

    public void UpdateView()
    {
        List<DndObject> objs = Objects.ConvertAll(new System.Converter<PlaceableObject, DndObject>(PlaceableToObject));
        ScrollViewHandler.Update(objs, this);
    }

    public static DndObject PlaceableToObject(PlaceableObject placeable)
    {
        return placeable;
    }

    public void Edit(Location objectToEdit)
    {
        ShowBaseInfo(objectToEdit);
        imageHandler.ShowImage(objectToEdit.Image, CurrentImageSprite);
        CurrentEditObject = objectToEdit;
    }

    public void Save()
    {
        SaveBaseInfo(CurrentEditObject);
        SaveImage(CurrentEditObject);
        List<Location> Locations = PackConstructor.instance.CurrentThemePack.Locations;
        if (!Locations.Contains(CurrentEditObject))
            Locations.Add(CurrentEditObject);
    }
}
