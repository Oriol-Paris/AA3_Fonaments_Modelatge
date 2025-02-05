using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fabrik : MonoBehaviour
{
    public List<Transform> joints;
    public Transform target;

    private float tolerance = 2f;
    private float maxIterations = 1e5f;
    private float moveSpeed = 1.0f;
    private float rotationSpeed = 45.0f;

    private float lambda;
    private Vector3[] Links;
    private int countIterations;
    private List<Vector3> initPosition;
    bool canMove = true;


    void Start()
    {
        Links = new Vector3[joints.Count - 1];

        for (int i = 0; i < joints.Count - 1; i++)
        {
            Links[i] = joints[i + 1].position - joints[i].position;
        }

        initPosition = new List<Vector3>();

        for (int i = 0; i < joints.Count; i++)
        {
            initPosition.Add(joints[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if (countIterations < maxIterations && Vector3.Distance(joints[joints.Count - 1].position, target.position) > tolerance)
            {
                PerformFABRIK();
                countIterations++;
            }
            else if (Vector3.Distance(joints[joints.Count - 1].position, target.position) < tolerance)
            {
                FindAnyObjectByType<DroneMovement>().SetCanMove(false);
                FindAnyObjectByType<DroneMovement>().transform.SetParent(joints.Last());
                canMove = false;
            }
        }
        else
        {
            ReturnToOriginalPositions();
        }
    }

    void ReturnToOriginalPositions()
    {
        for (int i = 0; i < joints.Count; i++)
        {
            joints[i].position = Vector3.MoveTowards(joints[i].position, initPosition[i], moveSpeed / 4 * Time.deltaTime);
        }
    }

    void PerformFABRIK()
    {
        Forward();
        Backward();
        AlignLastJoint();
    }

    void Forward()
    {
        joints[joints.Count - 1].position = Vector3.MoveTowards(joints[joints.Count - 1].position, target.position, moveSpeed * Time.deltaTime);

        for (int i = joints.Count - 2; i >= 0; i--)
        {
            float distance = Vector3.Magnitude(Links[i]);
            float denominator = Vector3.Distance(joints[i].position, joints[i + 1].position);
            lambda = distance / denominator;
            Vector3 temp = lambda * joints[i].position + (1 - lambda) * joints[i + 1].position;
            joints[i].position = temp;
        }
    }

    void Backward()
    {
        joints[0].position = initPosition[0];

        for (int i = 1; i < joints.Count; i++)
        {
            float distance = Vector3.Magnitude(Links[i - 1]);
            float denominator = Vector3.Distance(joints[i - 1].position, joints[i].position);
            lambda = distance / denominator;
            Vector3 temp = lambda * joints[i].position + (1 - lambda) * joints[i - 1].position;
            joints[i].position = temp;
        }
    }

    void AlignLastJoint()
    {
        Vector3 directionToTarget = target.position - joints[joints.Count - 1].position;
        Quaternion desiredRotation = Quaternion.LookRotation(-directionToTarget);

        joints[joints.Count - 1].rotation = Quaternion.RotateTowards(
            joints[joints.Count - 1].rotation,
            desiredRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
