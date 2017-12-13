using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location Objective", menuName = "NPC/Quest/Objective/Location", order = 2)]
public class LocationObjectiveScriptableObject : ObjectiveScriptableObject
{
    [SerializeField]
    private string description;

    [SerializeField]
    private Location location;

    [SerializeField]
    private string locale;

    public string Description
    {
        get
        {
            return description;
        }
    }

    public Location Location
    {
        get
        {
            return location;
        }
    }

    public string Locale
    {
        get
        {
            return locale;
        }
    }
}
