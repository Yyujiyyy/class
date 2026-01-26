using UnityEngine;

/// <summary>
/// 連番アニメーション
/// </summary>
public class AnimationScript : MonoBehaviour
{
    [SerializeField] Sprite[] frames;
    private float frameRates = 0.35f;

    SpriteRenderer sr;
    int index;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 連番アニメーション
        timer += Time.deltaTime;

        if (timer >= frameRates)
        {
            timer = 0f;
            index = (index + 1) % frames.Length;
            sr.sprite = frames[index];
        }
    }
}
