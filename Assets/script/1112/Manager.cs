using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] Transform _player;
    Camera _camera;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _enemy;

    List<GameObject> Bullets = new List<GameObject>();
    List<GameObject> Enemys = new List<GameObject>();

    float halfw;
    float halfh;

    float timer;

    float speed = 10f;

    void Start()
    {
        _camera = Camera.main;

        //幅と高さを取得している
        //halfw = 
        //halfh = 

        for(int i = 0; i < 20; i++)
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
        
        if (Input.GetMouseButton(0) && 1 <= timer)
        {
            foreach(GameObject bullet in Bullets)
            {
                if (!bullet.activeInHierarchy)
                {
                    bullet.SetActive(true);
                    bullet.transform.position = _player.position;

                    timer = 0;
                    //全弾発生するのを防ぐ
                    break;
                }
            }
            Debug.Log("tama");
        }

        foreach (GameObject bullet in Bullets)
        {
            if (bullet.activeInHierarchy)
            {
                bullet.transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }

        //bullet不足の場合
        //どこかでSetActive(false)にする処理
    }

    void Clamp()
    {
        Vector3 min = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
        Vector3 max = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));
        //nearClipPlaneとは、カメラが「どこから」描画を開始するかを決める距離
        //ViewportToWorldPointは０１で画面内の位置を表せる

        Vector3 pos = _player.position;
        pos.x = Mathf.Clamp(_player.position.x, min.x + halfw * 2, max.x - halfw * 2);
        pos.y = Mathf.Clamp(_player.position.y, min.y + halfh * 2, max.y - halfh * 2);
        //Debug.DrawLine(min, max);
        //Debug.Log(min);
        //Debug.Log(max);
        _player.position =  pos;
    }
}
