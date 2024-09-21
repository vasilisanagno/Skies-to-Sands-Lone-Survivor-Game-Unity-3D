using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float offsetX, offsetZ;
    [SerializeField] private float LerpSpeed;
    // Start is called before the first frame update
    private void LateUpdate() {
        transform.position = Vector3.Lerp(transform.position,
        new Vector3(target.position.x + offsetX, target.position.y + 161.9938f, target.position.z + offsetZ), LerpSpeed);
    }
}
