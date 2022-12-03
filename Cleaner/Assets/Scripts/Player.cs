using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public PlayerInputs inputAction;

    Vector2 move;
    Vector2 rotate;
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 20.0f;

    private int trashCount = 0;
    public int trashLimit = 10;

    public TMP_Text scoreText;

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

        inputAction.Player.Rotate.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        inputAction.Player.Rotate.canceled += cntxt => rotate = Vector2.zero;

        inputAction.Player.Compact.performed += cntxt => compact();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * -move.y * Time.deltaTime * moveSpeed, Space.Self);
        //transform.Translate(Vector3.right * move.x * Time.deltaTime * speed, Space.Self);
        transform.Rotate(Vector3.up * rotate.x * Time.deltaTime * rotateSpeed);

        scoreText.text = trashCount + "/" + trashLimit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(trashCount <= trashLimit - 1)
        {
            if (collision.collider.tag == "Trash")
            {
                Destroy(collision.gameObject);
                trashCount++;
            }
        }
    }

    void compact()
    {
        if(trashCount >= trashLimit)
        {
            trashCount = 0;
        }
    }
}
