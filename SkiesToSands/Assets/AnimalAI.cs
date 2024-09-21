using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public List<Transform> waypoints;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private int _currentTarget;
    private bool _reverse;
    private bool _targetReached;

    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (waypoints.Count > 0 && waypoints[_currentTarget] != null)
        {   
            _agent.SetDestination(waypoints[_currentTarget].position);

            float distance = Vector3.Distance(transform.position, waypoints[_currentTarget].position);

            // if(distance < 4 && (_currentTarget == 0 || _currentTarget == waypoints.Count - 1)) {
            //     // GetComponent<Animator>().SetBool("Move", false);
            // }
            // else {
            //     // GetComponent<Animator>().SetBool("Move", true);
            // }

            if (distance < 4.0f && !_targetReached) {
                _targetReached = true;
                
                StartCoroutine(WaitBeForMoving());
            }
        }
    }

    IEnumerator WaitBeForMoving() {
        if(_currentTarget == waypoints.Count - 1) {
            yield return new WaitForSeconds(2f);
        }

        if(_reverse) {
            _currentTarget--;
            if(_currentTarget <= 0) {
                _reverse = false;
                _currentTarget = 0;
            }
        }
        else {
            _currentTarget++;
            if(_currentTarget == waypoints.Count) {
                _reverse = true;
                _currentTarget--;
            }
        }
        _targetReached = false;
    }
}