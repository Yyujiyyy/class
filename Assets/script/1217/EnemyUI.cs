using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] GameObject[] _ui = new GameObject[2];
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _ui.Length; i++)
        {
            _ui[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (index < 0) index = 0;
        if (1 < index) index = 1;


        if(this.gameObject.activeSelf)
        {
            _ui[index].SetActive(true);

            if (index == 0) index++;
            if(index == 1) index--;
        }
    }
}
