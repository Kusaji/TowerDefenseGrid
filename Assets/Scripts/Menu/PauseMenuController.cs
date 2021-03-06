using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenuObject;
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pauseMenuObject.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
            }
            else
            {
                pauseMenuObject.SetActive(false);
                Time.timeScale = 1.0f;
                isPaused = false;
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
