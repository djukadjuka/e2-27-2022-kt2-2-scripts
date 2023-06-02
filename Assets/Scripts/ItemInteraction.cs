using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] public float interactDistance = 3.0f;
    [SerializeField] public Transform interactionStart;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.origin = interactionStart.position;
        ray.direction += interactionStart.position;
        Debug.DrawLine(ray.origin, ray.direction.normalized * interactDistance, Color.red);

        if(Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                Debug.Log($"Got item: {hit.collider.gameObject.name}");
            }
        }
    }
}
