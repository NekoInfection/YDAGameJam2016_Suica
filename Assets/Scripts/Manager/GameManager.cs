using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return instance;
        }
    }

    private List<ManagerBase> managerList = new List<ManagerBase>();
    public List<ManagerBase> ManagerList { get { return managerList; } }

    

	void Awake () {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        var scene = SceneManager.Instance;
        var resources = ResourceManager.Instance;
	}
	


}
