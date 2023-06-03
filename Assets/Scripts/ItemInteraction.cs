using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] public float interactDistance = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.CompareTag("CanInteractWith"))
            {
                // If player can put item in inventory - put it in inventory
                // Otherwise run InteractWith method from item
                if (hit.collider.CompareTag("CanPutInInventory"))
                {

                }
                else
                {

                }
                Debug.Log($"Got item: {hit.collider.gameObject.name}");
            }
        }
    }
    public void DisplayItemText()
    {

    }

}
