using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    GameObject _player;
    [SerializeField] private GameObject _GameOverUI;
    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _pause;

    private InputAction attackAction;
    private InputAction pauseAction;

    [Header("time")]
    [NonSerialized] public bool gameOverStarted = false;

    void OnEnable()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        pauseAction = InputSystem.actions.FindAction("Pause");
        attackAction.Enable();
        pauseAction.Enable();

        attackAction.performed += OnAttack;
        pauseAction.performed += OnPause;
    }

    void OnDisable()
    {
        attackAction.performed -= OnAttack;
        pauseAction.performed -= OnPause;
        attackAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _GameOverUI.SetActive(false);
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが非アクティブになった「瞬間」だけ
        if(!_player.activeSelf && !gameOverStarted && !_title.activeSelf)
        {
            gameOverStarted = true;
            Time.timeScale = 0.2f;   // ★ スロー開始
            StartCoroutine(GameOverSlow());
        }

        // GameOver演出中は Coroutine に任せる
        if (!gameOverStarted && (_pause.activeSelf || _title.activeSelf))
        {
            Time.timeScale = 0f;
        }
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        // タイトル画面で攻撃したらゲーム開始
        if (_title.activeSelf)
        {
            _title.SetActive(false);
            gameOverStarted = false;
        }
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        _pause.SetActive(!_pause.activeSelf);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameOverSlow()
    {
        float t = 0f;
        float start = Time.timeScale;
        float end = 0.2f;

        while (t < 1f)
        {
            //timeScaleの影響を受けないdeltaTime
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(start, end, t);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(GameOverSequence());
    }

    /// <summary>
    /// ゲームオーバー画面をオフ、タイトルをオン
    /// </summary>
    /// <param name="Seconds"></param>
    /// <returns></returns>
    private IEnumerator GameOverSequence()
    {
        Time.timeScale = 1f;     // ★ 元に戻す
        _GameOverUI.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);

        _GameOverUI.SetActive(false);
        _title.SetActive(true);
    }
}