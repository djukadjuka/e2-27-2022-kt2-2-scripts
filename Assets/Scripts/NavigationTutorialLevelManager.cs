using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player_AimStateManager = player.GetComponent<AimStateManager>();
        player_MovementStateManager = player.GetComponent<MovementStateManager>();
        PanelManager = ObjectivePanel.GetComponent<ObjectivePanelManager>();

        player_AimStateManager.UpdateEnabled = false;
        player_MovementStateManager.UpdateEnabled = false;
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
                    MoveObjective();
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

    private void MoveObjective()
    {
        if (ObjectiveTriggers[1].GetComponent<MoveObjectiveTriggerScript>().PlayerLeft)
        {
            Debug.Log("Ayyoooo");
        }
    }
}
