using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersListComponent : MonoBehaviour
{
    public List<PlayerData> players;
    public GameObject Prefab;
    public RectTransform Content;

    // Start is called before the first frame update
    void Start()
    {
        FillList();
        //players.Add(GameObject.FindWithTag("Player").GetComponent<PlayerData>());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillList()
    {
        foreach (RectTransform child in Content)
        {
            Destroy(child.gameObject);
        }
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            PlayerData data = player.GetComponent<PlayerComponent>().playerData;
            players.Add(data);
            GameObject clone = Instantiate<GameObject>(Prefab, Content);
            clone.GetComponent<PlayerContainer>().Setup(data);
        }
    }
}
