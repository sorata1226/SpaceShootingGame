using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //移動スピード
    public float speedX;
    public float speedZ;

    //弾
    public GameObject bullet;
    float bulletInterval;

    //敵
    public GameObject enemy;
    float enemyInterval;

    //爆発
    public GameObject explosion;

    //Sliderと体力
    Slider slider;
    int playerLife;

	// Use this for initialization
	void Start () {
        //弾のインターバル
        bulletInterval = 0.0f;

        //敵のインターバル
        enemyInterval = 0.0f;

        //体力の設定
        playerLife = 3;

        //スライダーコンポーネントを取得
        slider = GameObject.Find("Slider").GetComponent<Slider>();
		
	}
	
	// Update is called once per frame
	void Update () {
        //移動
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        print(vertical);
        print(horizontal);

        if (Input.GetKey("up"))
        {
            MoveToUp(vertical);
        }
        if (Input.GetKey("right"))
        {
            MoveToRight(horizontal);
        }
        if (Input.GetKey("left"))
        {
            MoveToLeft(horizontal);
        }
        if (Input.GetKey("down"))
        {
            MoveToBack(vertical);
        }

        //弾の生成
        bulletInterval += Time.deltaTime;
        if (Input.GetKey("space"))
        {
            if (bulletInterval >= 0.2f)
            {
                GenerateBullet();
            }
        }

        //敵の生成
        enemyInterval += Time.deltaTime;
        if(enemyInterval >= 5.0f)
        {
            GenerateEnemy();
        }
    }



    //移動するためのメソッド
    void MoveToUp(float vertical)
    {
        transform.Translate(0, 0, vertical * speedZ);
    }

    void MoveToRight(float horizontal)
    {
        transform.Translate(horizontal * speedX, 0, 0);
    }

    void MoveToLeft(float horizontal)
    {
        transform.Translate(horizontal * speedX, 0, 0);
    }

    void MoveToBack(float vertical)
    {
        transform.Translate(0, 0, vertical * speedZ);
    }

    //弾を生成するためのメソッド
    void GenerateBullet()
    {
        bulletInterval = 0.0f;
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    //敵を生成するためのメソッド
    void GenerateEnemy()
    {
        Quaternion q = Quaternion.Euler(0, 180, 0);
        enemyInterval = 0.0f;
        //ランダムな場所に生成
        Instantiate(enemy, new Vector3(Random.Range(-100, 100), transform.position.y, transform.position.z + 200), q);
        //自身の目の前に生成
        Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, transform.position.z + 200), q);
    }

    //爆発
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "EnemyBullet")
        {
            //弾が当たれば体力を1減らす
            playerLife--;
            //sliderのvalueに、体力を代入する
            slider.value = playerLife;
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(coll.gameObject);
            //体力が0以下になれば、戦闘機が爆発するようにする
            if (playerLife <= 0)
            {
                Destroy(this.gameObject);

                //ハイスコア更新
                ScoreController obj = GameObject.Find("Main Camera").GetComponent<ScoreController>();
                obj.SaveHighScore();

            }
            //Destroy(this.gameObject);
        }
    }

}
