using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

/*
    1. 주변환경을 관측(Observations)
    2. 정책에 의한 행동(Actions)
    3. 행동에 따른 보상(Reward)
*/

public class MummyAgent : Agent
{
    // 관측 종류
    /*
        - Rigidbody Velocity
        - 타겟과의 거리
        - 자신의 위치
    */

    private Transform tr;
    private Rigidbody rb;
    public Transform targetTr;

    // 초기화 작업시 호출되는 메소드
    public override void Initialize()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // 학습(Episode)이 시작될때 마다 호출
    public override void OnEpisodeBegin()
    {
        // 물리엔진의 초기화
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 에이전트의 위치를 불규칙하게 변경
        tr.localPosition = new Vector3(Random.Range(-4.0f, +4.0f)
                                        , 0.0f
                                        , Random.Range(-4.0f, +4.0f));

        // 타겟의 위치를 불규칙하게 변경
        targetTr.localPosition = new Vector3(Random.Range(-4.0f, +4.0f)
                                        , 0.5f
                                        , Random.Range(-4.0f, +4.0f));

    }

    // 주변환경을 관측 데이터를 브레인에게 전송
    public override void CollectObservations(VectorSensor sensor)
    {
        // 타겟의 위치를 관측
        sensor.AddObservation(targetTr.position); //x,y,z --> 3개
        // 자신의 위치를 관측
        sensor.AddObservation(tr.localPosition);  //x,y, z --> 3개
        // 속도 관측
        sensor.AddObservation(rb.velocity.x);     // 1개
        sensor.AddObservation(rb.velocity.z);     // 1개
    }

    // 브레인으로 부터 명령을 전달 받을때 마다 호출
    public override void OnActionReceived(ActionBuffers actions)
    {
        /*
            연속(Continues)  -1.0f ~ 0.0f ~ +1.0f
            이산(Discrete)   -1, 0, +1
        */


        var action = actions.ContinuousActions;
        Debug.Log($"[0]={action[0]}, [1]={action[1]}");



    }

    // 개발자 테스트용 입력
    public override void Heuristic(in ActionBuffers actionsOut)
    {

    }
}
