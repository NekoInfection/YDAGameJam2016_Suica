using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public enum KeyType {
        Arrow,
        WASD,
        TenKey
    }

    public KeyType moveType = KeyType.Arrow;

    KeyCode keyLeft;
    KeyCode keyRight;
    KeyCode keyUp;
    KeyCode keyDown;

    public KeyCode VerticalAttack;
    public KeyCode HorizontalAttack;

    //参照
    Player player;
    Move move;
    Attack attack;
	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        move = GetComponent<Move>();
        attack = GetComponent<Attack>();

        switch (moveType) {
            case KeyType.Arrow:
                SetKeyCode(KeyCode.LeftArrow , KeyCode.RightArrow , KeyCode.UpArrow , KeyCode.DownArrow);
            break;
            case KeyType.WASD:
                SetKeyCode(KeyCode.A , KeyCode.D , KeyCode.W , KeyCode.S);
            break;
            case KeyType.TenKey:
                SetKeyCode(KeyCode.Keypad4 , KeyCode.Keypad6 , KeyCode.Keypad8 , KeyCode.Keypad2);
            break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(VerticalAttack)) {
            attack.Vertical();
        }
        if (Input.GetKey(HorizontalAttack)) {
            attack.Horizontal();
        }

        if (Input.GetKey(keyLeft)) {
            move.velocity.x += -player.speed;
        }
        if (Input.GetKey(keyRight)) {
            move.velocity.x += player.speed;
        }
        if (Input.GetKey(keyUp)) {
            move.velocity.y += player.speed;
        }
        if (Input.GetKey(keyDown)) {
            move.velocity.y += -player.speed;
        }
    }

    public void SetKeyCode(KeyCode left,KeyCode right, KeyCode up,KeyCode down) {
        keyLeft = left;
        keyRight = right;
        keyUp = up;
        keyDown = down;
    }
}
