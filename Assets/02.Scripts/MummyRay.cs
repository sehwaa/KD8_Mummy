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
        MaxStep = 5000;
        stageManager = transform.root.GetComponent<StageManager>();

        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // 스테이지 초기화 메소드 호출
        stageManager.InitStage();

        // 물리엔진 초기화
        rb.velocity = rb.angularVelocity = Vector3.zero;
        // 에이전트의 위치를 불규칙하게 변경
        tr.localPosition = new Vector3(Random.Range(-20.0f, 20.0f)
                                    , 0.0f
                                    , Random.Range(-20.0f, 20.0f));

        tr.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.DiscreteActions;
        //Debug.Log($"[0]={action[0]}, [1]={action[1]}");

        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;

        // Branch 0 => action[0]
        switch (action[0])
        {
            case 1: dir = tr.forward; break;
            case 2: dir = -tr.forward; break;
        }
        // Branch 1 => action[1]
        switch (action[1])
        {
            case 1: rot = -tr.up; break;
            case 2: rot = tr.up; break;
        }

        tr.Rotate(rot, Time.fixedDeltaTime * turnSpeed);
        rb.AddForce(dir * moveSpeed, ForceMode.VelocityChange);

        // 움직임 유도를 위한 마이너스 페널티
        AddReward(-1 / (float)MaxStep); // -1/5000 = -0.005f
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.DiscreteActions; //Discreate(0, 1, 2, 3, ...)
        actions.Clear();

        // 전진/후진 이동 - Branch 0 = (0:정지, 1:전진, 2:후진) size = 3
        if (Input.GetKey(KeyCode.W))
        {
            actions[0] = 1; // 전진
        }
        if (Input.GetKey(KeyCode.S))
        {
            actions[0] = 2; // 후진
        }

        // 좌/우 회전 - Branch 1 = (0:무회전, 1:왼쪽회전, 2:오른쪽회전) size = 3
        if (Input.GetKey(KeyCode.A))
        {
            actions[1] = 1; // 왼쪽으로 회전
        }
        if (Input.GetKey(KeyCode.D))
        {
            actions[1] = 2; // 오른쪽 회전
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("GOOD_ITEM"))
        {
            AddReward(+1.0f);
            rb.velocity = rb.angularVelocity = Vector3.zero;
            Destroy(coll.gameObject);
        }

        if (coll.collider.CompareTag("BAD_ITEM"))
        {
            AddReward(-1.0f);
            EndEpisode();
        }

        if (coll.collider.CompareTag("DEADZONE"))
        {
            AddReward(-0.01f);
        }
    }
}
