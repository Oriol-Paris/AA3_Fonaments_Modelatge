using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public List<Transform> delimiters = new List<Transform>();

    public float maxSpeed;
    public float proximityThreshold;

    private Vector3 velocity;
    private Vector3 desiredPosition;
    private float distanceToTarget;
    private bool canMove;


    private void Start()
    {
        desiredPosition = transform.position;
        velocity = Vector3.zero;
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
            IsNearDesiredPosition();
        }
    }

    public void Move()
    {
        Vector3 desiredVelocity = (desiredPosition - transform.position).normalized * maxSpeed;

        Vector3 steering = desiredVelocity - velocity;

        velocity += steering * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if (velocity.sqrMagnitude > 0.01f)
        {
            transform.forward = velocity.normalized;
        }
    }

    public void IsNearDesiredPosition()
    {
        distanceToTarget = Vector3.Distance(transform.position, desiredPosition);
        if (distanceToTarget <= proximityThreshold)
        {
            GetRandomPointInRange();
        }
    }

    public void GetRandomPointInRange()
    {
        if (delimiters.Count != 8)
            return;

        // Encuentra los límites del cubo
        float minX = Mathf.Min(delimiters[0].position.x, delimiters[1].position.x, delimiters[2].position.x, delimiters[3].position.x,
                               delimiters[4].position.x, delimiters[5].position.x, delimiters[6].position.x, delimiters[7].position.x);
        float maxX = Mathf.Max(delimiters[0].position.x, delimiters[1].position.x, delimiters[2].position.x, delimiters[3].position.x,
                               delimiters[4].position.x, delimiters[5].position.x, delimiters[6].position.x, delimiters[7].position.x);

        float minY = Mathf.Min(delimiters[0].position.y, delimiters[1].position.y, delimiters[2].position.y, delimiters[3].position.y,
                               delimiters[4].position.y, delimiters[5].position.y, delimiters[6].position.y, delimiters[7].position.y);
        float maxY = Mathf.Max(delimiters[0].position.y, delimiters[1].position.y, delimiters[2].position.y, delimiters[3].position.y,
                               delimiters[4].position.y, delimiters[5].position.y, delimiters[6].position.y, delimiters[7].position.y);

        float minZ = Mathf.Min(delimiters[0].position.z, delimiters[1].position.z, delimiters[2].position.z, delimiters[3].position.z,
                               delimiters[4].position.z, delimiters[5].position.z, delimiters[6].position.z, delimiters[7].position.z);
        float maxZ = Mathf.Max(delimiters[0].position.z, delimiters[1].position.z, delimiters[2].position.z, delimiters[3].position.z,
                               delimiters[4].position.z, delimiters[5].position.z, delimiters[6].position.z, delimiters[7].position.z);

        // Genera un punto aleatorio dentro de los límites
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        desiredPosition = new Vector3(randomX, randomY, randomZ);
    }

    public void SetCanMove(bool _canMove)
    {
        canMove = _canMove;
    }
}
