using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject pipes;

    float timeBetween = 2f;
    float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        NewPipe();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeBetween)
        {
            NewPipe();
            currentTime = 0;
        }
    }

    void NewPipe()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + Random.Range(-0.6f, 1.6f), transform.position.z);
        Instantiate(pipes, newPosition, transform.rotation);
    }
}
