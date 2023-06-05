using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectiveTriggerScript : MonoBehaviour
{
    public bool PlayerLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLeft = true;
        }
    }
}
