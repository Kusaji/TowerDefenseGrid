using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{
    public static int playerMoney;

    public int startingMoney;
    public int enemiesKilled;

    public int currentHealth;
    public int maxHealth;

    private Text moneyText;
    private Text healthText;
    public LevelController levelController;

    [Header("Debug")]
    public float timeScale;


    // Start is called before the first frame update
    void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        healthText = GameObject.Find("PlayerHealthText").GetComponent<Text>();
        levelController = GameObject.Find("UI").GetComponent<LevelController>();

        playerMoney = startingMoney;
        moneyText.text = $"Credits: {playerMoney}";

        currentHealth = maxHealth;
        healthText.text = $"Health: {currentHealth} | {maxHealth}";

        Time.timeScale = timeScale;

        StartCoroutine(UpdateUIText());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        healthText.text = $"Health: {currentHealth} | {maxHealth}";

        if (currentHealth <= 0)
        {
            levelController.FailedLevel();
        }
    }

    IEnumerator UpdateUIText()
    {
        while (gameObject)
        {
            moneyText.text = $"Credits: {playerMoney}";
            yield return new WaitForSeconds(0.1f);
        }
    }
}
