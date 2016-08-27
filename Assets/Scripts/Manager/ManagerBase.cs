using UnityEngine;
using System.Collections;

public enum ManagerID
{
    NULL,
    RESOURCES,
    SCENE,
    EFFECT,
}

public class ManagerData
{
    public ManagerBase managerBase = null;
    public ManagerID managerID = ManagerID.NULL;
    public bool isActive = false;
}

public class ManagerBase : MonoBehaviour {

    private ManagerData data = new ManagerData();
    public ManagerData GetData { get { return data; } }

    protected void InitManager(ManagerBase manager,ManagerID id)
    {
        data.managerBase = manager;
        data.managerID = id;
        data.isActive = gameObject.activeInHierarchy;

        GameManager.Instance.ManagerList.Add(this);
        Debug.Log(data.managerID.ToString() + " Initialized!");
    }

    public void ChangeActive(bool value)
    {
        this.gameObject.SetActive(value);
    }


}
