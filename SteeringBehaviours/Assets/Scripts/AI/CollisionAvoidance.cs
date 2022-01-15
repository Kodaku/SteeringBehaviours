using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    public CharacterController controller;
    public CharacterController[] targets;
    public float maxAcceleration;
    public float radius;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steeringOutput = GetSteering();

        controller.Move(-steeringOutput.linearAcceleration * Time.deltaTime);
    }

    private SteeringOutput GetSteering()
    {
        SteeringOutput steeringOutput = new SteeringOutput();

        float shortestTime = Mathf.Infinity;

        CharacterController firstTarget = null;
        float firstMinSeparation = 0.0f;
        float firstDistance = 0.0f;
        Vector3 firstRelativePos = Vector3.zero;
        Vector3 firstRelativeVel = Vector3.zero;

        foreach(CharacterController target in targets)
        {
            Vector3 relativePos = target.transform.position - transform.position;
            Vector3 relativeVel = controller.velocity - target.velocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);
            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;

            if(minSeparation > 2 * radius)
            {
                continue;
            }

            if(timeToCollision > 0.0f && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        if(!firstTarget)
        {
            return steeringOutput;
        }

        Vector3 realtivePos = Vector3.zero;

        if(firstMinSeparation <= 0.0f || firstDistance < 2 * radius)
        {
            realtivePos = firstTarget.transform.position - transform.position;
        }
        else
        {
            realtivePos = firstRelativePos + firstRelativeVel * shortestTime;
        }

        realtivePos = realtivePos.normalized;

        steeringOutput.linearAcceleration = realtivePos * maxAcceleration;
        steeringOutput.angularAcceleration = 0.0f;

        if(controller.velocity.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(controller.velocity.x, controller.velocity.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            // steeringOutput.linearAcceleration = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            // steeringOutput.linearAcceleration *= maxAcceleration;
        }

        return steeringOutput;
    }
}
