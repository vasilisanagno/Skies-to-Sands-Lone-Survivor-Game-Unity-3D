using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 30f;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Target")) {
            print("hit " + collision.gameObject.name + " !");
            EnemyHealth target = collision.gameObject.transform.GetComponent<EnemyHealth>();
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
