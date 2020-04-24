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
    public GMEntity GM;
    public GameObject[] GMInterfaces;

    public Text Result;
    public Text PlayerName;

    public bool isLocal;
    public bool isMaster;

    [SerializeField]
    public PlayerData Data;
    public GameDataContainer Skin;
    public bool AddMarkerMode;
    public Dropdown MarkerSelector;
    public bool CanUpdate = false;

    public Text MarkerText;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Data.Name);
            stream.SendNext(Data.Dice);
            stream.SendNext(Data.Skin);

            stream.SendNext(Data.BaseAttributes.Count);
            foreach(float value in Data.BaseAttributes)
                stream.SendNext(value);

            stream.SendNext(Data.AdditionalAttributes.Count);
            foreach (float value in Data.AdditionalAttributes)
                stream.SendNext(value);

            stream.SendNext(Data.Effects.Count);
            foreach (int value in Data.Effects)
                stream.SendNext(value);

            stream.SendNext(Data.Inventory.Count);
            foreach (int value in Data.Inventory)
                stream.SendNext(value);

            stream.SendNext(Data.Equipment.Count);
            foreach (int value in Data.Equipment)
                stream.SendNext(value);
        }
        else if (stream.IsReading)
        {
            Data.Name = (string)stream.ReceiveNext();
            Data.Dice = (string)stream.ReceiveNext();
            Data.Skin = (int)stream.ReceiveNext();

            int count = (int)stream.ReceiveNext();
            for(int i = 0; i < count; i++)
                Data.BaseAttributes[i] = (float)stream.ReceiveNext();

            count = (int)stream.ReceiveNext();
            for (int i = 0; i < count; i++)
                Data.AdditionalAttributes[i] = (float)stream.ReceiveNext();

            count = (int)stream.ReceiveNext(); Data.Effects.Clear();
            for (int i = 0; i < count; i++)
                Data.Effects.Add((int)stream.ReceiveNext());

            count = (int)stream.ReceiveNext(); Data.Inventory.Clear();
            for (int i = 0; i < count; i++)
                Data.Inventory.Add((int)stream.ReceiveNext());

            count = (int)stream.ReceiveNext(); Data.Equipment.Clear();
            for (int i = 0; i < count; i++)
                Data.Equipment.Add((int)stream.ReceiveNext());
        }
    }

    public void HandleInterfaces()
    {
        if (isLocal)
        {
            SetupInterface(CommonInterfaces, true);
            if (isMaster)
            {
                SetupInterface(GMInterfaces, true);
                SetupInterface(PlayerInterfaces, false);
            }
            else
            {
                SetupInterface(GMInterfaces, false);
                SetupInterface(PlayerInterfaces, true);
            }
        }
        else
        {
            SetupInterface(CommonInterfaces, false);
            SetupInterface(PlayerInterfaces, false);
            SetupInterface(GMInterfaces, false);
        }
    }

    public void SetupInterface(GameObject[] interfaces, bool flag)
    {
        foreach (GameObject obj in interfaces)
        {
            obj.SetActive(flag);
        }
    }

    public void Start()
    {
        isLocal = base.photonView.IsMine;
        isMaster = PhotonNetwork.IsMasterClient;
        Data.PlayerId = base.photonView.ViewID;

        GameManager.instance.Players.Add(this);

        foreach (PlayerController pc in GameManager.instance.Players)
        {
            PhotonView view = PhotonView.Get(pc);
            photonView.RPC("UpdatePlayerView", RpcTarget.All);
        }

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
        GM.SetupLocationsDropDown();
        GM.SetupDropDown(GameManager.instance.CurrentThemePack.Objects, MarkerSelector);
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
        if(AddMarkerMode)
        {
            AddMarker();
            MarkerText.text = "Режим установки";
        }
        else
        {
            MarkerText.text = "Установить метку";
        }
    }

    public void SetMarkerMode()
    {
        AddMarkerMode = true;
    }

    public void AddMarker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ЛКМ нажата");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, LayerMask))
            {
                GameManager.instance.ClientAddMarker(MarkerSelector.value, hit.point);
                AddMarkerMode = false;
            }
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

    #region GM

    public void ClientSetMap(int index)
    {
        GameManager.instance.SetMap(index);
        PhotonView view = PhotonView.Get(this);
        photonView.RPC("SetMap", RpcTarget.All, index);
    }

    public void CallRpcOnPlayerWithId(string method, int playerId, int objId)
    {
        foreach (PlayerController pc in GameManager.instance.Players)
        {
            if (pc.Data.PlayerId == playerId)
            {
                PhotonView view = PhotonView.Get(this);
                photonView.RPC(method, RpcTarget.All, objId);
            }
        }
    }

    public void ClientAddItem(int playerId, int itemId)
    {
        CallRpcOnPlayerWithId("AddItem", playerId, itemId);
    }

    public void ClientRemoveItem(int playerId, int itemId)
    {
        CallRpcOnPlayerWithId("RemoveItem", playerId, itemId);
    }

    public void ClientAddEquipment(int playerId, int itemId)
    {
        CallRpcOnPlayerWithId("AddEquipment", playerId, itemId);
    }

    public void ClientRemoveEquipment(int playerId, int itemId)
    {
        CallRpcOnPlayerWithId("RemoveEquipment", playerId, itemId);
    }

    public void ClientAddEffect(int playerId, int itemId)
    {
        CallRpcOnPlayerWithId("AddEffect", playerId, itemId);
    }

    public void ClientRemoveEffect(int playerId, int itemId)
    {
        CallRpcOnPlayerWithId("RemoveEffect", playerId, itemId);
    }

    public void ClientSetAttribute(int playerId, float value, int index)
    {
        foreach (PlayerController pc in GameManager.instance.Players)
        {
            if (pc.Data.PlayerId == playerId)
            {
                PhotonView view = PhotonView.Get(this);
                photonView.RPC("SetAtribute", RpcTarget.All, value, index);
            }
        }
    }

    #region RPC
    [PunRPC]
    public void SetMap(int index)
    {
        GameManager.instance.SetMap(index);
    }

    [PunRPC]
    public void AddItem(int id)
    {
        Player.Inventory.Add(id);
    }

    [PunRPC]
    public void RemoveItem(int id)
    {
        Player.Inventory.Remove(id);
    }

    [PunRPC]
    public void AddEquipment(int id)
    {
        Player.Inventory.AddEquipment(id);
    }

    [PunRPC]
    public void RemoveEquipment(int id)
    {
        Player.Inventory.RemoveEquipment(id);
    }

    [PunRPC]
    public void SetAtribute(float value, int index)
    {
        Data.BaseAttributes[index] = value;
    }

    [PunRPC]
    public void AddEffect(int id)
    {
        Player.Effects.Add(id);
    }

    [PunRPC]
    public void RemoveEffect(int id)
    {
        Player.Effects.Remove(id);
    }

    [PunRPC]
    public void GenerateEvent(int id, int playerId)
    {

    }

    [PunRPC]
    public void UpdatePlayerView()
    {
        Common.PlayerInfo.UpdatePlayerView(GM.GMInteraction);
    }

    #endregion

    #endregion

    #region Networking

    public void OnDestroy()
    {
        GameManager.instance.Players.Remove(this);

        foreach (PlayerController pc in GameManager.instance.Players)
        {
            PhotonView view = PhotonView.Get(pc);
            photonView.RPC("UpdatePlayerView", RpcTarget.All);
        }
    }

    #endregion
}
