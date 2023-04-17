using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Component assignments.
    public List<GameObject> targets;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;

    // Variables.
    public bool isGameActive;
    public bool isGamePaused;
    private int score;
    private int lives;
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P) && !isGamePaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyUp(KeyCode.P) && isGamePaused)
        {
            UnPauseGame();
        }
    }

    // It updates the score.
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // It updates lives. If live is zero, it ends the game.
    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }

    // It spawns random targets.
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // It ends the game and actives game over screen.
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    // It restarts the game.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // It starts the game with taking and adjusting diffuclty level.
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateLives(3);
        score = 0;
        titleScreen.gameObject.SetActive(false);
    }

    // It pauses the game and actives the pause screen.
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.gameObject.SetActive(true);
        isGamePaused = true;
    }

    // It unpauses the game and deactives the pause screen.
    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        pauseScreen.gameObject.SetActive(false);
        isGamePaused = false;
    }
}
