using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _callButton;
    [SerializeField]
    private int _coinsNeeded = 8;
    [SerializeField]
    private Elevator _elevator;

    private bool _elevatorCalled = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (Input.GetKeyDown(KeyCode.E) && player.Coins >= _coinsNeeded)
            {
                if (_callButton) {
                    if (_elevatorCalled)
                    {
                        _callButton.material.SetColor("_Color", Color.red);
                    }
                    else
                    {
                        _callButton.material.SetColor("_Color", Color.green);
                    }
                }
                _elevator.CallElevator();
            }
        }
    }
}
