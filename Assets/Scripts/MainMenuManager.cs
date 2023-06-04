using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] public Animator animator;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        animator.SetTrigger("Start");
        StartCoroutine(StartNewGameCoroutine());
    }

    IEnumerator StartNewGameCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("DummyLoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
