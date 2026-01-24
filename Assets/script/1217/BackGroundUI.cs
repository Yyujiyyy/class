using UnityEngine;
/// <summary>
///  背景画像のスクロール
/// </summary>
public class BackGroundUI : MonoBehaviour
{//２枚の画像を用意して、端まで行ったらもう片方の端にワープしてそれを繰り返す
    
    [SerializeField] RectTransform[] _backGroundUI = new RectTransform[2];

    // Update is called once per frame
    void Update()
    {
        //_backGroundUI[0].position -= Vector3.right;

        for (int i = 0; i < _backGroundUI.Length; i++)
        {
            _backGroundUI[i].anchoredPosition -= Vector2.right * Time.deltaTime;

            if (_backGroundUI[i].position.x <= -19)
            {
                Vector2 pos = _backGroundUI[i].anchoredPosition;
                pos.x = 19;
                _backGroundUI[i].anchoredPosition = pos;
            }
        }
    }
}
