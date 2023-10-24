using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float boostSpeed = 15f;
    public float maxBoostValue = 3f;

    public Text speedText;
    public Text boostText;

    private float _xInput;
    private float _zInput;
    private Rigidbody _rb;

    private bool _spaceDown = false;
    private float _currentBoostValue;
    private float _currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentBoostValue = maxBoostValue;
        _currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _xInput = Input.GetAxis("Horizontal");
        _zInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && _currentBoostValue > 0)
        {
            _spaceDown = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) || _currentBoostValue <= 0)
        {
            _spaceDown = false;
        }

        if (_spaceDown)
        {
            _currentSpeed = boostSpeed;
            if (_currentBoostValue >= 0f)
            {
                _currentBoostValue -= Time.deltaTime;
            }
        }
        else
        {
            _currentSpeed = baseSpeed;
        }
    }

    void FixedUpdate()
    {
        _rb.AddForce(new Vector3(_xInput, 0f, _zInput) * _currentSpeed);

        speedText.text = Mathf.RoundToInt(Mathf.Abs(_rb.velocity.z)).ToString();
        boostText.text = Mathf.RoundToInt(_currentBoostValue).ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Debug.Log("Oups");
        }
    }
}
