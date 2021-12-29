using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatch : MonoBehaviour
{
    public CharacterController controller;
    public CharacterController targetController;
    public float maxAcceleration;
    private float timeToTarget = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steeringOutput = GetSteering();

        controller.Move(steeringOutput.linearAcceleration * Time.deltaTime);
    }

    private SteeringOutput GetSteering()
    {
        SteeringOutput steeringOutput = new SteeringOutput();

        if(targetController.velocity.magnitude <= 0.1f)
        {
            steeringOutput.linearAcceleration = Vector3.zero;
            return steeringOutput;
        }

        steeringOutput.linearAcceleration = targetController.velocity - controller.velocity;

        steeringOutput.linearAcceleration /= timeToTarget;

        if(steeringOutput.linearAcceleration.magnitude > maxAcceleration)
        {
            steeringOutput.linearAcceleration = steeringOutput.linearAcceleration.normalized * maxAcceleration;
        }

        steeringOutput.angularAcceleration = 0.0f;

        return steeringOutput;
    }
}
