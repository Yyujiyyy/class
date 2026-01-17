using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    private InputAction moveAction;

    private float speed = 4;

    //[SerializeField] private GameObject _pause;

    void OnEnable()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        
        Vector3 move = new Vector3(moveValue.x, moveValue.y, 0);
        transform.position += move * speed * Time.deltaTime;
    }
}
