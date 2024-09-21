using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAISoldier : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 6;
    public float speedRun = 9;
    
    public float temp = 0; 
    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;
    public float hitChance = 0.75f;
    public int hitDamage = 25;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        EnviromentView();

        if(!m_IsPatrol){
            Chasing();
        }
        else {
            GetComponent<Animator>().SetBool("Attack", false);
            Patroling();
        }
    }

    private void Chasing() {
        GetComponent<Animator>().SetBool("Attack", false);
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer) {
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);

            // Rotate to face the player
            Vector3 direction = (m_PlayerPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 15f) {

            // make it attack as it's less than stopping distance
            
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 12f) {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                GetComponent<Animator>().SetTrigger("Run");
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)>= 10f){
                    Stop();
                    GetComponent<Animator>().SetBool("Attack",true);
                    if (UnityEngine.Random.value <= hitChance)
                    {
                        // Simulate shooting by applying damage directly to the player
                        player.GetComponent<PlayerHealth>().TakeDamage(hitDamage);
                    }
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Patroling() {

        if (m_PlayerNear) {
            if (m_TimeToRotate <= 0){
                Move(speedWalk);
                GetComponent<Animator>().SetTrigger("Run");
                LookingPlayer(playerLastPosition);
            }
            else {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance ||( temp == navMeshAgent.remainingDistance && temp != 0))
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    GetComponent<Animator>().SetTrigger("GoIdle");
                    m_WaitTime -= Time.deltaTime;
                }
            }
            else {
                GetComponent<Animator>().SetTrigger("Run");
            }

            temp = navMeshAgent.remainingDistance;
        }
    }

    void Move(float speed) {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void Stop() {
        // navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void NextPoint() {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        
    }

    void CaughtPlayer() {
        m_CaughtPlayer = true;
    }

    void LookingPlayer(Vector3 player) {
        navMeshAgent.SetDestination(player);

        // Rotate to face the player
        Vector3 direction = (player - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if(Vector3.Distance(transform.position, player) <= 0.3f){
            if(m_WaitTime <= 0){
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView() {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }

            if (Vector3.Distance(transform.position, player.position) > viewRadius){
                m_PlayerInRange = false;
            }

            if(m_PlayerInRange) {
                m_PlayerPosition = player.position;
            }
        }
    }
}
