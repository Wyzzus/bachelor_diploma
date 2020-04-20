using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SFB;

public class DndEditor : MonoBehaviour
{
    public ScrollViewHandler ScrollViewHandler;

    public InputField NameField;
    public InputField DescriptionField;

    public Image CurrentImageSprite;
    [HideInInspector]
    public string CurrentImageBase64;

    public ImageHandler imageHandler;

    public virtual void Edit<T>(T obj) where T : DndObject
    {

    }

    public virtual void Start()
    {
        //imageHandler = new ImageHandler();
    }

    public void ShowBaseInfo(DndObject objectToEdit)
    {
        NameField.text = objectToEdit.Name;
        DescriptionField.text = objectToEdit.Description;
    }

    public bool AttributesNotChangedIn(List<Attribute> objectAttributes)
    {
        List<Attribute> packAttributes = PackConstructor.instance.CurrentThemePack.Attributes;
        if (objectAttributes.Count != packAttributes.Count)
            return false;

        for (int i = 0; i < packAttributes.Count; i++)
        {
            if (packAttributes[i].Name != objectAttributes[i].Name)
                return false;
        }

        return true;
    }

    public List<Attribute> CloneAttributes(List<Attribute> oldList)
    {
        List<Attribute> newList = new List<Attribute>();
        foreach (Attribute a in oldList)
        {
            newList.Add(new Attribute(a));
        }
        return newList;
    }

    public void SaveBaseInfo(DndObject toSave)
    {
        toSave.Name = NameField.text;
        toSave.Description = DescriptionField.text;
        PackConstructor.instance.UpdateView();
    }

    public void SaveImage(IDisplayable displayable)
    {
        displayable.Image = CurrentImageBase64;
        System.GC.KeepAlive(displayable.Image);
    }

    public void LoadImage()
    {
        var extensions = new[] 
        {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Выберите изображение", Application.dataPath, extensions, false);
        if (paths.Length > 0)
        {
            StartCoroutine(ProcessingImage(paths[0]));
        }
    }

    public IEnumerator ProcessingImage(string path)
    {
        byte[] imageBytes = File.ReadAllBytes(path);
        CurrentImageBase64 = System.Convert.ToBase64String(imageBytes);
        imageHandler.ShowImage(CurrentImageBase64, CurrentImageSprite);
        yield return null;
    }

    public virtual void ClearEditor()
    {
        NameField.text = "";
        DescriptionField.text = "";
        CurrentImageBase64 = "";
        if (CurrentImageSprite)
        {
            CurrentImageSprite.sprite = null;
            CurrentImageSprite.rectTransform.sizeDelta = Vector2.zero;
        }
    }
}

[System.Serializable]
public class ScrollViewHandler
{
    public GameObject prefab;
    public RectTransform content;

    public void Update<T>(List<T> ObjectList, DndEditor editor = null) where T : DndObject
    {
        ClearContent();
        foreach(DndObject obj in ObjectList)
        {
            GameObject clone = GameObject.Instantiate<GameObject>(prefab, content);
            clone.GetComponent<DataContainer>().Setup(obj, editor);
        }
    }

    public void UpdateOnComponent<T>(List<T> ObjectList, EntityComponent component = null) where T : DndObject
    {
        ClearContent();
        foreach (DndObject obj in ObjectList)
        {
            GameObject clone = GameObject.Instantiate<GameObject>(prefab, content);
            //clone.GetComponent<DataContainer>().Setup(obj, component);
        }
    }

    public void ClearContent()
    {
        foreach (RectTransform child in content)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}

[System.Serializable]
public class ImageHandler
{
    public float MaxW = 450;
    public float MaxH = 450;

    public void ShowImage(string base64, Image ImageSprite)
    {
        if (base64 == null || base64.Length == 0)
            return;
        byte[] imageBytes = System.Convert.FromBase64String(base64);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(imageBytes);
        ImageSprite.sprite = null;
        ImageSprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
        ImageSprite.rectTransform.sizeDelta = CalculateSpriteSize(texture);
    }

    public Vector2 CalculateSpriteSize(Texture2D texture)
    {
        float newWidth = MaxW;
        float newHeight = MaxH;

        float ratio = (float)texture.width / (float)texture.height;

        if (ratio > 1)
        {
            newHeight = newWidth / ratio;
            if (newHeight > MaxH)
            {
                newHeight = MaxH;
                newWidth = newHeight * ratio;
            }
        }
        else
        {
            newWidth = newHeight * ratio;
            if (newWidth > MaxW)
            {
                newWidth = MaxW;
                newHeight = newWidth / ratio;
            }
        }

        return new Vector2(newWidth, newHeight);
    }
}
