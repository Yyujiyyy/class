using System;
using System.Collections.Generic;
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

    [Header("パーティクル")]
    [SerializeField] private ParticleSystem _deathParticle;
    [SerializeField] private ParticleSystem _pDeathParticle;

    [Header("SE")]
    public int changeA = 0;
    public int changeD = 0;

    [Header("Score")]
    [NonSerialized] public int _score = 0;
    [NonSerialized] public int _highScore = 0;

    [SerializeField] private GameObject _title;
    bool _isCleared = false;


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
                    changeA = 4;
                    break;
                }
                else
                    changeA = 0;

            }
            timer = 0;
        }
        DEnemyBullet();

        // 
        if(!_player.activeSelf && _pActive)
        {
            _pActive = false;
            Invoke(_methodName, 2);
        }

        // タイトルが出現時のリセット
        if (_title.activeSelf && !_isCleared)
        {
            AllDeactivate();
            _isCleared = true;
        }

        if (!_title.activeSelf)
        {
            _isCleared = false;
        }
    }
    /// <summary>
    /// enemyとbulletの破壊処理
    /// </summary>
    /// 
    void DEnemyBullet()
    {
        //軽量化
        if (0.05f <= timer2)
        {
            foreach ( GameObject enemy in Enemys)
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

                            enemy.SetActive(false);
                            // enemy破壊時のScore加算
                            _score += 100;
                            // ハイスコア更新
                            if (_highScore < _score)
                            {
                                _highScore = _score;
                            }
                            
                            bullets.SetActive(false);
                            Instantiate(_deathParticle, enemyPos, Quaternion.identity);
                            // enemy死亡時の音に変更
                            changeD = 2;
                        }
                        else
                            changeD = 0;
                    }
                    else
                        changeD = 0;
                    // それ以外はずっと0
                }

                // playerがenemyに当たったら死亡
                if(_player.activeSelf)
                {
                    if (enemyPos.x - halfw <= _playerT.position.x && _playerT.position.x <= enemyPos.x + halfw)
                    {
                        if (enemyPos.y - halfh <= _playerT.position.y && _playerT.position.y <= enemyPos.y + halfh)
                        {//enemyの判定は忠実に、playerの判定は、中心が当たったら
                            enemy.SetActive(false);
                            _player.SetActive(false);
                            Instantiate(_pDeathParticle, _playerT.position, Quaternion.identity);
                        }
                    }
                }
            }
            timer2 = 0;
        }
    }
    /// <summary>
    /// プレイヤーの復活
    /// </summary>
    public void PlayerActive()
    {
        _playerT.position = new Vector3(-7, 0, 0);
        _player.SetActive(true);
        _pActive = true;
    }

    void AllDeactivate()
    {
        foreach (var enemy in Enemys)
            enemy.SetActive(false);

        foreach (var bullet in manager.Bullets)
            bullet.SetActive(false);

        _score = 0;
    }
}
