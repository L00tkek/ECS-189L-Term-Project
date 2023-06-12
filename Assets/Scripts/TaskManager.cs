using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField] protected GameObject taskPrefab;
    [SerializeField] float cooldown;
    [SerializeField] float spawnWidth;
    [SerializeField] float spawnHeight;
    [SerializeField] TextMeshProUGUI test;
    [SerializeField] int dailyTasks = 15;

    private int numTasks;
    private float timeElapsed;
    private System.Random rand;

    // this code needs to be in Awake because
    // it is relied on by the PlayerController init code,
    // which is called in Start
    void Awake()
    {
        this.rand = new System.Random();
        this.timeElapsed = this.cooldown;
        this.numTasks = 0;
        updateText();
    }

    public void incrementTasks() {
        this.numTasks++;
        updateText();
    }

    public void decrementTasks() {
        this.numTasks--;
        updateText();
    }

    void updateText() {
        test.SetText("Tasks: {0}", this.numTasks);
    }

    public void SpawnTask()
    {
        GameObject task = (GameObject)Object.Instantiate(taskPrefab);
        task.transform.position = new Vector3((float)(this.rand.NextDouble() * this.spawnWidth - this.spawnWidth / 2.0), 
            (float)(this.rand.NextDouble() * this.spawnHeight - this.spawnHeight / 2.0), 0.0f);
        incrementTasks();
    }

    public void SpawnTasks()
    {
        for(int i = 0; i < dailyTasks; i++)
        {
            SpawnTask();
        }
    }

    void Update()
    {
        this.timeElapsed -= Time.deltaTime;
        while (this.timeElapsed < 0.0f)
        {
            SpawnTask();
            this.timeElapsed += this.cooldown;
        }
    }
}
