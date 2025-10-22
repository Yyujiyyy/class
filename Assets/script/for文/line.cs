using UnityEngine;

public class line : MonoBehaviour
{
    Transform T;

    [SerializeField] GameObject[] Box;   //cube

    float timer;                        //線
    float interval = 0.1f;
    [SerializeField] float Maxrow = 9;
    int Counter = 0;
    int lineCounter = 0;

    float timer2;
    int PlaneCounter = 0;               //平面
    [SerializeField] int Maxrowline = 9;
    int number = 0;

    float timer3;
    int independentnumber = 0;          //上面だけが開いている箱
    int BoxX, BoxY, BoxZ;
    int MaxBox = 8;
    bool X;
    bool S;
    bool Flont;

    // Start is called before the first frame update
    void Start()
    {
        T = transform;

        PlaneCounter = 20;      //ずらして視認する

        Flont = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        timer3 += Time.deltaTime;

        row();

        rowline();

        OpenBox();
    }

    void row()
    {
        if (interval <= timer && Counter < Maxrow)   //interval秒間隔で
        {
            Instantiate(Box[Counter % Box.Length], new Vector3(Counter, 0, 0), Quaternion.identity);

            Counter++;
            timer = 0;
        }
    }

    void rowline()
    {
        if (interval <= timer && PlaneCounter < Maxrowline + 20 && lineCounter < /*列の最大数*/Maxrowline)
        {
            Instantiate(Box[number % Box.Length], new Vector3(PlaneCounter, 0, lineCounter), Quaternion.identity);

            PlaneCounter++; number++;
            timer = 0;
            //Debug.Log(PlaneCounter);
        }
        else if (Maxrowline + 20 <= PlaneCounter)       //列操作
        {
            lineCounter++;                              //改列
            PlaneCounter = 20;   //reset
            //Debug.Log("a");
        }
    }



    //===============================================================
    //                    上面だけが開いている箱
    //===============================================================

    void OpenBox()
    {
        int num;                            //条件

        //前

        if (interval <= timer && BoxY < MaxBox && Flont)
        {
            num = BoxY++;           //条件更新

            Instant();
            return;
        }
        else if (MaxBox <= BoxY)
        {
            BoxX++;
            BoxY = 0;
            return;
        }

        if (MaxBox <= BoxX && MaxBox <= BoxY)
        {
            Flont = false;
            //X = true;
            return;
        }


        //横

        if (MaxBox - 1 <= BoxY && MaxBox <= BoxX && BoxZ < MaxBox && !Flont)       //このif文は先に書かないといけない。
        {                                           //これをelse ifに持ってくると、他の条件もtrueになってしまうため。
            BoxZ++;                                 //絶対にかぶらない物を先に持ってくると条件のかぶりをなくせる。
            BoxY = 0;
            BoxX = 0;               //Zのみを++してX,Yを初期化
            Debug.Log("c");
            X = true;               //この条件を満たすif文は後に書かないと反映されない。

            //Debug.Log(BoxX);
            //Debug.Log(BoxY);
            //Debug.Log(BoxZ);
        }
        else if (MaxBox <= BoxY)   //１回のみ実行され、条件を変える
        {
            X = false;              //stop
            BoxY = 0;               //reset
            S = true;               //start
            Debug.Log("change");
        }
        else if (interval <= timer && BoxX < MaxBox && S)                         //MaxBoxまで繰り返し実行（X）
        {
            num = BoxX++;           //reroad

            Instant();                                  //Yを0に戻さないといけないので処理を分ける
            //Debug.Log("s");
        }
        else if (interval <= timer && MaxBox <= BoxX && BoxY < MaxBox - 1)
        {
            S = false;
            num = BoxY++;           //reroad

            Instant();

            Debug.Log("b");
        }

        if (interval <= timer && BoxY < MaxBox && X)
        {
            Instant();

            num = BoxY++;       //後から条件追加することで、(0,0,0)から始める
        }

        void Instant()
        {
            Instantiate(Box[independentnumber % Box.Length], new Vector3(BoxX + 40, BoxY, BoxZ), Quaternion.identity);

            independentnumber++;        //どんな場合でも++;

            timer = 0;
            timer2 = 0;
            timer3 = 0;
        }
    }
}