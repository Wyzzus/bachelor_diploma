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
    public string CurrentImageBase64;

    public float MaxW = 450;
    public float MaxH = 450;

    public ImageHandler imageHandler;

    public virtual void Start()
    {
        imageHandler = new ImageHandler();
        imageHandler.MaxH = MaxH;
        imageHandler.MaxW = MaxW;
    }

    public void ShowBaseInfo(DndObject objectToEdit)
    {
        NameField.text = objectToEdit.Name;
        DescriptionField.text = objectToEdit.Description;
    }

    public void SaveBaseInfo(DndObject toSave)
    {
        toSave.Name = NameField.text;
        toSave.Description = DescriptionField.text;
    }

    public void SaveImage(IDisplayable displayable)
    {
        displayable.Image = CurrentImageBase64;
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
        if(CurrentImageSprite)
            CurrentImageSprite.sprite = null;
    }
}

[System.Serializable]
public class ScrollViewHandler
{
    public GameObject prefab;
    public RectTransform content;

    public void Update(List<DndObject> ObjectList, DndEditor editor = null)
    {
        foreach(RectTransform child in content)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach(DndObject obj in ObjectList)
        {
            GameObject clone = GameObject.Instantiate<GameObject>(prefab, content);
            clone.GetComponent<DataContainer>().Setup(obj, editor);
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
