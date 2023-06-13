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

    public void startGame()
    {
        this.displayText.SetText("");
        displayTime = 0.0f;
        player.GetComponent<PlayerController>().allowMovement = false;
        gameObject.SetActive(true);
    }

    void endGame()
    {
        player.GetComponent<PlayerController>().allowMovement = true;
        gameObject.SetActive(false);
    }

    void displayNum(int num)
    {
        this.displayText.SetText("{}", num);
        this.displayTime = this.displayCooldown;
    }

    public void buttonPressed(int buttonNum)
    {
        displayNum(buttonNum);
        if (buttonNum == 0)
        {
            endGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.displayTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        displayTime -= Time.deltaTime;
        if (displayTime <= 0.0f)
        {
            this.displayText.SetText("");
            displayTime = 0.0f;
        }
    }
}
