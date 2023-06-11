using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float walkSpeed;
    float inputHorizontal;
    float inputVertical;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
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
