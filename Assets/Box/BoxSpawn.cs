using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawn : MonoBehaviour
{
    // Stores the Box Prefab
    public GameObject BoxPrefab;

	// Awake is used to initialise all GameObjects
    void Awake()
    {
		// Assign Current Box Spawn Script 
		GameController.instance.CurrentBoxSpawnScript = this;
    }

    // Method used to Spawn a New Box
    public void SpawnBox()
    {
        // Spawn New Box at Parent Position
        GameObject NewBox = Instantiate(BoxPrefab, transform.position, Quaternion.identity);

        // Move New Box under Parent
        NewBox.transform.parent = this.transform;

        // Increase Box Spawn Count
        GameController.instance.BoxCounter();
    }

}