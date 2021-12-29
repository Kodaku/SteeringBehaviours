using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{
    public CharacterController targetController;
    public float maxPrediction;
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

    public override SteeringOutput GetSteering()
    {
        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;

        float speed = controller.velocity.magnitude;
        float prediction = 0.0f;
        if(speed < distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }

        target.transform.position += targetController.velocity * prediction;

        return base.GetSteering();
    }
}
