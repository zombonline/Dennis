using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            FindObjectOfType<GameSessionScore>().projectilesAvoided++;
            Destroy(collision.gameObject);
        }
    }
}
