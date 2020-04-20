using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoComponent : MonoBehaviour
{
    public GameObject PlayerListInfoPref;
    public GameObject PlayerListInfo;
    public PlayerData playerData;

    public void Start()
    {
        PlayerListInfo = GameObject.Instantiate(PlayerListInfoPref, GameObject.Find("PlayerUI").transform);
        GetComponent<RectTransform>().transform.position = new Vector3(-335, 181, 0);
        PlayerListInfo.SetActive(false);
        playerData = GetComponentInParent<PlayerData>();
    }

    public void ShowAndHide()
    {
        PlayerListInfo.SetActive(!PlayerListInfo.activeSelf);
    }

    public void ShowInventory()
    {

    }
}
