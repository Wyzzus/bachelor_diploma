using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{
    public LayerMask LayerMask;
    public Vector3 NewPosition = Vector3.zero;
    public CommonEntity Common;
    public PlayerEntity Player;

    public Text Result;
    public Text PlayerName;

    public bool isLocal;

    public PlayerData Data;
    public GameDataContainer Skin;

    public override void Start()
    {
        base.Start();
        //Data = new PlayerData();
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
        Movement();
        if(isLocal)
            SetPlayerInfo();
        GetPlayerInfo();
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
