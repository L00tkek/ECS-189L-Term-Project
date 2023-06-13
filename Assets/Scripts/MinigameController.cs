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
    private bool displayingNumbers;

    public void startGame()
    {
        this.displayText.SetText("");
        this.displayTime = 0.0f;
        this.player.GetComponent<PlayerController>().allowMovement = false;
        gameObject.SetActive(true);
        this.numbers = new int[] {1,2,3,4,5};
        this.index = 0;
        this.displayingNumbers = true;
    }

    void endGame()
    {
        this.player.GetComponent<PlayerController>().allowMovement = true;
        gameObject.SetActive(false);
    }

    void displayNextNum()
    {
        displayNum(numbers[index]);
        this.index++;
        if (this.index >= 5)
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

        if (this.index >= 5) {
            endGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
}
