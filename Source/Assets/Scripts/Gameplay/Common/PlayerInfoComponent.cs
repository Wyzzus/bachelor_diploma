using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoComponent : EntityComponent
{
    public PlayerData Data;

    public ScrollViewHandler PlayerViewHandler;
    
    [Header ("UI")]
    public InputField Name;
    public Dropdown SkinSelector;

    public DndObjectUI PlayerMarker;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public void FillSkinSelector(List<Avatar> Skins)
    {
        SkinSelector.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (Avatar a in Skins)
        {
            Dropdown.OptionData option = new Dropdown.OptionData
            {
                text = a.Name
            };
            options.Add(option);
        }
        SkinSelector.AddOptions(options);
    }

    public void UpdatePlayerView(GMInteractionComponent component)
    {
        PlayerViewHandler.UpdatePlayersView(GameManager.instance.Players, component);
    }
}
