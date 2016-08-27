using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Vertical() {
        //アニメーションで棒を縦に振りかざす
        Debug.Log("Vertical");
    }

    public void Horizontal() {
        //アニメーションで棒を横に振りかざす
        Debug.Log("Horizontal");
    }
}
