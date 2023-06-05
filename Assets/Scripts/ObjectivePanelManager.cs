using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectivePanelManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI ObjectiveText;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        this.ObjectiveText.SetText(text);
    }

    public void FadePanelIn()
    {
        animator.Play("objectivePanel_fadeIn");
    }

    public void FadePanelOut()
    {
        animator.Play("objectivePanel_fadeOut");
    }

    public void FadeTextIn()
    {
        animator.Play("objectiveText_fadeIn");
    }

    public void FadeTextOut()
    {
        animator.Play("objectiveText_fadeOut");
    }
}
