using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private Image character;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private bool scriptedEvent;

    private AudioSource audioPlayer;
    public bool isOpen { get; private set; }

    
    private ResponseHandler responseHandler;
    //private EventHandler eventHandler;
    private TypewriterEffect typewriterEffect;

    // Start is called before the first frame update
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        audioPlayer = GetComponent<AudioSource>();
        //eventHandler = GetComponent<EventHandler>();
        CloseDialogueBox();
        
    }

    public void ShowDialogue(DialogObject dialogueObject)
    {
        isOpen = true; 
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogObject dialogueObject)
    { 
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            AudioClip voice;

            if (dialogueObject.Character[i].Voice != null)
            {
                voice = dialogueObject.DialogueVO[i];
            }
            else
            {
                voice = null;
            }
            

            Sprite charPortrait = dialogueObject.Character[i].CharImage;

            string name = dialogueObject.Character[i].CharName;

            bool events = dialogueObject.HasEvent[i];


            if (dialogueObject.Character[i].CharImage == null)
            {
                character.gameObject.SetActive(false);
            }
            else
            {
                character.gameObject.SetActive(true);
                character.sprite = charPortrait;
            }

            // activate VO
            if (voice != null)
            {
                audioPlayer.clip = voice;
                audioPlayer.Play();
            }

            
            if (events)
            {
                Debug.Log("EVVVEEENT THERES AN EVENT!!!!!");
                Debug.Log("MAKE SURE NAMES MATCH");
                // ok this is awful and i hope it just works so that i never have to do this again
                // find gameobjects
                EventHandler[] objs = FindObjectsOfType<EventHandler>();
                Debug.Log(objs);
                // iterate through each of the objects
                for (int i_ = 0; i_ < objs.Length;i_++)
                {
                    Debug.Log("event name: " + objs[i_].name);
                    Debug.Log("object name: " + dialogueObject.name);
                    // check if the names match (because thhat's the only way we can
                    // identify that the events are linked)
                    // MAKE SURE THAT THE GAMEOBJECT WITH THE EVENT SCRIPT ATTACHED AND 
                    // THE SCRIPTABLEOBJECT SHARE THE SAME NAME
                    if (objs[i_].name.ToLower() == dialogueObject.name.ToLower())
                    {
                        // hopefully runs the event
                        objs[i_].Events[i].OnDialogueRun.Invoke();
                    }
                }
            }

            nameLabel.text = name;

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses)
            {
                break;
            }

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
           StartCoroutine(CloseDialogueBox());
        }

        
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.isRunning)
        {
            yield return null;

            // old input system
            /* if (Input.GetKeyDown(KeyCode.E))
            {
                typewriterEffect.Stop();
            } */

            if (Input.GetKeyDown(KeyCode.E))
            {
                typewriterEffect.Stop();
            }
        }
    }

    public IEnumerator CloseDialogueBox()
    {
        
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;

        // wait a moment before dialogue can reopen

        yield return new WaitForSeconds(0.3f);

        isOpen = false;
    }

    public void EndDialogue()
    {
        if (!typewriterEffect.isRunning)
        {
            StartCoroutine(CloseDialogueBox());
        }
        
    }

    // don't think we need this cus its from seams' different input system

    /*public void StopTypeWriter(InputAction.CallbackContext context)
    {
        if (typewriterEffect.isRunning && textLabel.text != "")
        {
            typewriterEffect.Stop();
        }
    } */

}
