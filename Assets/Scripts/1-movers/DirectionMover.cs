using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMover : MonoBehaviour
{
    [Tooltip("Movement vector in meters per second")]
    [SerializeField] float velocity;

    [SerializeField] private Vector3 direction;

    void Update() {
        transform.position +=  direction * (velocity * Time.deltaTime);
    }

    public void SetVelocity(float newVelocity) {
        this.velocity = newVelocity;
    }

    public void SetDirection(Vector3 dir)
    {
        this.direction = dir;
    }
}
