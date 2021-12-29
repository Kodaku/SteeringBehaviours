using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public CharacterController controller;
    public float wanderOffset;
    public float wanderRadius;
    public float wanderRate;
    public float maxAcceleration;
    private Vector3 target;
    private Vector3 nextDest;
    private float currentTimer;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = wanderRate;
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steeringOutput = GetSteering();

        // transform.rotation = Quaternion.Euler(0.0f, steeringOutput.angularAcceleration, 0.0f);
        controller.Move(steeringOutput.linearAcceleration * Time.deltaTime);
    }

    public SteeringOutput GetSteering()
    {
        currentTimer += Time.deltaTime;
        SteeringOutput steeringOutput = new SteeringOutput();
        steeringOutput.linearAcceleration = controller.velocity;
        steeringOutput.angularAcceleration = transform.eulerAngles.y;
        if(currentTimer > wanderRate)
        {
            currentTimer = 0.0f;
            target = transform.position;
            Vector3 nextDestCenter = wanderOffset * transform.forward;

            target += nextDestCenter;

            nextDest = target + wanderRadius * (new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, wanderRadius * Random.Range(-1.0f, 1.0f)));
        }

        steeringOutput.linearAcceleration = (nextDest - transform.position).normalized;

        if(steeringOutput.linearAcceleration.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(steeringOutput.linearAcceleration.x, steeringOutput.linearAcceleration.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            steeringOutput.linearAcceleration = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            steeringOutput.linearAcceleration *= maxAcceleration;
        }

        steeringOutput.angularAcceleration = 0.0f;

        return steeringOutput;
    }  

}
