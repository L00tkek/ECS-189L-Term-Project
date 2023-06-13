using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class MinigameController : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI displayText;
    [SerializeField] GameObject player;
    [SerializeField] float displayCooldown;
    private float displayTime;
    private int[] numbers;
    private int index;
    private int sequenceLength;
    private bool displayingNumbers;
    private System.Random rand;

    void shuffle()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if ((int)(this.rand.NextDouble() * 2) == 0)
                {
                    int tmp = this.numbers[i];
                    this.numbers[i] = this.numbers[j];
                    this.numbers[j] = tmp;
                }
            }
        }
    }

    public void startGame(int sequenceLength)
    {
        this.sequenceLength = sequenceLength;
        this.displayText.SetText("");
        this.displayTime = 0.0f;
        this.player.GetComponent<PlayerController>().allowMovement = false;
        gameObject.SetActive(true);
        this.numbers = new int[] {0,1,2,3,4,5,6,7,8,9};
        shuffle();
        this.index = 0;
        this.displayingNumbers = true;
    }

    void endGame()
    {
        this.player.GetComponent<PlayerController>().decrementTask();
        this.player.GetComponent<PlayerController>().allowMovement = true;
        gameObject.SetActive(false);
    }

    void displayNextNum()
    {
        displayNum(numbers[index]);
        this.index++;
        if (this.index >= sequenceLength)
        {
            this.displayingNumbers = false;
            this.index = 0;
        }
    }

    void displayNum(int num)
    {
        this.displayText.SetText("{}", num);
        this.displayTime = this.displayCooldown;
    }

    public void buttonPressed(int buttonNum)
    {
        if (this.displayingNumbers)
        {
            return;
        }

        if (buttonNum == this.numbers[index])
        {
            this.displayText.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            this.index++;
        }
        else
        {
            this.displayText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            this.index = 0;
            this.displayingNumbers = true;
        }

        displayNum(buttonNum);

        if (this.index >= sequenceLength)
        {
			this.player.GetComponent<PlayerController>().spoons -= 5;
            endGame();
        }
    }

    public void cancelPressed()
    {
        this.player.GetComponent<PlayerController>().spawnTask();
        endGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.rand = new System.Random();
        this.displayTime = 0.0f;
        this.index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.displayTime -= Time.deltaTime;
        if (this.displayTime <= 0.0f)
        {
            this.displayText.SetText("");
            this.displayTime = 0.0f;
            if (this.displayingNumbers)
            {
                this.displayText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                displayNextNum();
            }
        }
    }

    void Awake()
    {
        this.rand = new System.Random();
    }
}
