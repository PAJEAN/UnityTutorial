using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float maxTimeSpaceKeyDown = 3f;
    public GameObject startPoint;
    public Text nbHitText;

    private Rigidbody _rb;
    private Vector3 _applyForce;
    private bool _spaceDown = false;
    private bool _spaceUp = false;
    private float _spaceKeyDownDuration = 0f;
    private float _currentBoostValue;
    private int _nbHit = 0;

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
            _applyForce = dir * baseSpeed * _spaceKeyDownDuration;
            _applyForce.y = 0;
            _nbHit += 1;
            UpdateUi();
        }
    }

    void FixedUpdate()
    {
        if (_spaceUp)
        {
            _rb.AddForce(_applyForce, ForceMode.Impulse);
            _spaceUp = false;
            _spaceKeyDownDuration = 0f;
            _applyForce = Vector3.zero;
        }
    }

    void UpdateUi()
    {
        nbHitText.text = _nbHit.ToString();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Borders"))
        {
            _rb.velocity = Vector3.zero;
            transform.position = startPoint.transform.position;
        }

        if (col.CompareTag("Hole"))
        {
            _rb.velocity = Vector3.zero;
            Debug.Log("WIN !");
        }
    }
}
