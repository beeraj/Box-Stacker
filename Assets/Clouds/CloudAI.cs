using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float CloudSpeed = Random.Range(GameController.MinCloudSpeed, GameController.MaxCloudSpeed);
        transform.Translate(Vector3.right * CloudSpeed * Time.deltaTime);
    }
}
