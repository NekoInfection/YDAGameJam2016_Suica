using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 0.1f;
    public State state;
    InputManager inputManager;

    // Use this for initialization
    void Start () {
        inputManager = GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
        switch (state) {
            case State.IDLE:
            break;
            case State.MOVING:
                
            break;
            case State.ATTACKING:
                
            break;
            case State.DEATH:
            break;
        }
	}

    void StateTransition(State nextState) {
        switch (state) {

        }
    }
}
