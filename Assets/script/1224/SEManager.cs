using System;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    [Header("参照オブジェクト")]
    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _GameOverUI;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pause;
    UIManager uIManagerScript;
    Manager managerScript;
    Enemy enemyScript;



    [Header("BGM")]
    [SerializeField] private AudioClip _titleBGM;
    [SerializeField] private AudioClip _gameoverBGM;
    [SerializeField] private AudioClip _inGameBGM;
    [SerializeField] private AudioSource audiosouceBGM;
    [Tooltip("前フレームのBGM")] private int currentAudio = -1;

    [Header("SE")]
    [SerializeField] private AudioClip _playerDeath;
    // 別スクリプトで
    [SerializeField] private AudioClip _enemyDeath; // Enemy
    [SerializeField] private AudioClip _bulletFire; // Manager
    [SerializeField] private AudioClip _enemyApear; // Enemy
    // ここまで
    [SerializeField] private AudioClip _gameStart;
    [SerializeField] private AudioClip _pausing;
    [SerializeField] private AudioSource audiosouceSE;
    [NonSerialized]  public int currentSE;
    

    // Start is called before the first frame update
    void Start()
    {
        audiosouceBGM = GetComponent<AudioSource>();
        uIManagerScript = FindObjectOfType<UIManager>();
        managerScript = FindObjectOfType<Manager>();
        enemyScript = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // BGM
        int nextAudio = BGMWhat();
        // 変わった瞬間だけ
        if (nextAudio != currentAudio)
        {
            currentAudio = nextAudio;

            switch (currentAudio)
            {
                case 1:
                    audiosouceBGM.clip = _titleBGM;
                    break;
                case 2:
                    audiosouceBGM.clip = _inGameBGM;
                    break;
                case 3:
                    audiosouceBGM.clip = _gameoverBGM;
                    break;
                default:
                    return;
            }
            // 再生
            audiosouceBGM.Play();
        }

        // SE
        int which = SEWhat();
        // 変わった瞬間だけ
        if(which != currentSE)
        {
            if (managerScript.change != 0)
                which = managerScript.change;
            if (enemyScript.changeA != 0)
                which = enemyScript.changeA;
            if (enemyScript.changeD != 0)
                which = enemyScript.changeD;

            currentSE = which;

            switch (currentSE)
            {
                case 1:
                    audiosouceSE.PlayOneShot(_playerDeath);
                    Debug.Log("currentSE");
                    break;
                case 2:
                    audiosouceSE.PlayOneShot(_enemyDeath);
                    Debug.Log("currentSE");
                    break;
                case 3:
                    audiosouceSE.PlayOneShot(_bulletFire);
                    Debug.Log("currentSE");
                    break;
                case 4:
                    audiosouceSE.PlayOneShot(_enemyApear);
                    Debug.Log("currentSE");
                    break;
                case 5:
                    audiosouceSE.PlayOneShot(_gameStart);
                    Debug.Log("currentSE");
                    break; 
                case 6:
                    audiosouceSE.PlayOneShot(_pausing);
                    Debug.Log("currentSE");
                    break;
            }
        }
    }

    /// <summary>
    /// 何のBGMを流すか決めている
    /// </summary>
    /// <returns>
    /// 1:タイトル、2:ゲーム中、3:ゲームオーバー画面
    /// </returns>
    private int BGMWhat()
    {
        if (_title.activeSelf)
        {
            return 1;
        }
        else if (_GameOverUI.activeSelf)
        {
            return 3;
        }
        else
            // デフォルトはゲームBGM
            return 2;
    }

    /// <summary>
    /// 何のSEを流すか決定する
    /// </summary>
    /// <returns></returns>
    private int SEWhat()
    {
        // プレイヤー死亡時
        if (!_player.activeSelf)
        {
            return 1;
        }
        // enemyの特定面倒だからEnemyのスクリプトでやる
        // ゲームスタート時
        else if(_title.activeSelf && uIManagerScript.gameOverStarted)
        {
            return 2;
        }
        // pause画面に移行時
        else if(_pause.activeSelf)
        {
            return 3;
        }
        // デフォルト
        return 0;
    }
}