using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    //player movement
    public PlayerInputs inputAction;
    Vector2 move;
    Vector2 rotate;
    public float speed = 5.0f;

    Rigidbody rb;

    private void OnEnable()
    {
        inputAction.Player.Enable();
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        inputAction = new PlayerInputs();

        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * move.y * Time.deltaTime * speed, Space.Self);
        transform.Translate(Vector3.right * move.x * Time.deltaTime * speed, Space.Self);
    }
}
