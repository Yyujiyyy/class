using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Delay : MonoBehaviour
{
    Transform _T;
    Vector3 moveDir;
    Vector3 Wv, Av, Sv, Dv;

    bool isJumping = false;
    [Tooltip("ジャンプの強さ")] Vector3 JumpF;
    Rigidbody _rb;

    [SerializeField] GameObject _foot;
    public Jump Jump;

    [SerializeField, Tooltip("入力から実行までの遅延（秒）")]
    float inputDelay = 1f;

    [SerializeField, Tooltip("移動1回あたりの距離")]
    float moveDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _T = transform;

        Wv = new Vector3(0.1f, 0, 0);
        Sv = new Vector3(-0.1f, 0, 0);
        Av = new Vector3(0, 0, -0.1f);
        Dv = new Vector3(0, 0, 0.1f);

        JumpF = new Vector3(0, 5, 0);
        _rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        moveDir = Vector3.zero;     //向き

        //移動

        if (Input.GetKey(KeyCode.W))
        {
            StartCoroutine(DelayMove(Wv));
        }

        if (Input.GetKey(KeyCode.S))
        {
            StartCoroutine(DelayMove(Sv));
        }

        if (Input.GetKey(KeyCode.A))
        {
            StartCoroutine(DelayMove(Av));
        }

        if (Input.GetKey(KeyCode.D))
        {
            StartCoroutine(DelayMove(Dv));
        }

        //if (moveDir != Vector3.zero)     //向きが違うとき
        //{
        //    _T.rotation = Quaternion.LookRotation(moveDir);

        //    _T.position += moveDir * 0.1f;
        //}



        //Jump

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DelayJump());
        }

        Ground();
    }


    private void Ground()
    {
        bool grounded = false;

        if (Jump != null)
        {
            grounded = Jump.CheckGroundStatus();
        }
        else if (_foot != null)
        {
            grounded = Physics.Raycast(_foot.transform.position, Vector3.down, 0.2f);
        }
        else
        {
            // どちらも設定されていなければ安全なフォールバック（必要に応じて調整）
            grounded = true;
        }

        isJumping = !grounded;
        //Debug.Log("Ground check: " + grounded);
    }

    IEnumerator DelayMove(Vector3 dir)
    {
        yield return new WaitForSeconds(inputDelay);

        if (dir == Vector3.zero) yield break;

        // 実行時の向きで回転して移動
        _T.rotation = Quaternion.LookRotation(dir);
        _T.position += dir.normalized * moveDistance;

        Debug.Log("Delayed Move executed: " + dir);
    }

    IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(inputDelay);

        // 実行直前に接地判定を更新
        Ground();

        if (!isJumping)
        {
            _rb.AddForce(JumpF, ForceMode.Impulse);
            isJumping = true;
            Debug.Log("Delayed Jump executed");
        }
        else
        {
            Debug.Log("Delayed Jump canceled: already jumping");
        }
    }

}