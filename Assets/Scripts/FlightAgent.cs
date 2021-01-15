using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class FlightAgent : Agent
{
    //bool isCrashed;
    Rigidbody rBody;
    PlaygroundController playgroundController;
    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        playgroundController = GameObject.Find("PlaygroundController").GetComponent<PlaygroundController>();
    }


    public Transform Target;
    public override void OnEpisodeBegin()
    {
        //Reset the airplane's transform and velocity
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = new Vector3(0, 0, 1.0f);
        this.transform.localPosition = new Vector3(0, 1.0f, 0);
        this.transform.localEulerAngles = new Vector3(0, 0, 0);

        //Reset the target's position
        playgroundController.ResetTarget();
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.y);
    }


    public float forceMultiplier = 10;
    public override void OnActionReceived(float[] vectorAction)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.y = vectorAction[1];
        rBody.AddForce(controlSignal * forceMultiplier);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell off platform
        if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}