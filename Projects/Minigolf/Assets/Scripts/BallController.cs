using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float maxTimeSpaceKeyDown = 3f;

    private Rigidbody _rb;
    private bool _spaceDown = false;
    private bool _spaceUp = false;
    private float _spaceKeyDownDuration = 0f;
    private float _currentBoostValue;
    private Vector3 _applyForce;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spaceDown = true;
        }

        if (_spaceDown)
        {
            _spaceKeyDownDuration += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) || _spaceKeyDownDuration > maxTimeSpaceKeyDown)
        {
            _spaceUp = true;
            _spaceDown = false;
            Vector3 dir = (this.transform.position - Camera.main.transform.position).normalized;
            _applyForce = new Vector3(dir.x, 0f, dir.z) * baseSpeed * _spaceKeyDownDuration;
        }
    }

    void FixedUpdate()
    {
        if (_spaceUp)
        {
            _rb.AddForce(_applyForce, ForceMode.VelocityChange);
            _spaceUp = false;
            _spaceKeyDownDuration = 0f;
            _applyForce = Vector3.zero;
        }
    }
}
