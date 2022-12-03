using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;
    //player movement
    public PlayerInputs inputAction;
    Vector2 move;
    Vector2 rotate;
    public float speed = 5.0f;

    public int trashcount = 0;
    public int limit = 10;

    public TMP_Text scoreText;

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
        inputAction.Player.Compact.performed += cntxt => compact();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * move.y * Time.deltaTime * speed, Space.Self);
        transform.Translate(Vector3.right * move.x * Time.deltaTime * speed, Space.Self);

        scoreText.text = trashcount + "/" + limit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(trashcount <= limit - 1)
        {
            if (collision.collider.tag == "trash")
            {
                Destroy(collision.gameObject);
                trashcount++;
            }
        }
    }

    void compact()
    {
        if(trashcount >= limit)
        {
            trashcount = 0;
        }
    }
}
