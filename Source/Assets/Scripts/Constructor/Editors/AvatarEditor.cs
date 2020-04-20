using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarEditor : DndEditor
{
    public Avatar CurrentEditObject;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void Edit<T>(T objectToEdit)
    {
        base.Edit(objectToEdit);
        ShowBaseInfo(objectToEdit);
        Avatar converted = objectToEdit as Avatar;
        imageHandler.ShowImage(converted.Image, CurrentImageSprite);
        CurrentEditObject = converted;
        CurrentImageBase64 = converted.Image;
    }

    public void Save()
    {
        SaveImage(CurrentEditObject);
        List<Avatar> Avatars = PackConstructor.instance.CurrentThemePack.Avatars;
        if (!Avatars.Contains(CurrentEditObject))
            Avatars.Add(CurrentEditObject);
        SaveBaseInfo(CurrentEditObject);
    }

    public override void ClearEditor()
    {
        CurrentEditObject = new Avatar();
        base.ClearEditor();
    }
}
