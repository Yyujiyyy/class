using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Patinkko : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] slotTexts = new TextMeshProUGUI[3];
    [SerializeField] private Image[] slotImages = new Image[3];  // 3�̃��[����Image
    [SerializeField] private Sprite[] numberSprites = new Sprite[8];  // 1�`8�̉摜
    private int[] numbers = new int[3];
    private bool[] isStopped = new bool[3];  // �e���[�����~�܂��Ă��邩
    private int stopCount = 0;               // ����X�y�[�X����������
    [Range(0, 10)][SerializeField] private float changeInterval = 0.5f; // �������ς��Ԋu�i�b�j
    private float[] changeTimers = new float[3];          // �e���[�����Ƃ̃^�C�}�[

    void Start()
    {
        // �ŏ��͑S������
        for (int i = 0; i < isStopped.Length; i++)
            isStopped[i] = false;
    }

    void Update()
    {
        // �X�y�[�X���������珇�ԂɎ~�߂�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stopCount < isStopped.Length)
            {
                isStopped[stopCount] = true; // ���̃��[�����~�߂�
                stopCount++;
            }
            else
            {
                // �S���~�܂��� �� �S���Z�b�g���ĉ�]�ĊJ
                stopCount = 0;
                for (int i = 0; i < isStopped.Length; i++)
                {
                    isStopped[i] = false;
                }
            }
        }

        // �e���[������
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
