using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager
{
    public bool Save(ThemePack pack, out string Message)
    {
        Message = "Успешно сохранено!";

        var extensions = new[]
        {
            new ExtensionFilter("Theme Pack", "hgd"),
        };
        var path = StandaloneFileBrowser.SaveFilePanel("Сохранить пак " + pack.Name, Application.dataPath, pack.Name, extensions);
        if (path.Length > 0)
        {
            SavePackTo(pack, path, out Message);
            return true;
        }
        else
        {
            Message = "Ошибка выбора каталога!";
            return false;
        }
    }

    public bool Save(ThemePack pack, string path, out string Message)
    {
        return SavePackTo(pack, path, out Message);
    }

    public ThemePack Load(out string Message)
    {
        Message = "Успешно загружено";

        var extensions = new[]
        {
            new ExtensionFilter("Theme Pack", "hgd"),
        };

        var paths = StandaloneFileBrowser.OpenFilePanel("Загрузить пак", Application.dataPath, extensions, false);
        if (paths.Length > 0)
        {
            return LoadPackFrom(paths[0], out Message);
        }   
        else
        {
            Message = "Ошибка выбора каталога!";
            return null;
        }
    }

    public ThemePack Load(string path, out string Message)
    {
        return LoadPackFrom(path, out Message);
    }

    public bool SavePackTo(ThemePack pack, string path, out string Message)
    {
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, pack);
        fs.Close();
        Message = "Успешно сохранено!";
        return true;
    }

    public ThemePack LoadPackFrom(string path, out string Message)
    {
        Message = "Успешно загружено!";

        ThemePack pack = new ThemePack();
        if (File.Exists(path))
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                pack = (ThemePack)formatter.Deserialize(fs);
            }
            catch (System.Exception e)
            {
                Message = e.Message;
            }
            finally
            {
                fs.Close();
            }
        }
        else
        {
            Message = "Ошибка выбора файла";
        }
        return pack;
    }
}
