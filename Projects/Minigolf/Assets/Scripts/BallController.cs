using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float maxTimeSpaceKeyDown = 3f;
    
    private Text _nbHitText;
    private Image _greenBar;
    private Rigidbody _rb;
    private Vector3 _applyForce;
    private GameObject _spawnPoint;
    private bool _spaceDown = false;
    private bool _spaceUp = false;
    private float _spaceKeyDownDuration = 0f;
    private int _nbHit = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        _nbHitText = GameObject.FindGameObjectWithTag("UI_nbHitText").GetComponent<Text>();
        _greenBar = GameObject.FindGameObjectWithTag("UI_boostBar").GetComponent<Image>();
        _greenBar.fillAmount = 0;
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

            _greenBar.fillAmount = _spaceKeyDownDuration / maxTimeSpaceKeyDown;
        }

        if (Input.GetKeyUp(KeyCode.Space) || _spaceKeyDownDuration > maxTimeSpaceKeyDown)
        {
            _spaceUp = true;
            _spaceDown = false;
            Vector3 dir = (this.transform.position - Camera.main.transform.position).normalized;
            _applyForce = dir * baseSpeed * _spaceKeyDownDuration;
            _applyForce.y = 0;
            _nbHit += 1;

            _greenBar.fillAmount = 0;

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
        _nbHitText.text = _nbHit.ToString();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Borders"))
        {
            StopBall();
            Vector3 _spawnPosition = _spawnPoint.transform.position;
            transform.position = new Vector3(_spawnPosition.x, _spawnPosition.y + 0.25f, _spawnPosition.z);
        }

        if (col.CompareTag("Hole"))
        {
            StopBall();
            Debug.Log("WIN !");
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }
    }

    private void StopBall()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
