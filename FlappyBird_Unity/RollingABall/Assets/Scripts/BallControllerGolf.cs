using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class BallControllerGolf : MonoBehaviour
{
    public float baseSpeed = 2000f;
    public float maxTimeSpaceKeyDown = 3f;

    public Camera camera;

    public Text speedText;
    public Text boostText;

    private Rigidbody _rb;

    private bool _spaceDown = false;
    private bool _spaceUpEvent = false;
    private float _timeSpaceKeyDown = 0f;


    private float _currentBoostValue;
    private Vector3 _currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spaceDown = true;
        }

        if (_spaceDown)
        {
            _timeSpaceKeyDown += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) || _timeSpaceKeyDown > maxTimeSpaceKeyDown)
        {
            _spaceUpEvent = true;
            _spaceDown = false;
            Debug.Log(_timeSpaceKeyDown);


            Vector3 pos = camera.transform.position;
            Vector3 dir = (this.transform.position - camera.transform.position).normalized;

            _currentSpeed = new Vector3(dir.x, 0f, dir.z) * baseSpeed * _timeSpaceKeyDown;


            Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);
            Debug.Log(dir);

            /*Let�s say camera is your point A, and this is your point B.

            Vector3 AB = B - A.Destination - Origin.

            This is a direction and a distance. To have only the direction(and a distance of 1), you have to normalize it.

            AB = AB.normalized OR AB.Normalize()*/
        }

        UpdateSpeed();
        UpdateTimeSpaceDown();


        /*camera.transform.position = Vector3.MoveTowards(camera.transform.position, this.transform.position, -(7f - Vector3.Distance(camera.transform.position, this.transform.position)));*/
    }

    void FixedUpdate()
    {
        if (_spaceUpEvent)
        {
            _rb.AddForce(_currentSpeed, ForceMode.Acceleration);
            _spaceUpEvent = false;
            _timeSpaceKeyDown = 0f;
            _currentSpeed = Vector3.zero;
        }
    }

    void UpdateSpeed()
    {
        speedText.text = Mathf.RoundToInt(Mathf.Abs(_rb.velocity.z)).ToString();
    }

    void UpdateTimeSpaceDown()
    {
        boostText.text = Mathf.RoundToInt(_timeSpaceKeyDown).ToString();
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
            camera.transform.RotateAround(this.transform.position,
                                            camera.transform.up,
                                            -Input.GetAxis("Mouse X") * 5f);

            camera.transform.RotateAround(this.transform.position,
                                            camera.transform.right,
                                            -Input.GetAxis("Mouse Y") * 5f);

        }

        Vector3 delta = camera.transform.position - transform.position;
        /*delta.y = 0; // Keep same Y level*/
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, transform.position + delta.normalized * 7, 0.5f);
        camera.transform.position = transform.position + delta.normalized * 7;
    }
}
