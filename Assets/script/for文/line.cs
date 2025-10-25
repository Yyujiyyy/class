using UnityEngine;

public class line : MonoBehaviour
{
    [SerializeField] GameObject[] Box;   //cube

    private float timer;                        //線
    private float interval = 0.1f;
    [SerializeField] private float Maxrow = 9;
    private int Counter = 0;
    private int lineCounter = 0;

    private float timer2;
    private int PlaneCounter = 0;               //平面
    [SerializeField] private int Maxrowline = 9;
    private int number = 0;

    private float timer3;
    private int independentnumber = 0;          //上面だけが開いている箱
    private int BoxX, BoxY, BoxZ;
    private int MaxBox = 9;
    private bool X;
    private bool S;
    private bool Height;
    private int flontX, flontY;
    private bool behind;




    // Start is called before the first frame update
    void Start()
    {
        PlaneCounter = 20;      //ずらして視認する

        Height = true;
        behind = false;
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
        if (interval <= timer2 && PlaneCounter < Maxrowline + 20 && lineCounter < /*列の最大数*/Maxrowline)
        {
            Instantiate(Box[number % Box.Length], new Vector3(PlaneCounter, 0, lineCounter), Quaternion.identity);

            PlaneCounter++; number++;
            timer2 = 0;
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

        if (MaxBox - 1 <= flontX && MaxBox <= flontY && Height && !behind)   //強制停止のための条件
        {
            Height = false;
            flontX = 0; flontY = 0;         //初期化
            BoxZ++;
            BoxY = 1;
            BoxX = 0;
            X = true;                                       //横に移行
        }

        else if (interval <= timer3 && flontY < MaxBox && Height)
        {
            num = flontY++;           //条件更新

            FlontInstant();
        }
        else if (MaxBox <= flontY && Height)
        {
            flontX++;
            flontY = 0;
        }
        

        //---------------------------------------------
        //横

        if (MaxBox <= BoxY && MaxBox <= BoxX && BoxZ < MaxBox && !Height)       //このif文は先に書かないといけない。
        {                                           //これをelse ifに持ってくると、他の条件もtrueになってしまうため。
            BoxZ++;                                 //絶対にかぶらない物を先に持ってくると条件のかぶりをなくせる。
            BoxY = 1;
            BoxX = 0;               //Zのみを++してX,Yを初期化
            Debug.Log("c");
            X = true;               //この条件を満たすif文は後に書かないと反映されない。

            //Debug.Log(BoxX);
            //Debug.Log(BoxY);
            //Debug.Log(BoxZ);
        }
        else if (MaxBox - 1 < BoxY)   //１回のみ実行され、条件を変える
        {
            X = false;              //stop
            BoxY = 1;               //reset
            S = true;               //start
            Debug.Log("change");
        }
        else if (interval <= timer3 && BoxX < MaxBox - 1 && S)                         //MaxBoxまで繰り返し実行（X）
        {
            num = BoxX++;           //reroad

            Instant();                                  //Yを0に戻さないといけないので処理を分ける
            //Debug.Log("s");
        }
        else if (interval <= timer3 && MaxBox <= BoxX && BoxY < MaxBox && !behind)
        {
            S = false;
            num = BoxY++;           //reroad

            Instant();

            Debug.Log("b");
        }

        if (interval <= timer3 && BoxY <= MaxBox && X)
        {
            Instant();

            num = BoxY++;       //後から条件追加することで、(0,0,0)から始める
        }

        //-------------------------------------------
        //後

        if(MaxBox <= BoxZ && !behind)
        {
            Height = true;
            behind = true;
        }
    }

    void Instant()
    {
        Instantiate(Box[independentnumber % Box.Length], new Vector3(BoxX + 40, BoxY, BoxZ), Quaternion.identity);

        independentnumber++;        //どんな場合でも++;

        timer = 0;
        timer2 = 0;
        timer3 = 0;
    }

    void FlontInstant()
    {
        Instantiate(Box[independentnumber % Box.Length], new Vector3(flontX + 40, flontY, BoxZ ), Quaternion.identity);

        independentnumber++;        //どんな場合でも++;

        timer = 0;
        timer2 = 0;
        timer3 = 0;
    }
}