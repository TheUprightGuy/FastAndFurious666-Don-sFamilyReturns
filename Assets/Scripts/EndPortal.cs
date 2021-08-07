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
    private void Start()
    {
        //ExitingLevel.SetActive(true);
        //EnteringLevel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            TriggerPortal();
            StartArena();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((AILayers == (AILayers | (1 << other.gameObject.layer))) ||
            GetComponent<CarlAI>()) //Hit thing is an AI
        {
            other.gameObject.SetActive(false);
        }

        if (other.GetComponent<Movement>()) //AI is player
        {
            player = other.gameObject;
            TriggerPortal();
        }
    }

    void StartArena()
    {
        player.SetActive(true);

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

    void TriggerPortal()
    {
        player.SetActive(false);
        
        BadEnding();

        //if (true) //Check for bad deeds on the player in here
        //{
        //    GoodEnding();
        //}
        //else
        //{
        //    BadEnding();
        //}
        
    }

    void BadEnding()
    {
        //Get AI not yet in portal
        if (AIContainer != null)
        {

            int aiCount = AIContainer.transform.childCount;
            float spacing = 360.0f / aiCount;

            player.transform.position = new Vector3(
                    Radius * Mathf.Cos((aiCount * spacing) * Mathf.Deg2Rad),
                    0.0f,
                    Radius * Mathf.Sin((aiCount * spacing) * Mathf.Deg2Rad)) + DestinationMarker.transform.position; //placements

            for (int i = 0; i < aiCount - 1; i++)
            {
                AIContainer.transform.GetChild(i).gameObject.SetActive(false); //Turn off
                AIContainer.transform.GetChild(i).transform.position = new Vector3(
                    Radius * Mathf.Cos((i * spacing) * Mathf.Deg2Rad),
                    0.0f,
                    Radius * Mathf.Sin((i * spacing) * Mathf.Deg2Rad)) + DestinationMarker.transform.position; //placements
            }
        }

        DoCutScene();
       
    }
    void GoodEnding()
    {
        //GO INTO END SCENE
    }
    void DoCutScene()
    {
        //TRIGGER CUTSCENE HERE, CALL STARTARENA ON CUTSCENE FINISH
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
