using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject levelSuccessObject;
    public GameObject levelFailureObject;
    public Text enemiesKilledText;

    public Economy playerEconomy;

    public void LevelBeat()
    {
        levelSuccessObject.SetActive(true);
        enemiesKilledText.text = $"Enemies Killed: {playerEconomy.enemiesKilled}";
    }

    public void FailedLevel()
    {
        levelFailureObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
