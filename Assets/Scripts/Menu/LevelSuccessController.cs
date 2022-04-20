using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSuccessController : MonoBehaviour
{
    public GameObject levelSuccessObject;
    public Text enemiesKilledText;

    public Economy playerEconomy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelBeat()
    {
        levelSuccessObject.SetActive(true);
        enemiesKilledText.text = $"Enemies Killed: {playerEconomy.enemiesKilled}";
    }
}
