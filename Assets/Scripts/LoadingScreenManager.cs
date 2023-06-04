using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] public Animator animator;

    [HideInInspector] public List<string> quotes = new List<string>
    {
        "A suspicious mind is a healthy mind.",
        "Let the flames of hate NEVER die!",
        "All mortal life is folly that does not feed the spirit.",
        "Honour the craft of death.",
        "Withstand the winds of the Underworld."
    };
    [HideInInspector] public string selectedQuote;
    [SerializeField] public TextMeshProUGUI quoteUI;

    [HideInInspector] public float maxLoadTime = 6, minLoadTime = 2;
    [SerializeField] public float loadingTime;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        selectedQuote = quotes[rand.Next(0, quotes.Count - 1)];
        quoteUI.SetText(selectedQuote);
        loadingTime = (float)(rand.NextDouble() * (maxLoadTime - minLoadTime) + minLoadTime);
        StartCoroutine(LoadNextScene());
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(loadingTime);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("NavigationTutorialScene");
    }
}
