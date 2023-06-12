using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject taskPrefab;
    [SerializeField] float cooldown;
    [SerializeField] float spawnRadius;
    private float timeElapsed;
    private System.Random rand;

    void Start()
    {
        this.rand = new System.Random();
        this.timeElapsed = this.cooldown;
    }

    void Update()
    {
        this.timeElapsed -= Time.deltaTime;
        while (this.timeElapsed < 0.0f)
        {
            GameObject thing = (GameObject)Object.Instantiate(taskPrefab);
            thing.transform.position += new Vector3((float)(this.rand.NextDouble() * this.spawnRadius * 2.0 - this.spawnRadius), (float)(this.rand.NextDouble() * this.spawnRadius * 2.0 - this.spawnRadius), 0.0f);
            this.timeElapsed += this.cooldown;
        }
    }
}
