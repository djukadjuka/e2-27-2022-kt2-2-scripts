using Assets.Scripts.ObjectiveTriggerScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationTutorialLevelManager : MonoBehaviour
{

    [Header("Game Objects")]
    public GameObject player;
    public GameObject ObjectivePanel;
    public TextMeshProUGUI ObjectiveReminder;
    public AimStateManager player_AimStateManager;
    public MovementStateManager player_MovementStateManager;
    ObjectivePanelManager PanelManager;
    public List<GameObject> ObjectiveTriggers = new List<GameObject>();

    bool ObjectiveLoading = false;

    [Header("Objectives")]
    public int currentObjective = -1;
    public GameObject Bread;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player_AimStateManager = player.GetComponent<AimStateManager>();
        player_MovementStateManager = player.GetComponent<MovementStateManager>();
        PanelManager = ObjectivePanel.GetComponent<ObjectivePanelManager>();

        player_AimStateManager.UpdateEnabled = false;
        player_MovementStateManager.UpdateEnabled = false;
        player_MovementStateManager.RunEnabled = false;
        player_MovementStateManager.JumpEnabled = false;
        player_MovementStateManager.CrouchEnabled = false;

        StartCoroutine(NextObjective(1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!ObjectiveLoading)
        {
            switch (currentObjective)
            {
                case 0:
                    LookObjective();
                    break;
                case 1:
                    MoveObjectiveWalk();
                    break;
                case 2:
                    MoveObjectiveRun();
                    break;
                case 3:
                    MoveObjectiveCrouch();
                    break;
                case 4:
                    MoveObjectiveJump();
                    break;
                case 5:
                    MoveObjectiveFindBread();
                    break;
                default:
                    break;
            }
        }
    }
    
    public IEnumerator NextObjective(float wait)
    {
        this.currentObjective++;
        ObjectiveLoading = true;
        yield return new WaitForSeconds(wait);

        switch (currentObjective)
        {
            case 0:
                // LOOK OBJECTIVE
                yield return new WaitForSeconds(0.5f);
                PanelManager.SetText("Use the mouse to look around. Look around your surroundings to continue. Your current objective will be displayed in the top left corner.");
                PanelManager.FadePanelIn();
                yield return new WaitForSeconds(1f);
                PanelManager.FadePanelOut();
                ObjectiveReminder.SetText(" > Look around to continue.");
                player_AimStateManager.UpdateEnabled = true;
                break;
                // WALK OBJECTIVE
            case 1:
                Destroy(ObjectiveTriggers[0]);
                ObjectiveTriggers[1].SetActive(true);
                ObjectiveReminder.SetText("");
                PanelManager.SetText("Use the keys W, S, A, D to move forwards, backwards, strafe left and right. Use them to explore your surroundings.");
                PanelManager.FadePanelIn();
                yield return new WaitForSeconds(1f);
                PanelManager.FadePanelOut();
                ObjectiveReminder.SetText(" > Explore surroundings with the keys W, S, A, D.");
                player_MovementStateManager.UpdateEnabled = true;
                break;
            case 2:
                // RUN OBJECTIVE
                Destroy(ObjectiveTriggers[1]);
                ObjectiveTriggers[2].SetActive(true);
                ObjectiveReminder.SetText("");
                PanelManager.SetText("You can run holding down the shift key (default), that should speed up your exploration.");
                PanelManager.FadePanelIn();
                yield return new WaitForSeconds(1f);
                PanelManager.FadePanelOut();
                ObjectiveReminder.SetText(" > Use the shift key (default) to run around and explore the area.");
                player_MovementStateManager.RunEnabled = true;
                break;
            case 3:
                // CROUCH OBJECTIVE
                Destroy(ObjectiveTriggers[2]);
                ObjectiveTriggers[3].SetActive(true);
                ObjectiveReminder.SetText("");
                PanelManager.SetText("You'll have to crouch to get under that pile-up. Use the left ctrl key (default) to crouch and move under those obstacles.");
                PanelManager.FadePanelIn();
                yield return new WaitForSeconds(1f);
                PanelManager.FadePanelOut();
                ObjectiveReminder.SetText(" > Use the left ctrl key (default) to crawl under obstacles.");
                player_MovementStateManager.CrouchEnabled = true;
                break;
            case 4:
                // JUMP OBJECTIVE
                Destroy(ObjectiveTriggers[3]);
                ObjectiveTriggers[4].SetActive(true);
                ObjectiveReminder.SetText("");
                PanelManager.SetText("You'll have to jump over those obstacles to continue. Use the space key (default) to jump over those boxes.");
                PanelManager.FadePanelIn();
                yield return new WaitForSeconds(1f);
                PanelManager.FadePanelOut();
                ObjectiveReminder.SetText(" > Use the space key (default) to jump over obstacles.");
                player_MovementStateManager.JumpEnabled = true;
                break;
            case 5:
                // FIND FOOD OBJECTIVE
                Destroy(ObjectiveTriggers[4]);
                // ObjectiveTriggers[5].SetActive(true);
                ObjectiveReminder.SetText("");
                PanelManager.SetText("Well done, you understand the basics of moving around the world. ");
                PanelManager.FadePanelIn();
                yield return new WaitForSeconds(1f);
                PanelManager.FadeTextOut();
                yield return new WaitForSeconds(1f);
                PanelManager.SetText("It's time to find some food. Use the movement controls to explore the environment and find some bread. When you do, use the E key (default) to put it in your inventory.");
                PanelManager.FadeTextIn();
                yield return new WaitForSeconds(1f);
                PanelManager.FadePanelOut();
                ObjectiveReminder.SetText(" > Use the movement controls to find bread and put it in your inventory using the E key (default).");
                break;
            case 6:
                // CONGRATS
                // Destroy(ObjectiveTriggers[5]);
                ObjectiveReminder.SetText("");
                PanelManager.SetText("Congratulations, that concludes the demo for the controls. Hope to see you again in the complete product!");
                PanelManager.FadePanelIn();
                yield return new WaitForSeconds(1f);
                PanelManager.FadePanelOut();
                yield return new WaitForSeconds(1f);
                // Return to main menu from here
                SceneManager.LoadScene("MainMenuScene");
                break;
            default:
                break;
        }

        ObjectiveLoading = false;
    }

    private void LookObjective()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.name == "LookObjectiveTrigger")
            {
                StartCoroutine(NextObjective(0.0f));
            }
        }
    }

    private void MoveObjectiveWalk()
    {
        if (ObjectiveTriggers[1].GetComponent<PlayerMovementTrigger>().PlayerMovedOut)
        {
            StartCoroutine(NextObjective(0.0f));
        }
    }

    private void MoveObjectiveRun()
    {
        if (ObjectiveTriggers[2].GetComponent<PlayerMovementTrigger>().PlayerMovedInto)
        {
            StartCoroutine(NextObjective(0.0f));
        }
    }

    private void MoveObjectiveCrouch()
    {
        if (ObjectiveTriggers[3].GetComponent<PlayerMovementTrigger>().PlayerMovedInto)
        {
            StartCoroutine(NextObjective(0.0f));
        }
    }

    private void MoveObjectiveJump()
    {
        if (ObjectiveTriggers[4].GetComponent<PlayerMovementTrigger>().PlayerMovedInto)
        {
            StartCoroutine(NextObjective(0.0f));
        }
    }

    private void MoveObjectiveFindBread()
    {
        if (!Bread)
        {
            StartCoroutine(NextObjective(0.0f));
        }
    }
}
