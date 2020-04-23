using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun, IPunObservable
{
    public SaveLoadManager saveLoadManager;

    public static GameManager instance;

    public ThemePack CurrentThemePack;

    public MapComponent Map;
    public GameObject EventInfo;

    public DndObjectUI UIPart;
    public List<PlayerController> Players = new List<PlayerController>();

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            
        }
        else if (stream.IsReading)
        {
            
        }
    }

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        saveLoadManager = new SaveLoadManager();
        string path = @"C:\Users\Wyzzus\Desktop\Stalker2.hgd";
        //string path = Application.dataPath + @"\Packs\Stalker1.hgd";
        string message = "";
        CurrentThemePack = saveLoadManager.Load(path, out message);
        Debug.Log(message);
    }
    
    public void SetMap(int id)
    {
        Debug.Log("Set map to " + id);
        Map.SetMap(id);
    }

    public void GenerateEvent(int id, int playerId)
    {

    }
}
