using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState 
{
    WALKING,
    RUNNING
}

public partial class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement staticRef;
    public static PlayerMovement GetReference => staticRef;

    [SerializeField]
    Transform playerInputSpace;
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;
    [SerializeField] DialogueUI dialogueUI;

    private MovementState movementState;

    Rigidbody body;
    Vector3 velocity, desiredVelocity;

    [SerializeField] private KeyCode interactionBind;
    [SerializeField] private Animator animator;

    public IInteractable Interactable { get; set; }
    public DialogueUI DialogueUI => dialogueUI;

    private DialogueActivator currentDialogueActivator;

    public bool canMove;

    public bool onPickup;

    public bool beingAttacked;

    private bool spaghetti = true;

    public bool onDialogue;


    private void Awake()
    {
        staticRef = this;
        canMove = true;
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (DialogueUI.isOpen)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        Vector2 playerInput;

        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        if (playerInputSpace)
        {
            Vector3 forward = playerInputSpace.forward;
            forward.y = 0f;
            forward.Normalize();

            Vector3 right = playerInputSpace.right;
            right.y = 0f;
            right.Normalize();

            desiredVelocity = (forward * playerInput.y + right * playerInput.x) * maxSpeed;

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementState = MovementState.RUNNING;
            animator.SetBool("Running", true);
            maxSpeed = 10f;
            // maxAcceleration = 20f;
            spaghetti = false;
        }
        else
        {
            if (!spaghetti)
            {
                maxSpeed = 5.5f;
                spaghetti = true;
            }

            movementState = MovementState.WALKING;
            animator.SetBool("Running", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            maxSpeed = 5.5f;
            //maxAcceleration = 10f;
        }

        // controls switching between idle and walking animation
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        // activate dialogue on collision
        if (currentDialogueActivator != null)
        {
            if (currentDialogueActivator.activateOnCollision)
            {
                if (!dialogueUI.isOpen)
                {
                    Interactable.Interact(this);
                }
            }
        }

        if (Input.GetKeyDown(interactionBind))
        {
            Interaction();
        }

        if (!canMove)
        {
            body.velocity = Vector3.zero;
        }

        //Debug.Log(Interactable);
    }

    // dont ask why all this music stuff is in playermovement i'm in a rush ok
    private int RandomAmbienceDecide(int maxAmountOfThemes)
    {
        return Random.Range(1, maxAmountOfThemes);
    }

    private void Interaction()
    {
        Debug.Log("Interaction key pressed");
        //begin dialogue
        if (Interactable != null && !DialogueUI.isOpen)
        {
            Debug.Log("Run dialogue please");
            Interactable.Interact(this);
            ResetDialogueStore();
        }

        if (onPickup)
        {
            // this will not work if multiple weapon pickups are in the scene
            WeaponPickup wep = FindObjectOfType<WeaponPickup>();

            if (wep != null)
            {
                wep.PickUpWeapon();
                onPickup = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            velocity = body.velocity;
            float maxSpeedChange = maxAcceleration * Time.deltaTime;

            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

            body.velocity = velocity;
        }     
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "PlaceholderDDRActivation":
                UnityEngine.SceneManagement.SceneManager.LoadScene("DDRTesting");
                break;
            case "PlaceholderBattleActivation":
                UnityEngine.SceneManagement.SceneManager.LoadScene("BattleTesting");
                break;
            case "DialogueTrigger":
                if (other.GetComponent<DialogueActivator>().activateOnCollision)
                {
                    currentDialogueActivator = other.gameObject.GetComponent<DialogueActivator>();
                }
                else
                {
                    onDialogue = true;
                }
                break;
            case "Teleporter":
                // load manager now :DD
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Dungeon w Castle");

                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().StartLoadingArea("EndSlums");

                break;
            case "EndTeleporter":
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().StartLoadingArea("CreditsScene");
                break;
            case "WeaponPickup":
                other.GetComponent<WeaponPickup>().weaponRef = GetComponent<Weapon>();
                onPickup = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "DialogueTrigger":
                Debug.LogWarning("Leaving dialogue");
                currentDialogueActivator = null;
                Interactable = null;
                onDialogue = false;
                break;
            case "WeaponPickup":
                Debug.Log("LEAVING PICKUP");
                other.GetComponent<WeaponPickup>().weaponRef = null;
                onPickup = false;
                break;
        }
    }

    // hack fix for a thing
    public void ResetDialogueStore()
    {
        currentDialogueActivator = null;
        Interactable = null;
    }
}
