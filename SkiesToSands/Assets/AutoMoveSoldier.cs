using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AutoMoveSoldier : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        // Move the soldier forward continuously
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

