using System;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] Transform _player;
    Camera _camera;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _enemy;

    public List<GameObject> Bullets = new List<GameObject>();

    MeshRenderer mr;

    [NonSerialized] public float halfw;
    [NonSerialized] public float halfh;

    float timer;

    float speed = 10f;
    Vector3 min, max;

    void Start()
    {
        _camera = Camera.main;
        mr = _player.GetComponent<MeshRenderer>();

        //幅と高さを取得している
        halfw = mr.bounds.extents.x;
        halfh = mr.bounds.extents.y;

        for (int i = 0; i < 20; i++)
        {
            GameObject obj = Instantiate(_bullet, _player.position, Quaternion.identity);
            obj.SetActive(false);
            Bullets.Add(obj);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            _player.position += _player.up * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _player.position -= _player.up * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _player.position += _player.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _player.position -= _player.right * Time.deltaTime * speed;
        }

        Clamp();

        if (Input.GetMouseButton(0) && 0.5f <= timer)
        {
            foreach (GameObject bullet in Bullets)
            {
                if (!bullet.activeInHierarchy)
                {
                    bullet.SetActive(true);
                    bullet.transform.position = _player.position;

                    timer = 0;
                    //全弾発生するのを防ぐ

                    //Debug.Log("tama");
                    break;
                }
            }
        }

        foreach (GameObject bullet in Bullets)
        {   //Activeのもののみ
            if (bullet.activeInHierarchy)
            {
                bullet.transform.position += Vector3.right * Time.deltaTime * speed;

                if (max.x <= bullet.transform.position.x)
                {
                    bullet.SetActive(false);
                }
            }
        }
        //bullet不足の場合
    }
    /// <summary>
    /// プレイヤーの動く範囲を画面内に制限
    /// </summary>
    void Clamp()
    {
        min = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
        max = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));
        //nearClipPlaneとは、カメラが「どこから」描画を開始するかを決める距離
        //ViewportToWorldPointは０１で画面内の位置を表せる

        Vector3 pos = _player.position;
        pos.x = Mathf.Clamp(_player.position.x, min.x + halfw, max.x - halfw);
        pos.y = Mathf.Clamp(_player.position.y, min.y + halfh, max.y - halfh);
        //Debug.DrawLine(min, max);
        //Debug.Log(min);
        //Debug.Log(max);
        _player.position = pos;
    }
}
