using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ポーズしているときの処理
/// </summary>
public class Pause : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction attackAction;

    private int index = 0;

    [SerializeField] private GameObject _goTitle;
    [SerializeField] private GameObject _goGame;
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _title;
    [Tooltip("タイトルかゲームか")] GameObject[] UI = new GameObject[2];

    int preindex = 0;
    private Vector2 prev;

    void OnEnable()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.Enable();

        attackAction = InputSystem.actions.FindAction("Attack");
        attackAction.Enable();
        attackAction.performed += OnAttack;
    }
    void OnDisable()
    {
        attackAction.performed -= OnAttack;

        moveAction.Disable();
        attackAction.Disable();
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale != 0) return;

        switch (index)
        {
            case 0:
                ResumeGame();
                break;
            case 1:
                GoToTitle();
                break;
        }
    }

    private void Start()
    {
        UI[0] = _goTitle;
        UI[1] = _goGame;
        // 初期状態
        UI[0].SetActive(true);
        UI[1].SetActive(false);

        prev = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // すべてこの中に書く
        if (Time.timeScale == 0)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            Vector2 move = moveValue;
            
            if (move.x > 0.5f && prev.x <= 0.5f)
            {
                preindex = index;
                index = Mathf.Clamp(index + 1, 0, UI.Length - 1);
            }

            if (move.x < -0.5f && prev.x >= -0.5f)
            {
                preindex = index;
                index = Mathf.Clamp(index - 1, 0, UI.Length - 1);
            }

            prev = move;
        }

        if (index != preindex)
        {
            UI[preindex].SetActive(false);
            UI[index].SetActive(true);
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        _pause.SetActive(false); // ポーズUIを消す
    }

    void GoToTitle()
    {
        _pause.SetActive(false); // ポーズUIを消す
        _title.SetActive(true);

        // UIと選択状態を初期化
        index = 0;
        preindex = 0;
        prev = Vector2.zero;
        UI[0].SetActive(true);
        UI[1].SetActive(false);

        Time.timeScale = 0f;
    }
}
