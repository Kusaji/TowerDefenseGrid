using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectSelector : MonoBehaviour
{
    public GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectGrid();
        }
    }

    void SelectGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.gameObject.CompareTag("TowerGrid"))
            {
                //If we have a grid selected and it's shop is open, close it.
                if (selectedObject != null && selectedObject.CompareTag("TowerGrid"))
                {
                    selectedObject.GetComponent<TowerGrid>().CloseShopMenu();
                }

                //Then we grab our new tower grid and open its shop
                selectedObject = hit.transform.gameObject;
                selectedObject.GetComponent<TowerGrid>().OpenShopMenu();
            }

            if (!hit.transform.CompareTag("TowerGrid"))
            {
                //If we have a selected grid already and click on anything else, close the shop.
                if (selectedObject != null && selectedObject.CompareTag("TowerGrid"))
                {
                    //selectedObject.GetComponent<TowerGrid>().CloseShopMenu();

                    //clear selected grid
                    selectedObject = null;
                }
            }
        }
    }
}
