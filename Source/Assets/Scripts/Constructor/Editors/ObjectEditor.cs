using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectEditor : DndEditor
{
    public PlaceableObject CurrentEditObject;
    public InputField CategoryName;

    public List<DndCategory> Categories;
    public Dropdown CategoryDropDown;

    public override void Start()
    {
        base.Start();
        Categories = PackConstructor.instance.CurrentThemePack.Categories;
        CreateCategory("Объекты");
    }

    public void Edit(PlaceableObject objectToEdit)
    {
        ShowBaseInfo(objectToEdit);
        imageHandler.ShowImage(objectToEdit.Image, CurrentImageSprite);
        CurrentEditObject = objectToEdit;
    }

    public void CreateCategory(string categoryName)
    {
        DndCategory newCategory = new DndCategory
        {
            Name = categoryName
        };
        Categories.Add(newCategory);
        UpdateView();
    }

    public void RemoveCategory(DndCategory category)
    {
        Categories.Remove(category);
        List<DndObject> cats = Categories.ConvertAll(new System.Converter<DndCategory, DndObject>(CategoryToObject));
        ScrollViewHandler.Update(cats);
        HandleDropDown();
        UpdateView();
    }

    public void AddCategory()
    {
        CreateCategory(CategoryName.text);
    }

    public void UpdateView()
    {
        List<DndObject> cats = Categories.ConvertAll(new System.Converter<DndCategory, DndObject>(CategoryToObject));
        ScrollViewHandler.Update(cats, this);
        HandleDropDown();
    }

    public void HandleDropDown()
    {
        CategoryDropDown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach(DndCategory category in Categories)
        {
            Dropdown.OptionData option = new Dropdown.OptionData
            {
                text = category.Name
            };
            options.Add(option);
        }
        CategoryDropDown.AddOptions(options);
    }

    public static DndObject CategoryToObject(DndCategory category)
    {
        return category;
    }

    public void Save()
    {
        SaveBaseInfo(CurrentEditObject);
        SaveImage(CurrentEditObject);
        CurrentEditObject.CategoryId = CategoryDropDown.value;
        List<PlaceableObject> Objects = PackConstructor.instance.CurrentThemePack.Objects;
        if (!Objects.Contains(CurrentEditObject))
            Objects.Add(CurrentEditObject);
    }

    public override void ClearEditor()
    {
        CurrentEditObject = new PlaceableObject();
        base.ClearEditor();
    }
}
