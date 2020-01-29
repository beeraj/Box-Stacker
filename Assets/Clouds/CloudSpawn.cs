using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour 
{

    // Array of Clouds
	public GameObject[] CloudPrefabs;

    // Stores the Current GameObect Position
    private Transform CurrentPosition;

    // Seconds Between Cloud Spawn
    public float MinSpawnTimer = 0.5f;
    public float MaxSpawnTimer = 0.9f;
    public float SecondsBetweenSpawn = 0.0f;

    // Seconds Between Last Cloud Spawn
    private float ElapsedTime = 0.0f;


    // Use this for initialization
    void Start ()
    { 
        // Get and Set Current GameObject/Spawner Position
        CurrentPosition = this.gameObject.transform;

        // Set New Random Spawn Timer Speed
        SecondsBetweenSpawn = Random.Range(0.5f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

        // Start Timer
        ElapsedTime += Time.deltaTime;

        // Check if Spawn Time Limit has been Reached
        if (ElapsedTime > SecondsBetweenSpawn)
        {
            // Reset Timer
            ElapsedTime = 0;

            // Set New Random Spawn Timer Speed
            SecondsBetweenSpawn = Random.Range(MinSpawnTimer, MaxSpawnTimer);

            // Select Random Cloud
            int RandomCloud = Random.Range(0, CloudPrefabs.Length);

            // Spawn Selected Cloud
            GameObject NewCloud = Instantiate(CloudPrefabs[RandomCloud], CurrentPosition.position, Quaternion.identity);

            // Move New Cloud Spawn Under Parent GameObject
            NewCloud.transform.parent = gameObject.transform;
        }

    }









}
