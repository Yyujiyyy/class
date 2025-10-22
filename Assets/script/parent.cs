using UnityEngine;

public class Parent : MonoBehaviour
{
    Transform T;
    Rigidbody rb;
    AudioClip audioClip;
    Collider C;
    AudioSource Audiosource;

    Vector3 startpos, endpos;
    float time;
    [SerializeField] float Second;
    float clickcounter = 0;
    float now;
    [SerializeField] GameObject text;


    // Start is called before the first frame update
    void Start()
    {
        T = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        audioClip = GetComponent<AudioClip>();
        C = GetComponent<Collider>();
        Audiosource = GetComponent<AudioSource>();

        this.transform.parent = GameObject.Find("parent").transform;      //親子関係

        startpos = new Vector3(0, 0, 0);
        endpos = new Vector3(0, 5, 0);
        time = 0;        //初期化

        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))        //線形補間
        {
            time += Time.deltaTime / Second;
            this.T.position = Vector3.Lerp(startpos, endpos, time);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))        //親子関係解除
        {
            T.parent = null;
        }



        if (Input.GetMouseButtonDown(0))
        {
            if (clickcounter == 0)
            {
                now = Time.time;
            }

            clickcounter += 1;
            Debug.Log(clickcounter);
        }

        if (Time.time < now + 1)
        {
            if (clickcounter >= 5)
            {
                text.SetActive(true);
            }
        }

        if (Time.time >= now + 1)
            clickcounter = 0;

        if(text)
        {
            Invoke(nameof(Hidetext), 3);
        }
    }

    void Hidetext()
    {
        text.SetActive(false);
    }
}
