using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _player;

    Transform _playerT;
    MeshRenderer mr;

    List<GameObject> Enemys = new List<GameObject>();
    //ManagerスクリプトのListを使うため参照
    Manager manager;
    //必要条件
    float timer = 0;
    float timer2 = 0;
    float numberOfEnemy = 10;
    //あたり判定
    float halfw, halfh;
    
    bool _pActive;

    const string _methodName = "PlayerActive";

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        _playerT = _player.transform;
        //enemyのあたり判定のため
        mr = _enemy.GetComponent<MeshRenderer>();

        //当たり判定
        halfw = mr.bounds.extents.x;
        halfh = mr.bounds.extents.y;

        for (int i = 0; i < numberOfEnemy; i++)
        {
            GameObject obj = Instantiate(_enemy, new Vector3(0, 0, 0), Quaternion.identity);
            obj.SetActive(false);
            Enemys.Add(obj);
        }

        manager = FindObjectOfType<Manager>();

        _pActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        if (2 <= timer)
        {
            for (int i = 0; i < numberOfEnemy; i++)
            {   //SetActive(false)のものを０から探してくる
                if (!Enemys[i].activeSelf)
                {
                    Enemys[i].SetActive(true);
                    Enemys[i].transform.position = _playerT.position + new Vector3(10, 0, 0);
                    break;
                }
            }
            timer = 0;
        }
        DEnemyBullet();

        if(!_player.activeSelf && _pActive)
        {
            _pActive = false;
            Invoke(_methodName, 2);
        }
    }
    /// <summary>
    /// enemyとbulletの破壊処理
    /// </summary>
    /// 
    void DEnemyBullet()
    {
        //軽量化
        if (0.1f <= timer2)
        {
            foreach (GameObject enemy in Enemys)
            {   //SetActive(true)のenemyのみを判定
                if (!enemy.activeSelf) continue;

                Vector3 enemyPos = enemy.transform.position;

                //どの弾か判定
                foreach (GameObject bullets in manager.Bullets)
                {   //SetActive(true)のbulletsのみを判定
                    if (!bullets.activeSelf) continue;
                    //少しでも軽くするためにまとめる
                    
                    Vector3 bulletPos = bullets.transform.position;

                    if (enemyPos.x - halfw <= bulletPos.x && bulletPos.x <= enemyPos.x + halfw)
                    {
                        if (enemyPos.y - halfh <= bulletPos.y && bulletPos.y <= enemyPos.y + halfh)
                        {//enemyの判定は忠実に、弾の判定は、中心が当たったら
                            //Debug.Log("ほら");
                            enemy.SetActive(false);
                            bullets.SetActive(false);
                        }
                    }
                }

                if(_player.activeSelf)
                {
                    if (enemyPos.x - halfw <= _playerT.position.x && _playerT.position.x <= enemyPos.x + halfw)
                    {
                        if (enemyPos.y - halfh <= _playerT.position.y && _playerT.position.y <= enemyPos.y + halfh)
                        {//enemyの判定は忠実に、playerの判定は、中心が当たったら
                            //Debug.Log("ほら");
                            enemy.SetActive(false);
                            _player.SetActive(false);
                        }
                    }
                }
            }
            timer2 = 0;
        }
    }

    void PlayerActive()
    {
        _playerT.position = new Vector3(-7, 0, 0);
        _player.SetActive(true);
        _pActive = true;
    }
}
