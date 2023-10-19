using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusScript : MonoBehaviour
{
    private Manager _manager;
    private AudioSource _audioSource;
    public AudioClip audioClipOnTrigger;

    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Manager>();
        // Otherwise, no sound play because this gameobject is destroyed after the collision.
        _audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();

        if (Random.Range(0f, 1f) < 0.25f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-1.0f, 1.0f), transform.position.z);
        }
    }

    // Update is called once per frame
    void Update() {}

    void OnTriggerEnter2D(Collider2D col)
    {
        _manager.AddPoint(5);
        _audioSource.PlayOneShot(audioClipOnTrigger);
        Destroy(gameObject);
    }
}
