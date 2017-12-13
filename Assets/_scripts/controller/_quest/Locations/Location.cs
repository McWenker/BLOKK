using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    private bool playerVisit = false;

    [SerializeField]
    private string title;

    [SerializeField]
    private int locID;

    public bool PlayerVisit
    {
        get
        {
            return playerVisit;
        }
    }

    public string Title
    {
        get
        {
            return title;
        }
    }

    public int LocID
    {
        get
        {
            return locID;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 12)
        {
            playerVisit = true;
            QuestController.instance.CheckLocation(locID);
        }
    }
}
