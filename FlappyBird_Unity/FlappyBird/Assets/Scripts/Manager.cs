using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public float score = 0f;

    public Text scoreText;
    public GameObject gameOverPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(int point = 1)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
