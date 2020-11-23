using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.Damage();
                CharacterController cc = other.GetComponent<CharacterController>();
                if (cc)
                {
                    cc.enabled = false;
                }
                player.transform.position = _spawnPoint.position;
                if (cc)
                {
                    StartCoroutine(RenableCharacterController(cc));
                }
            }
        }
    }
    IEnumerator RenableCharacterController(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
