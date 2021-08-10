using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //public Vector3 destination;

    [Header("AI")]
    public LayerMask AILayers;
    public GameObject AIContainer;

    [Header("Destination")]
    public Transform DestinationMarker;
    public float Spacings;
    public int NumOfCols = 2;
    public List<GameObject> Placings = new List<GameObject>();

    [Header("Levels")]
    public GameObject ExitingLevel;
    public GameObject EnteringLevel;

    public GameObject player;
    private void Start()
    {
        ExitingLevel.SetActive(true);
        EnteringLevel.SetActive(false);
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((AILayers == (AILayers | (1 << other.gameObject.layer))) ||
            GetComponent<CarlAI>()) //Hit thing is an AI
        {
            Placings.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }

        if (other.GetComponent<Movement>()) //AI is player
        {
            Placings.Add(other.gameObject);
            TriggerPortal();
        }
        // TEMP
        //if (other.GetComponent<Movement>())
        //    CallbackHandler.instance.ShowEndScreen(EndState.Killed);
    }

    public void TriggerPortal()
    {
        //Get AI not yet in portal
        if (AIContainer != null)
        {
            foreach (Transform ai in AIContainer.transform)
            {
                if (ai.gameObject.activeSelf) //Not hit portal yet
                {
                    ai.gameObject.SetActive(false);
                    Placings.Add(ai.gameObject); //I could sort this by placings but I somehow doubt the player will noice
                }
            }
        }

        for (int i = 0; i < Placings.Count; i++)
        {
            int iPlace = Placings.Count - i;
            int y = Mathf.FloorToInt(iPlace / NumOfCols);
            int x = iPlace - (y * NumOfCols);
            Vector3 pos = new Vector3(x * Spacings, 0.0f, y * Spacings) + DestinationMarker.transform.position;
            
            Placings[i].transform.position = pos;
            Placings[i].transform.forward = DestinationMarker.transform.forward;
        }

        ////Preset positioning and rotation
        //for (int i = 0; i < Placings.Count; i++)
        //{
        //    float placing = Placings.Count - i;

        //    int y = Mathf.FloorToInt(placing / NumOfCols);
        //    int x =  (y * NumOfCols) - i;
        //    Vector3 pos = new Vector3(x * Spacings, 0.0f, y * Spacings) + DestinationMarker.transform.position;
        //    Placings[i].transform.position = pos;
        //    Placings[i].transform.forward = DestinationMarker.transform.forward;

        //}

        RoadUtilities.instance.SetRoad(EnteringLevel.GetComponentInChildren<LineRenderer>());

        //Set everything to true
        foreach (GameObject car in Placings)
        {
            car.SetActive(true);
        }

        //Wayd if you wanna do a countdown on next level probs chuck a pause at the same time when everything is set active
        //this.transform.parent.gameObject.SetActive(false);
        Placings.Clear(); //Clear this cause I'm pedantic

        //Setup next level environment
        ExitingLevel.SetActive(false);
        EnteringLevel.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        if (DestinationMarker != null)
        {
            for (int i = 0; i < 9; i++)
            {
                int y = Mathf.FloorToInt(i/NumOfCols);
                int x = i - (y * NumOfCols);
                Vector3 pos = new Vector3(x * Spacings, 0.0f, y * Spacings) + DestinationMarker.transform.position;
                Gizmos.DrawSphere(pos, 0.5f);
          
            }
        }
    }
}
