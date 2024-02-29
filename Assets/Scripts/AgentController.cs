using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents; //need this
using Unity.MLAgents.Sensors; //need this
using Unity.MLAgents.Actuators; //need this

public class AgentController : Agent
{
    [SerializeField] private Transform target;
    private Vector3 startPosition = new Vector3(0, 1, 0);
    public float moveSpeed;

    private Vector3 lastPosition;

    private void Update()
    {
        //if(lastPosition == transform.position)
        //{
        //    AddReward(-1);
        //}
        //lastPosition = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        //set a random position for the target
        transform.localPosition = startPosition;

        int rand = Random.Range(0, 2);
        
        if(rand == 0)
        {
            target.localPosition = new Vector3(-4.0f, 1f, 0f);
        }
        if(rand == 1)
        {
            target.localPosition = new Vector3(4f, 1f, 0f);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //do something with actions
        float move = actions.ContinuousActions[0];
        transform.localPosition += new Vector3(move, 0f) * moveSpeed * Time.deltaTime;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //lets agent know where it is
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            AddReward(2);
            EndEpisode();
        }   
        if (other.gameObject.tag == "Wall")
        {
            AddReward(-1);
            EndEpisode();
        }
    }
}
