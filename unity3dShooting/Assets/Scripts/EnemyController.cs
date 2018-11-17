using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    //敵の移動スピード
    float speed = 10.0f;

    //弾を撃つ間隔をあける
    float interval;

    //弾
    public GameObject enemyBullet;

    //爆発
    public GameObject explosion;

	// Use this for initialization
	void Start () {
        //intervalの初期値の設定
        interval = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-1 * transform.forward * Time.deltaTime * speed);
        //弾を撃つメソッドを呼び出す
        interval += Time.deltaTime;
        if (interval >= 0.8f)
        {
            GenerateEnemyBullet();
        }
    }

    //弾を撃つメソッド
    void GenerateEnemyBullet()
    {
        Quaternion q1 = Quaternion.Euler(0, 185, 0);
        Quaternion q2 = Quaternion.Euler(0, 175, 0);
        interval = 0.0f;
        Instantiate(enemyBullet, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z - 2), q1);
        Instantiate(enemyBullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z - 2), q2);
    }

    //衝突判定・爆発
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "PlayerBullet")
        {
            print(coll.gameObject.tag);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(coll.gameObject);
            //スコアの加算
            ScoreController obj = GameObject.Find("Main Camera").GetComponent<ScoreController>();
            obj.ScorePlus();
        }
    }

}
