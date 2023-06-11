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
        if (isMoving) {
            if (anim / 4 % 2 == 0) {
                spriteRenderer.sprite = spriteArray[1];
            } else {
                spriteRenderer.sprite = spriteArray[0];
            }
        } else {
            spriteRenderer.sprite = spriteArray[0];
        }
    }

    // physics-related movement goes here
    void FixedUpdate()
    {
        rb.velocity = new Vector2(inputHorizontal, inputVertical).normalized * walkSpeed;
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
