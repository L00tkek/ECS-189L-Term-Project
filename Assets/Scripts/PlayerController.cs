using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Sprite[] spriteArray;
    [SerializeField] float walkSpeed;
    [SerializeField] GameObject taskManager;
    TaskManager managerScript;
    [SerializeField] GameObject vignetteController;
    [SerializeField] GameObject minigame;
    float inputHorizontal;
    float inputVertical;

    int spoons;
    float timer;
    const float MAX_TIMER = 0.5f;
    [SerializeField] TextMeshProUGUI spoonsText;
    bool isMoving = false;
    // 0: forward (down, towards camera)
    // 1: right
    // 2: backward (up, away from camera)
    // 3: left
    int directionFacing = 0;
    int anim = 0;

    float fatigue;
    [SerializeField] GameObject fatigueOverlay;

    [SerializeField] TextMeshProUGUI lossText;
    public bool allowMovement;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        managerScript = taskManager.GetComponent<TaskManager>();

        Restart();
    }

    void Restart()
    {
        spoons = 100;
        timer = 0f;
        fatigue = 1f;
        UpdateText();

        GameObject[] allTasks = GameObject.FindGameObjectsWithTag("Task");
        Debug.Log(allTasks.Length);

        foreach (GameObject toDestroy in allTasks)
        {
            Destroy(toDestroy);
            managerScript.decrementTasks();
        }

        managerScript.SpawnTasks();

        // reset the player's position to its initial position
        gameObject.transform.position = new Vector3(8f, -5f, 0f);
        allowMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        Animate();

        timer += Time.deltaTime;
        if (timer > MAX_TIMER)
        {
            timer = 0f;
            spoons--;
            if (spoons < -100) {
                spoons = -100;
            }
            UpdateText();
        }

        fatigue = spoons >= 0 ? 1 : Mathf.Sqrt((spoons + 100.0f) / 200.0f);
        
        Color overlayColor = Color.HSVToRGB(0, 1f, fatigue);
        overlayColor.a = 1 - fatigue;
        fatigueOverlay.GetComponent<SpriteRenderer>().color = overlayColor;

        vignetteController.GetComponent<VignetteController>().setIntensity(spoons == -100 ? 0.0f : (1.0f - fatigue) * 0.5f);

        if (Input.GetKeyDown(KeyCode.R) && fatigue <= 0)
        {
            Restart();
        }
    }

    void Animate()
    {
        isMoving = inputHorizontal != 0.0f || inputVertical != 0.0f;
        if (inputHorizontal < 0.0f)
        {
            directionFacing = 3;
        }
        if (inputHorizontal > 0.0f)
        {
            directionFacing = 1;
        }
        if (inputVertical < 0.0f)
        {
            directionFacing = 0;
        }
        if (inputVertical > 0.0f)
        {
            directionFacing = 2;
        }

        anim += 1;
        anim %= 8;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (isMoving && anim / 4 % 2 == 0)
        {
            spriteRenderer.sprite = spriteArray[directionFacing * 2 + 1];
        }
        else
        {
            spriteRenderer.sprite = spriteArray[directionFacing * 2];
        }
    }

    // physics-related movement goes here
    void FixedUpdate()
    {
        if (allowMovement)
        {
            rb.velocity = new Vector2(inputHorizontal, inputVertical).normalized * walkSpeed * fatigue;
        }
    }

    public void decrementTask()
    {
        managerScript.decrementTasks();
    }

    public void spawnTask()
    {
        managerScript.SpawnTask();
    }

    void UpdateText()
    {
        lossText.text = "";

        if (spoons < 0)
        {
            spoonsText.SetText("Knives: {0}", spoons * -1);

            if (spoons <= -100)
            {
				minigame.GetComponent<MinigameController>().cancelPressed();
				allowMovement = false;
                lossText.text = "You lose. But there is no escape. Press R to restart.";
            }
        }
        else
        {
            spoonsText.SetText("Spoons: {0}", spoons);
        }
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Task"))
        {
            Destroy(collision.gameObject);
            rb.velocity = new Vector2(0, 0);
            //minigame.GetComponent<MinigameController>().startGame((int)(fatigue * -6.0f + 10.0f));
            minigame.GetComponent<MinigameController>().startGame((int)((-1 * spoons + 100) / 28.75f) + 4);
        }
        else if (collision.gameObject.CompareTag("Bed"))
        {
            spoons = Mathf.Min(100, spoons + 100);
            UpdateText();
            managerScript.SpawnTasks();
        }
    }
}
