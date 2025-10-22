using UnityEngine;
using System;

public class console : MonoBehaviour
{
    Transform _tr;
    [Header("試行回数"), SerializeField] int times;

    // Start is called before the first frame update
    void Start()
    {
        _tr = transform;
        times = 1000;

        //Debug.Log("a");
        //Debug.LogWarning("e?");
        //Debug.LogError("ha?");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < times; i++)
            {
                //_tr.position = _tr.position;
                transform.position = transform.position;
            }
        }
        else
        {
            for (int i = 0; i < times; i++)
            {
                _tr.position = _tr.position;
            }
        }



    }
}
