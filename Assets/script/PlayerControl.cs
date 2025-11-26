using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Transform _T;
    Vector3 moveDir;
    Vector3 Wv, Av, Sv, Dv;

    bool isJumping = false;
    [Tooltip("ジャンプの強さ")] Vector3 JumpF;
    Rigidbody _rb;

    [SerializeField] GameObject _foot;
    public Jump Jump;

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

    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1);

        Move();
        Jumping();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Jumping();
    }

    void Jumping()
    {
        //Jump
        //Debug.Log(isJumping);
        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(JumpF, ForceMode.Impulse);
                isJumping = true;

                Debug.Log("jump");
            }
        }

        if (Jump.CheckGroundStatus())
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }
    }

    void Move()
    {
        moveDir = Vector3.zero;     //向き

        //移動

        if (Input.GetKey(KeyCode.W))
        {
            moveDir += Wv;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDir += Sv;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDir += Av;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDir += Dv;
        }

        if (moveDir != Vector3.zero)     //向きが違うとき
        {
            _T.rotation = Quaternion.LookRotation(moveDir);

            _T.position += moveDir * 0.1f;
        }
    }
}