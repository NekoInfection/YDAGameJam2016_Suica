using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    public Vector2 velocity;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        //加速度に応じて移動します。
        transform.position += (Vector3)velocity;

        velocity *= 0.75f; 
    }
}
