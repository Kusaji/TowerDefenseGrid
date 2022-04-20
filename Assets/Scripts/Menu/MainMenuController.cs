using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Simply Close the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void OpenUIObject(GameObject uiToOpen)
    {
        uiToOpen.SetActive(true);
    }

    public void CloseUIObject(GameObject uiToClose)
    {
        uiToClose.SetActive(false);
    }
}
