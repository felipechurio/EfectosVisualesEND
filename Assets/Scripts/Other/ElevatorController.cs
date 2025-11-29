using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private GameObject activeObject;

    [Header("Doors")]
    [SerializeField] private Transform rightDoor1;
    [SerializeField] private Transform rightDoor2;
    [SerializeField] private Transform leftDoor1;
    [SerializeField] private Transform leftDoor2;

    [Header("Elevator")]
    [SerializeField] private Transform elevator;
    [SerializeField] private float liftHeight = 5f;
    [SerializeField] private float liftTime = 3f;

    [Header("Door movement")]
    [SerializeField] private float doorMoveDistance = 1f;
    [SerializeField] private float doorMoveTime = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorOpenSound;

    private bool isMoving = false;
    private bool doorsAreOpen = false;
    private bool waitingForExitTrigger = false;
    private bool doorOpenSoundPlayed = false; 

    private Vector3 rightDoor1ClosedPos;
    private Vector3 rightDoor2ClosedPos;
    private Vector3 leftDoor1ClosedPos;
    private Vector3 leftDoor2ClosedPos;

    private void Start()
    {
        rightDoor1ClosedPos = rightDoor1.localPosition;
        rightDoor2ClosedPos = rightDoor2.localPosition;
        leftDoor1ClosedPos = leftDoor1.localPosition;
        leftDoor2ClosedPos = leftDoor2.localPosition;
    }


    public void PlayerEnteredElevatorArea()
    {
        if (!isMoving && !activeObject.activeSelf)
            StartCoroutine(OpenDoors());
    }

    public void PlayerEnteredElevator()
    {
        if (!isMoving && doorsAreOpen)
            StartCoroutine(StartElevatorSequence());
    }

    public void PlayerExitedElevator()
    {
        if (waitingForExitTrigger)
        {
            StartCoroutine(CloseDoorsAfterExit());
        }
    }


    private IEnumerator StartElevatorSequence()
    {
        isMoving = true;


        yield return StartCoroutine(MoveDoors(false));


        Vector3 startPos = elevator.position;
        Vector3 endPos = startPos + Vector3.up * liftHeight;
        float elapsed = 0f;
        while (elapsed < liftTime)
        {
            elevator.position = Vector3.Lerp(startPos, endPos, elapsed / liftTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        elevator.position = endPos;


        yield return StartCoroutine(MoveDoors(true));
        waitingForExitTrigger = true; 
        isMoving = false;
    }

    private IEnumerator CloseDoorsAfterExit()
    {
        waitingForExitTrigger = false;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(MoveDoors(false));
    }

    private IEnumerator OpenDoors()
    {
        if (doorsAreOpen) yield break;

        PlayDoorSound(); 
        yield return StartCoroutine(MoveDoors(true));
        doorsAreOpen = true;
    }

    private IEnumerator MoveDoors(bool open)
    {
        float elapsed = 0f;

        Vector3 r1Start = rightDoor1.localPosition;
        Vector3 r2Start = rightDoor2.localPosition;
        Vector3 l1Start = leftDoor1.localPosition;
        Vector3 l2Start = leftDoor2.localPosition;

        Vector3 r1Target = rightDoor1ClosedPos + (open ? new Vector3(0f, 0f, doorMoveDistance) : Vector3.zero);
        Vector3 r2Target = rightDoor2ClosedPos + (open ? new Vector3(0f, 0f, doorMoveDistance) : Vector3.zero);
        Vector3 l1Target = leftDoor1ClosedPos + (open ? new Vector3(0f, 0f, -doorMoveDistance) : Vector3.zero);
        Vector3 l2Target = leftDoor2ClosedPos + (open ? new Vector3(0f, 0f, -doorMoveDistance) : Vector3.zero);

        while (elapsed < doorMoveTime)
        {
            rightDoor1.localPosition = Vector3.Lerp(r1Start, r1Target, elapsed / doorMoveTime);
            rightDoor2.localPosition = Vector3.Lerp(r2Start, r2Target, elapsed / doorMoveTime);
            leftDoor1.localPosition = Vector3.Lerp(l1Start, l1Target, elapsed / doorMoveTime);
            leftDoor2.localPosition = Vector3.Lerp(l2Start, l2Target, elapsed / doorMoveTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rightDoor1.localPosition = r1Target;
        rightDoor2.localPosition = r2Target;
        leftDoor1.localPosition = l1Target;
        leftDoor2.localPosition = l2Target;

        doorsAreOpen = open;
    }


    private void PlayDoorSound()
    {
        if (!doorOpenSoundPlayed) 
        {
            if (audioSource != null && doorOpenSound != null)
            {
                audioSource.PlayOneShot(doorOpenSound);
                doorOpenSoundPlayed = true;
            }
        }
    }
}