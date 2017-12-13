using UnityEngine;

namespace QuestSystem
{
    public class CollectionObjective : IQuestObjective
    {
        private string title;
        private string description;
        private string objectiveType = "collection";
        private int collectionTotal;
        private int collectionCurrent; // starts at 0
        private string verb;
        private GameObject itemToCollect;
        private bool isComplete;
        private bool isBonus;

        public string Title
        {
            get
            {
                return title;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public string ObjectiveType
        {
            get
            {
                return objectiveType;
            }
        }

        public int CollectionTotal
        {
            get
            {
                return collectionTotal;
            }
        }

        public int CollectionCurrent
        {
            get
            {
                return collectionCurrent;
            }
        }

        public GameObject ItemToCollect
        {
            get
            {
                return itemToCollect;
            }            
        }

        public bool IsComplete
        {
            get
            {
                return isComplete;
            }
        }

        public bool IsBonus
        {
            get
            {
                return isBonus;
            }
        }

        /// <summary>
        /// This constructor builds a CollectionObjective
        /// </summary>
        /// <param name="titleVerb">Describes the type of collection.</param>
        /// <param name="totalAmount">Amount required to complete objective.</param>
        /// <param name="item">Item to be collected.</param>
        /// <param name="descrip">Describe what will be collected.</param>
        /// <param name="bonus">If true, it is a bonus objective.</param>
        public CollectionObjective(string titleVerb, int totalAmount, GameObject item, string descrip, bool bonus)
        {
            title = titleVerb + " " + totalAmount + " " + item.name;
            verb = titleVerb;
            description = descrip;
            itemToCollect = item;
            collectionTotal = totalAmount;
            collectionCurrent = 0;
            isBonus = bonus;
            CheckProgress();
        }

        public void BeginCheck()
        {
            collectionCurrent = GameObject.Find("UI/PersistentCanvas/Inventory/BackpackColumns").GetComponent<Inventory>().CheckItem(itemToCollect);
            CheckProgress();
        }

        public void CheckItem(GameObject questItemToCheck)
        {
            if(questItemToCheck == itemToCollect)
            {
                UpdateProgress();
            }
        }

        public void CheckProgress()
        {
            if (collectionCurrent >= collectionTotal)
            {
                isComplete = true;
                Debug.Log(title + " completed.");
            }

        }

        public void UpdateProgress()
        {
            collectionCurrent++;
            Debug.Log("Collected " + collectionCurrent + "/" + collectionTotal + " " + itemToCollect.name);
            CheckProgress();
        }

        /*public override string ToString()
        {
            return collectionCurrent + "/" + collectionTotal + " " + itemToCollect.name + " " + verb + "ed.";
        }*/
    }
}