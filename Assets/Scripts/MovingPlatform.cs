using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetA, _targetB, _initialTarget;
    [SerializeField]
    private float _speed = 1.0f;

    Vector3 _currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        _currentTarget = _initialTarget.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        if (transform.position == _targetA.position)
        {
            _currentTarget = _targetB.position;
        }
        else if (transform.position == _targetB.position)
        {
            _currentTarget = _targetA.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
