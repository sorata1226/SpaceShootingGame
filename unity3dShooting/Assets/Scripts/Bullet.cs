﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        //弾を5秒後に削除する
        Destroy(this.gameObject, 5);
		
	}
	
	// Update is called once per frame
	void Update () {
        //弾を移動
        transform.Translate(transform.forward * Time.deltaTime * speed);

	}
}
