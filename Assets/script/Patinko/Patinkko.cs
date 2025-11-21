using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Patinkko : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] slotTexts = new TextMeshProUGUI[3];
    [SerializeField] private Image[] slotImages = new Image[3];  // 3つのリールのImage
    [SerializeField] private Sprite[] numberSprites = new Sprite[8];  // 1～8の画像
    private int[] numbers = new int[3];
    private bool[] isStopped = new bool[3];  // 各リールが止まっているか
    private int stopCount = 0;               // 何回スペースを押したか
    [Range(0, 10)][SerializeField] private float changeInterval = 0.5f; // 数字が変わる間隔（秒）
    private float[] changeTimers = new float[3];          // 各リールごとのタイマー

    void Start()
    {
        // 最初は全部動く
        for (int i = 0; i < isStopped.Length; i++)
            isStopped[i] = false;
    }

    void Update()
    {
        // スペースを押したら順番に止める
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stopCount < isStopped.Length)
            {
                isStopped[stopCount] = true; // 今のリールを止める
                stopCount++;
            }
            else
            {
                // 全部止まった → 全リセットして回転再開
                stopCount = 0;
                for (int i = 0; i < isStopped.Length; i++)
                {
                    isStopped[i] = false;
                }
            }
        }

        // 各リール動作
        for (int i = 0; i < isStopped.Length; i++)
        {
            if (!isStopped[i])
            {
                changeTimers[i] += Time.deltaTime;

                if (changeTimers[i] >= changeInterval)
                {
                    numbers[i]++;
                    if (numbers[i] == numberSprites.Length)
                        numbers[i] = 0;
                    slotImages[i].sprite = numberSprites[numbers[i]];
                    //slotTexts[i].text = numbers[i].ToString();
                    changeTimers[i] = 0;
                }
            }
        }
    }
}
