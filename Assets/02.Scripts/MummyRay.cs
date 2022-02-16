using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MummyRay : Agent
{
    private Transform tr;
    private Rigidbody rb;
    private StageManager stageManager;
    public float moveSpeed = 1.5f;      // 이동 속도
    public float turnSpeed = 200.0f;    // 회전 속도

    public override void Initialize()
    {
        MaxStep = 100;
        stageManager = transform.root.GetComponent<StageManager>();

        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // 스테이지 초기화 메소드 호출
        stageManager.InitStage();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
    }
}
