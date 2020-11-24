using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MovingBox"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < 0.05)
            {
                Rigidbody body = other.transform.GetComponent<Rigidbody>();
                if (body)
                {
                    body.isKinematic = true;
                    MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
                    if (renderer)
                    {
                        renderer.material.color = Color.blue;
                    }
                    Destroy(this);
                }
            }
        }
    }
}
