using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifetime : MonoBehaviour
{
    [SerializeField] float particleSystemLifetime;
    private void Start()
    {
        Destroy(gameObject, particleSystemLifetime);
    }

}
