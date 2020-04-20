using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LocationEditor : DndEditor
{
    public Location CurrentEditObject;

    public PlaceableObject CurrentPlaceableObject = null;

    public List<PlaceableObject> Objects;

    public List<PlaceableObject> LocationObjects = new List<PlaceableObject>();

    public Dropdown ObjectsDropdown;

    public ObjectLayerHelper ObjectsLayer;

    public GameObject LocationObjectContainerPrefab;

    public bool CanPlaceObjects = false;

    public override void Start()
    {
        base.Start();
    }

    public void Update()
    {
        if (Objects.Count > 0)
            CanPlaceObjects = true;
        else
            CanPlaceObjects = false;

        if(Input.GetMouseButtonUp(0) && CanPlaceObjects && ObjectsLayer.CanPlaceObject)
        {
            AddObjectOnMap(Input.mousePosition);
        }
    }

    public void SetCurrentPlaceable(int n)
    {
        CurrentPlaceableObject = Objects[n];
    }

    public void AddObjectOnMap(Vector2 pos, bool OnEdit = false)
    {
        GameObject clone = Instantiate<GameObject>(LocationObjectContainerPrefab);
        RectTransform cloneRect = clone.GetComponent<RectTransform>();

        if (OnEdit)
        {
            cloneRect.SetParent(ObjectsLayer.Layer);
            cloneRect.anchoredPosition = pos;
        }
        else
        {
            cloneRect.anchoredPosition = pos;
            cloneRect.SetParent(ObjectsLayer.Layer);
        }

        PlaceableObject newObj = new PlaceableObject(CurrentPlaceableObject);
        LocationObjects.Add(newObj);

        LocationObjectContainer container = clone.GetComponent<LocationObjectContainer>();
        container.Setup(newObj, this);
        container.SetPosition(cloneRect.localPosition);
    }

    public void RemoveObjectFromMap(PlaceableObject obj)
    {
        LocationObjects.Remove(obj);
    }

    public void UpdateView()
    {
        HandleDropDown();
        if(Objects.Count > 0)
            SetCurrentPlaceable(0);
    }

    public void HandleDropDown()
    {
        ObjectsDropdown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        Objects = PackConstructor.instance.CurrentThemePack.Objects;
        foreach (PlaceableObject category in Objects)
        {
            Dropdown.OptionData option = new Dropdown.OptionData
            {
                text = category.Name
            };
            options.Add(option);
        }
        ObjectsDropdown.AddOptions(options);
    }

    public static DndObject PlaceableToObject(PlaceableObject placeable)
    {
        return placeable;
    }

    public override void Edit<T>(T objectToEdit)
    {
        UpdateView();
        base.Edit(objectToEdit);
        ShowBaseInfo(objectToEdit);
        Location converted = objectToEdit as Location;
        imageHandler.ShowImage(converted.Image, CurrentImageSprite);
        CurrentEditObject = converted;
        foreach (PlaceableObject obj in CurrentEditObject.PlacedObjects)
        {
            CurrentPlaceableObject = obj;
            AddObjectOnMap(CurrentPlaceableObject.GetPosition2D(), true);
        }
        CurrentImageBase64 = converted.Image;
    }

    public void Save()
    {
        SaveImage(CurrentEditObject);
        CurrentEditObject.PlacedObjects = LocationObjects;
        List<Location> Locations = PackConstructor.instance.CurrentThemePack.Locations;
        if (!Locations.Contains(CurrentEditObject))
            Locations.Add(CurrentEditObject);
        SaveBaseInfo(CurrentEditObject);
    }

    public override void ClearEditor()
    {
        CurrentEditObject = new Location();
        LocationObjects = new List<PlaceableObject>();
        foreach(RectTransform child in ObjectsLayer.Layer)
        {
            Destroy(child.gameObject);
        }
        base.ClearEditor();
    }
}
