using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MarkerContainer : GameDataContainer
{
    public GameObject Button;

    public void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            Button.SetActive(false);
    }

    public void Remove()
    {
        int index = GameManager.instance.Map.Markers.IndexOf(this);
        GameManager.instance.ClientRemoveMarker(index);
    }
}
