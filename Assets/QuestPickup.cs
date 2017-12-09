using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPickup : MonoBehaviour
{
    [SerializeField]
    ItemScriptableObject itemData;

    public ItemScriptableObject ItemData
    {
        get
        {
            return itemData;
        }
    }
}
