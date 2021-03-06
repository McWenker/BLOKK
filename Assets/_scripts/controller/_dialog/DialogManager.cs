﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngineInternal;


public class DialogManager : MonoBehaviour
{
    // Singleton reference
    private static DialogManager _instance;

    // Public getter for the singleton reference
    public static DialogManager instance
    {
        get
        {
            // If there is no instance for DialogManager yet and we're not shutting down at the moment
            if (_instance == null && !isShuttingDown)
            {
                //Try finding and instance in the scene
                _instance = GameObject.FindObjectOfType<DialogManager>();
                //If no instance was found, let's create one
                if (!_instance)
                {
                    GameObject singleton = (GameObject)Instantiate(Resources.Load("DialogManager"));
                    singleton.name = "DialogManager";
                    _instance = singleton.GetComponent<DialogManager>();
                }
                //Set the instance to persist between levels.
                DontDestroyOnLoad(_instance.gameObject);
            }
            //Return an instance, either that we found or that we created.
            return _instance;
        }
    }
    
    public static bool isShuttingDown;
    
    //Unity calls this function when quitting, I'm using that info to avoid creating
    //something when the game is quitting as unity doesn't like that.
    void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    //I made 2 types of dialogues, one that is only one OK Button
    //and another that has both an Yes and a No button which lead
    //to different callbacks.
    public enum DialogType
    {
        OkDialog,
        YesNoDialog,
        QuestDialog
    };

    //Store the container panel that coantains both dialog boxes.
    public CanvasGroup dialogCanvasGroup;
    //Game object for the OK only dialog box
    public GameObject okDialogObject;
    //Game Object for the YesNO dialog box
    public GameObject yesNoDialogObject;
    //Game Object for the QuestFinish dialog box
    public GameObject questDialogObject;

    //Here go the dialog texts for both dialogs.
    public Text[] dialogText;
    //Here go the speaker images for both dialogs.
    public Image[] speakerImage;
    //Here go the speaker names for both dialogs
    public Text[] speakerName;

    //We're going to use a void delegate for the callbacks
    public delegate void dialogAnswer();
    //We have one for ok/yes
    public dialogAnswer okAnswer;
    //And one for no
    public dialogAnswer noAnswer;
    
    //Bool to check if there is already a dialog currently showing.
    public static bool showingDialog;

    void Awake()
    {
        //If there is no instance of this currently in the scene
        if (_instance == null)
        {
            //Set ourselves as the instance and mark us to persist between scenes
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If there is already an instance of this and It's not me, then destroy me as there should only be one.
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// This is the method we'll call to show the dialog. It takes a string for title, one for text,
    /// a dialog type (Ok only or Yes/No) and 2 callbacks, one for OK/Yes and the other one for NO
    /// </summary>
    /// <param name="_speakerImage">Image for the dialog speaker</param>
    /// <param name="_speakerName">Text for the dialog speaker's name</param>
    /// <param name="_text">Text contained in the dialog box</param>
    /// <param name="_desiredDialog">Type of the Dialog Box, either DialogType.OkDialog or DialogType.YesNoDialog</param>
    /// <param name="_dialogAnswer">Callback to call if user pressed [Ok] or [Yes] buttons</param>
    /// <param name="_dialogNegativeAnswer">Callback to call if the user presses the [No] button</param>
    public static void PopUpDialog(Sprite _speakerImage, string _speakerName, string _text,DialogType _desiredDialog = DialogType.OkDialog, dialogAnswer _dialogAnswer = null, dialogAnswer _dialogNegativeAnswer = null)
    {
        //If we're showing dialog already stop here.
        if (showingDialog) return;
        //Set the showing dialog bool to true to prevent another dialog over this.
        showingDialog = true;
        
        //Set our dialog boxes to show or not show based on it's desired type.
        switch (_desiredDialog)
        {
                case DialogType.OkDialog:
                    instance.okDialogObject.SetActive(true);
                    instance.yesNoDialogObject.SetActive(false);
                    instance.questDialogObject.SetActive(false);
                break;
                case DialogType.YesNoDialog:
                    instance.okDialogObject.SetActive(false);
                    instance.yesNoDialogObject.SetActive(true);
                    instance.questDialogObject.SetActive(false);
                break;
                case DialogType.QuestDialog:
                    instance.okDialogObject.SetActive(false);
                    instance.yesNoDialogObject.SetActive(false);
                    instance.questDialogObject.SetActive(true);
                break;
        }

        //Fill all the texts with the desired text.
        for (int _i = 0; _i < instance.dialogText.Length; _i++)
        {
            instance.dialogText[_i].text = _text;
        }

        //Fill all the titles with the desired title.
        for (int _i = 0; _i < instance.speakerImage.Length; _i++)
        {
            instance.speakerImage[_i].sprite = _speakerImage;
        }

        //Fill all the titles with the desired title.
        for (int _i = 0; _i < instance.speakerName.Length; _i++)
        {
            instance.speakerName[_i].text = _speakerName;
        }

        //Show the dialog canvas.
        instance.dialogCanvasGroup.gameObject.SetActive(true);

        //Set our callbacks to the ones we received.
        instance.okAnswer = _dialogAnswer;
        instance.noAnswer = _dialogNegativeAnswer;
    }

    
    public void DismissDialog(bool _answer)
    {

        //Hide the gameobjects and set the showingDialog back to false to allow for new dialog calls.
        okDialogObject.SetActive(false);
        yesNoDialogObject.SetActive(false);
        dialogCanvasGroup.gameObject.SetActive(false);
        showingDialog = false;

        //If answer is true call YES/OK delegate if one exists
        if (_answer)
        {
            if (okAnswer != null)
            {
                okAnswer();
            }
        }
        else
        {
            //If answer is false call NO delegate if one exists.
            if (noAnswer != null)
            {
                noAnswer();
            }
        }
    }

    
}
