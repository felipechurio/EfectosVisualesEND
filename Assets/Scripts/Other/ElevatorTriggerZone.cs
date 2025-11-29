using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTriggerZone : MonoBehaviour
{
    public enum TriggerType { ElevatorArea, ElevatorEnter, ElevatorExit } 
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private ElevatorController elevatorController;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (triggerType)
        {
            case TriggerType.ElevatorArea:
                elevatorController.PlayerEnteredElevatorArea();
                break;

            case TriggerType.ElevatorEnter:
                elevatorController.PlayerEnteredElevator();
                break;

            case TriggerType.ElevatorExit:
                elevatorController.PlayerExitedElevator();
                break;
        }
    }
}
