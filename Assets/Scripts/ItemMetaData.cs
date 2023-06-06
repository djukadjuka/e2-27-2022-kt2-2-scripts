using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMetaData : MonoBehaviour
{
    [Header("Metadata")]
    [SerializeField] public string ItemName;
    [SerializeField] public bool CanBePutInInventory;

    [HideInInspector] public bool IsHighlighted;

    [Header("Rendering")]
    [SerializeField] public List<Renderer> Renderers;
    [HideInInspector] public List<Material> Materials = new List<Material>();
    [SerializeField] public Color HighlightColor;

    // Start is called before the first frame update
    void Start()
    {
        IsHighlighted = false;
        foreach (Renderer renderer in Renderers)
        {
            Materials.Add(renderer.material);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void InteractWith(GameObject interactor)
    {
        Debug.Log($"INTERACTED WITH! {interactor.name}");
    }

    public void Highlight()
    {
        this.IsHighlighted = true;

        foreach (Material material in Materials)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", HighlightColor);
        }
    }
    
    public void UnHighlight()
    {
        this.IsHighlighted = false;

        foreach(Material material in Materials)
        {
            material.DisableKeyword("_EMISSION");
        }
    }
}
