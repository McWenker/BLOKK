using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collection Objective", menuName = "NPC/Quest/Objective/Collection", order = 1)]
public class CollectionObjectiveScriptableObject : ObjectiveScriptableObject
{
    [SerializeField]
    private string description;

    [SerializeField]
    private int collectionTotal;

    [SerializeField]
    private string verb;

    [SerializeField]
    private GameObject itemToCollect;

    public string Description
    {
        get
        {
            return description;
        }
    }

    public int CollectionTotal
    {
        get
        {
            return collectionTotal;
        }
    }

    public string Verb
    {
        get
        {
            return verb;
        }
    }

    public GameObject ItemToCollect
    {
        get
        {
            return itemToCollect;
        }
    }
}
