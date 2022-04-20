using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{
    public static int playerMoney;
    public int startingMoney;
    public int enemiesKilled;

    private Text moneyText;


    // Start is called before the first frame update
    void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        playerMoney = startingMoney;
        moneyText.text = $"Credits: {playerMoney}";

        StartCoroutine(UpdateUIText());
    }

    // Update is called once per frame
    void Update()
    {

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
