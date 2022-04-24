using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopActivateObject : MonoBehaviour
{
    public bool towerIsPurchasable;
    // Start is called before the first frame update
    void Start()
    {
        if (!towerIsPurchasable)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
