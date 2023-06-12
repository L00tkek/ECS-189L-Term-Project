using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField] protected GameObject taskPrefab;
    [SerializeField] float cooldown;
    [SerializeField] float spawnRadius;
    [SerializeField] TextMeshProUGUI test;
    [SerializeField] int dailyTasks = 15;

    private int numTasks;
    private float timeElapsed;
    private System.Random rand;

    void Start()
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
        task.transform.position += new Vector3((float)(this.rand.NextDouble() * this.spawnRadius * 2.0 - this.spawnRadius), 
            (float)(this.rand.NextDouble() * this.spawnRadius * 2.0 - this.spawnRadius), 0.0f);
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
