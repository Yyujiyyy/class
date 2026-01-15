using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    Renderer r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        r.enabled = false;
    }
}