using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Patikasu : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score = 0;
    private bool isPatinkoActive = true;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        Updatescore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPatinkoActive = !isPatinkoActive;     // true�Ȃ�false�ɁAfalse�Ȃ�true�ɐ؂�ւ�
        }

        if(isPatinkoActive)     // �t���O��true�Ȃ�Patinko���s
        {
            Patinko();
        }

        Updatescore();
    }

    void Updatescore()
    {
        scoreText.text = score.ToString();
    }

    void Patinko()
    {
        score += 1;
        if (score == 9)
            score = 0;
    }
}
