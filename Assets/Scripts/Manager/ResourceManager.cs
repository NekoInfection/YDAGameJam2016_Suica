using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : ManagerBase
{
    class ResourceData
    {
        public string resourceName;
        public GameObject resource;

        public ResourceData(string name, GameObject resource)
        {
            resourceName = name;
            this.resource = resource;
        }
    }

    private static ResourceManager instance = null;
    public static ResourceManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("ResourceManager");
                instance = obj.AddComponent<ResourceManager>();
            }
            return instance;
        }
    }



    private GameObject[] resouce;
    private Dictionary<string, ResourceData> resourceTable = new Dictionary<string, ResourceData>();


    void Start()
    {
        InitManager(this, ManagerID.RESOURCES);
    }

    public void ResourcesLoad(string sceneName)
    {
        resourceTable.Clear();

        resouce = Resources.LoadAll<GameObject>("Prefabs/" + sceneName);
        foreach (var obj in resouce)
        {
            resourceTable.Add(obj.name, new ResourceData("Prefabs/" + obj.name, obj));
            Debug.Log(obj.name + "Loaded!!!");
        }

        resouce = null;
    }

    public GameObject GetResourceScene(string key)
    {
        if (resourceTable.ContainsKey(key))
        {
            return resourceTable[key].resource;
        }

        return null;
    }

    public void ResourcesUnLoad()
    {
        StartCoroutine(UnLoadResources());
    }

    IEnumerator UnLoadResources()
    {
        foreach (KeyValuePair<string, ResourceData> pair in resourceTable)
        {
            Resources.UnloadAsset(pair.Value.resource);
            yield return null;
        }
    }
}
