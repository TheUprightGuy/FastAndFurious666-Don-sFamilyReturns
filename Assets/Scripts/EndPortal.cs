using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    //public Vector3 destination;

    [Header("AI")]
    public LayerMask AILayers;
    public GameObject AIContainer;

    [Header("Destination")]
    public Transform DestinationMarker;
    public float Radius = 5.0f;

    [Header("Levels")]
    public GameObject ExitingLevel;
    public GameObject EnteringLevel;

    public GameObject player;

    int position = 0;

    private void Start()
    {
        //ExitingLevel.SetActive(true);
        //EnteringLevel.SetActive(false);
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((AILayers == (AILayers | (1 << other.gameObject.layer))) ||
            GetComponent<CarlAI>()) //Hit thing is an AI
        {
            other.gameObject.SetActive(false);
            position++;
        }

        if (other.GetComponent<Movement>()) //AI is player
        {
            player = other.gameObject;
            TriggerPortal();
        }
    }

    public void StartArena()
    {
        player.SetActive(true);
        CallbackHandler.instance.ShowEndScreen(EndState.None);
        CallbackHandler.instance.ToggleFreeze(false);

        //Setup next level environment
        ExitingLevel.SetActive(false);
        EnteringLevel.SetActive(true);

        if (AIContainer != null)
        {
            foreach (Transform ai in AIContainer.transform)
            {
                ai.gameObject.SetActive(true);
                ai.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ai.GetComponent<CarlAI>().RoadUtils = null;
            }
            
        }
    }

    public void TriggerPortal()
    {
        player.SetActive(false);
        player.GetComponentInChildren<Gun>().ToggleWeapon(GunType.None);
        CallbackHandler.instance.ToggleFreeze(true);

        // Check to see if any AI are alive - if all are dead trigger you win
        if (!CallbackHandler.instance.CheckSurvivors())
        {
            CallbackHandler.instance.ShowEndScreen(EndState.Win);

            CallbackHandler.instance.ToggleFreeze(true);
            // Delay - Go to next screen, show ty message
            Invoke("ShowThankYou", 5.0f);
        }

        if (!CallbackHandler.instance.GetKilled()) //Check for bad deeds on the player in here
        {
            GoodEnding();
        }
        else
        {
            BadEnding();
        }   
    }

    void BadEnding()
    {
        //Get AI not yet in portal
        if (AIContainer != null)
        {
            int aiCount = AIContainer.transform.childCount;
            float spacing = 360.0f / (aiCount+1);

            Vector3 newPos =new Vector3(
                    Radius * Mathf.Cos(((0) * spacing) * Mathf.Deg2Rad),
                    0.0f,
                    Radius * Mathf.Sin(((0) * spacing) * Mathf.Deg2Rad)) + DestinationMarker.transform.position; //placements
            player.transform.position = newPos;
            player.transform.forward = EnteringLevel.transform.position - newPos;

            for (int i = 1; i < aiCount+1; i++)

            {
                AIContainer.transform.GetChild(i-1).gameObject.SetActive(false); //Turn off
                newPos = new Vector3(
                    Radius * Mathf.Cos((i * spacing) * Mathf.Deg2Rad),
                    0.0f,
                    Radius * Mathf.Sin((i * spacing) * Mathf.Deg2Rad)) + DestinationMarker.transform.position; //placements
                AIContainer.transform.GetChild(i - 1).transform.position = newPos;
                AIContainer.transform.GetChild(i - 1).transform.forward = EnteringLevel.transform.position - newPos;
            }
        }

        DoCutScene();     
    }

    void GoodEnding()
    {
        //GO INTO END SCENE
        // Get position
        if (position == 0)
        {
            CallbackHandler.instance.ShowEndScreen(EndState.First);
        }
        else if (position < 3)
        {
            CallbackHandler.instance.ShowEndScreen(EndState.SecondThird);
        }
        else
        {
            CallbackHandler.instance.ShowEndScreen(EndState.SecondThird);
        }

        // Delay - Go to next screen, show ty message
        Invoke("ShowThankYou", 5.0f);
    }

    void ShowThankYou()
    {
        CallbackHandler.instance.ShowEndScreen(EndState.Thanks);
    }

    void DoCutScene()
    {
        //TRIGGER CUTSCENE HERE, CALL STARTARENA ON CUTSCENE FINISH
        CallbackHandler.instance.ShowEndScreen(EndState.Killed);
        AudioHandler.instance.ToggleBGM();
        Invoke("StartArena", 5.0f);
    }

    private void OnDrawGizmos()
    {
        if (DestinationMarker != null)
        {
            float spacing = 360.0f / 9;
            for (int i = 0; i < 9; i++)
            {
                Vector3 pos = new Vector3(
                    Radius * Mathf.Cos((i * spacing) * Mathf.Deg2Rad), 
                    0.0f, 
                    Radius * Mathf.Sin((i * spacing) * Mathf.Deg2Rad));

                Gizmos.DrawSphere(pos + DestinationMarker.transform.position, 0.5f);
            }
        }
    }
}
