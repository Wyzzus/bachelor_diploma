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
        PlayerListInfoPref.GetComponent<GameObject>().SetActive(false);
        playerData = GetComponentInParent<PlayerData>();
    }

    public void ShowAndHide()
    {
        if (PlayerListInfo.activeSelf == false)
            PlayerListInfo.SetActive(true);
        else
            PlayerListInfo.SetActive(false);
    }

    public void ShowInventory()
    {

    }
}
