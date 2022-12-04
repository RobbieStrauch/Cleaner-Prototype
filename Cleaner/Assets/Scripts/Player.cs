using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;

    public PlayerInputs inputAction;

    Vector2 move;
    Vector2 rotate;
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 20.0f;

    private int compactCount = 0;
    private int trashCount = 0;
    public int trashLimit = 10;

    public TMP_Text scoreText;
    public TMP_Text countText;
    public GameObject compactText;

    private void OnEnable()
    {
        inputAction.Player.Enable();
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        inputAction = new PlayerInputs();

        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        inputAction.Player.Rotate.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        inputAction.Player.Rotate.canceled += cntxt => rotate = Vector2.zero;

        inputAction.Player.Compact.performed += cntxt => Compact();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * -move.y * Time.deltaTime * moveSpeed, Space.Self);
        transform.Rotate(Vector3.up * rotate.x * Time.deltaTime * rotateSpeed);

        scoreText.text = compactCount.ToString();
        countText.text = (trashCount + "/" + trashLimit).ToString();

        if (trashCount == trashLimit)
        {
            compactText.SetActive(true);
        }
        else
        {
            compactText.SetActive(false);
        }
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

    void Compact()
    {
        if(trashCount >= trashLimit)
        {
            compactCount++;
            trashCount = 0;
        }
    }

    public int GetScore()
    {
        return compactCount;
    }
}
