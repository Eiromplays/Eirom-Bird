using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemey : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var bird = collision.collider.GetComponent<Bird>();
        if (bird != null)
            Destroy(gameObject);

        var enemy = collision.collider.GetComponent<Enemey>();
        if (enemy != null)
            return;

        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y < -0.5)
                Destroy(gameObject);
        }
    }
}
