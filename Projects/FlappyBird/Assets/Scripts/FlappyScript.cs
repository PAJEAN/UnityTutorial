using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyScript : MonoBehaviour
{

    public float flyMove = 5f;
    public Rigidbody2D myRigidBody;
    private Manager gameManager;

    private bool birdAlive = true;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Alternative.
        // myRigidBody = GetComponent<Rigidbody2D>();
        
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Manager>();
        _audioSource = GetComponent<AudioSource>();


        Debug.Log(Physics.gravity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdAlive) 
        {
            myRigidBody.velocity = Vector2.up * flyMove;
        }
    }

    /*private void FixedUpdate()
    {
        myRigidBody.AddForce(new Vector2(0, 9.81f));
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdAlive)
        {
            _audioSource.Play();
        }
        birdAlive = false;
        gameManager.GameOver();
    }
}
