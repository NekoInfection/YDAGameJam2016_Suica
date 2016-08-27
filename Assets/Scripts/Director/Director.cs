using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {

    void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        var resources = ResourceManager.Instance;
    }
}
