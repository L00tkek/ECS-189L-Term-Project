using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Sprite[] spriteArray;
    [SerializeField] float walkSpeed;
    float inputHorizontal;
    float inputVertical;

    int spoons;
    float timer;
    const float MAX_TIMER = 0.5f;
    [SerializeField] TextMeshProUGUI test;
    bool isMoving = false;
    // 0: forward (down, towards camera)
    // 1: right
    // 2: backward (up, away from camera)
    // 3: left
    int directionFacing = 0;
    int anim = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spoons = 100;
        timer = 0f;
        test.SetText("Spoons: {0}", spoons);
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        isMoving = inputHorizontal != 0.0f || inputVertical != 0.0f;
        if (inputHorizontal < 0.0f) {
            directionFacing = 3;
        }
        if (inputHorizontal > 0.0f) {
            directionFacing = 1;
        }
        if (inputVertical < 0.0f) {
            directionFacing = 0;
        }
        if (inputVertical > 0.0f) {
            directionFacing = 2;
        }

        timer += Time.deltaTime;
        if (timer > MAX_TIMER)
        {
            timer = 0f;
            spoons--;
            test.SetText("Spoons: {0}", spoons);
        }

        anim += 1;
        anim %= 8;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (isMoving && anim / 4 % 2 == 0) {
            switch (directionFacing) {
                case 0:
                    spriteRenderer.sprite = spriteArray[1];
                    break;
                case 1:
                    spriteRenderer.sprite = spriteArray[3];
                    break;
                case 2:
                    spriteRenderer.sprite = spriteArray[5];
                    break;
                case 3:
                    spriteRenderer.sprite = spriteArray[7];
                    break;
            }
        } else {
            switch (directionFacing) {
                case 0:
                    spriteRenderer.sprite = spriteArray[0];
                    break;
                case 1:
                    spriteRenderer.sprite = spriteArray[2];
                    break;
                case 2:
                    spriteRenderer.sprite = spriteArray[4];
                    break;
                case 3:
                    spriteRenderer.sprite = spriteArray[6];
                    break;
            }
        }
    }

    // physics-related movement goes here
    void FixedUpdate()
    {
        rb.velocity = new Vector2(inputHorizontal, inputVertical).normalized * walkSpeed;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bbbbb");

        if (collision.gameObject.tag == "Task")
        {
            Destroy(collision.gameObject);
        }
    }
}
