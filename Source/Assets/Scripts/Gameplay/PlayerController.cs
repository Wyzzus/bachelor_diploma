using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    public LayerMask LayerMask;
    public Vector3 NewPosition = Vector3.zero;
    public CommonEntity Common;
    public GameObject[] CommonInterfaces;
    public PlayerEntity Player;
    public GameObject[] PlayerInterfaces;

    public GameObject[] GMInterfaces;

    public Text Result;
    public Text PlayerName;

    public bool isLocal;

    [SerializeField]
    public PlayerData Data;
    public GameDataContainer Skin;
    public bool CanUpdate = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Data);
        }
        else if (stream.IsReading)
        {
            Data = (PlayerData)stream.ReceiveNext();
        }
    }

    public void HandleInterfaces()
    {
        if (isLocal)
        {

        }
        else
        {
            SetupIntgerface(CommonInterfaces, false);
            SetupIntgerface(PlayerInterfaces, false);
            SetupIntgerface(GMInterfaces, false);
        }
    }

    public void SetupIntgerface(GameObject[] interfaces, bool flag)
    {
        foreach (GameObject obj in interfaces)
        {
            obj.SetActive(flag);
        }
    }

    public void Start()
    {
        isLocal = base.photonView.IsMine;
        HandleInterfaces();
        StartCoroutine(DelayedStart());
    }

    public IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);
        CanUpdate = true;
        Common.SetupPlayerInfo();
        SetupEffects();
        SetupInventory();
        SetupAttributes();
        GetPlayerInfo();
    }

    public void ChangeObjectActiveState(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
    
    public void Update()
    {
        if(CanUpdate)
        {
            if (isLocal)
            {
                Movement();
                SetPlayerInfo();
            }
            GetPlayerInfo();
        }
    }

    public void FixedUpdate()
    {
        Player.Attributes.CalculateAttributes();
    }

    #region Common

    public void Movement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("ПКМ нажата");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, LayerMask))
            {
                NewPosition = new Vector3(hit.point.x, 0, hit.point.z);
                //Debug.Log(NewPosition);
            }
        }
        Common.MoveTo(NewPosition);
    }

    public void CallDice(int n)
    {
        Common.CallDice(n);
    }

    public void ShowDice()
    {
        
    }
    
    public void SetPlayerInfo()
    {
        Data.Name = Common.GetName();
        Data.Dice = Common.GetDiceResult();
        Data.Skin = Common.GetSkin();
    }

    public void GetPlayerInfo()
    {
        Result.text = Data.Dice;
        PlayerName.text = Data.Name;
        if(Skin.DataID != Data.Skin)
        {
            Debug.Log(42 + " " + GameManager.instance.CurrentThemePack.Name);
            Skin.UIPart.ImageHandler.ShowImage(GameManager.instance.CurrentThemePack.Avatars[Data.Skin].Image, Skin.UIPart.ImageView);
            Skin.DataID = Data.Skin;
        }
    }

    #endregion

    #region Player

    public void SetupEffects()
    {
        Player.SetupEffects(Data);
    }

    public void SetupInventory()
    {
        Player.SetupInventory(Data);
    }

    public void SetupAttributes()
    {
        Player.SetupAttributes(Data);
    }

    #endregion
}
