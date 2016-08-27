using UnityEngine;
using System.Collections;
using System;

public class WaterMelon : EnemyBase {

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public override void Init()
    {
    }

    public override void Proc()
    {
        //エフェクト発生
        
        //スコア加算
        AddScore();

        //消滅
    }

    public override void AddScore()
    {

    }

    void OnCollisionEnter2D(Collision2D coll) {
        Proc();
    }
}
