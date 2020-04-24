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
        string path = @"C:\Users\Wyzzus\Desktop\Stalker1.hgd";
        //string path = Application.dataPath + @"\Packs\CurrentPack.hgd";
        string message = "";
        CurrentThemePack = saveLoadManager.Load(path, out message);
        Debug.Log(message);
    }
    
    public void SetMap(int id)
    {
        Debug.Log("Set map to " + id);
        Map.SetMap(id);
    }

    public void ClientAddMarker(int id, Vector3 pos)
    {
        PhotonView view = PhotonView.Get(this);
        photonView.RPC("AddMarker", RpcTarget.All, id, pos);
    }

    [PunRPC]
    public void AddMarker(int id, Vector3 pos)
    {
        Map.AddMarker(id, pos);
    }

    public void ClientRemoveMarker(int index)
    {
        PhotonView view = PhotonView.Get(this);
        photonView.RPC("RemoveMarker", RpcTarget.All, index);
    }

    [PunRPC]
    public void RemoveMarker(int index)
    {
        Map.RemoveMarker(index);
    }
}
