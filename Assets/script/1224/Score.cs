using System.Collections;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    Enemy _Enemy;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI _highS;

    int prevScore;
    Coroutine scaleCoroutine;

    void Start()
    {
        _Enemy = FindObjectOfType<Enemy>();

        prevScore = _Enemy._score;
        text.text = prevScore.ToString();
        _highS.text = _Enemy._highScore.ToString();
    }

    void Update()
    {
        // 変わった瞬間だけ
        if (_Enemy._score != prevScore)
        {
            prevScore = _Enemy._score;
            text.text = prevScore.ToString();

            // 連続加点対策
            if (scaleCoroutine != null)
                StopCoroutine(scaleCoroutine);

            scaleCoroutine = StartCoroutine(ScaleAnim());
        }

        _highS.text = _Enemy._highScore.ToString();
    }
    /// <summary>
    /// 拡縮アニメ
    /// </summary>
    /// <returns></returns>
    IEnumerator ScaleAnim()
    {
        Vector3 baseScale = Vector3.one;
        Vector3 bigScale = baseScale * 1.3f;

        text.transform.localScale = bigScale;

        yield return new WaitForSeconds(0.15f);

        text.transform.localScale = baseScale;
    }
}