using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _enemy;

    List<GameObject> Enemys = new List<GameObject>();

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(_enemy, new Vector3(0, 0, 0), Quaternion.identity);
            obj.SetActive(false);
            Enemys.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(5 <= timer)
        {

        }
    }
}
