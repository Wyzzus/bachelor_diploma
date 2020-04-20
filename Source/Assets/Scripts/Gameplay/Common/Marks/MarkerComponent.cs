using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerComponent : EntityComponent
{
    public bool MarkAddMode;
    public Vector3 MarkPosition = Vector3.zero;
    public LayerMask layerMask;
    public GameObject MarkPrefab;
    public List<GameObject> MarkLists;

    public override void Start()
    {
        base.Start();
        MarkAddMode = false;
    }

    void Update()
    {
        if (MarkAddMode == true)
            AddMark();
    }


    public void ChangeMarkMode()
    {
        MarkAddMode = !MarkAddMode;
    }

    public void AddMark()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Добавление марки на карту");
            Debug.Log("ЛКМ нажата");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, layerMask))
            {
                MarkPosition = new Vector3(hit.point.x, 0, hit.point.z);
                Debug.Log(MarkPosition);
                GameObject obj = Instantiate(MarkPrefab, MarkPosition, Quaternion.identity) as GameObject;
                MarkAddMode = false;
                MarkLists.Add(obj);
                MarkComponent markComponent = obj.GetComponent<MarkComponent>();
                markComponent.SetupID(MarkLists.IndexOf(obj));
                markComponent.Marker = this;
            }
        }
    }

    public void DestroyMark(GameObject obj)
    {
        MarkLists.Remove(obj);
        Destroy(obj);
    }
}
