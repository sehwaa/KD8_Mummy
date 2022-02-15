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

    }

    // 학습(Episode)이 시작될때 마다 호출
    public override void OnEpisodeBegin()
    {

    }

    // 주변환경을 관측 데이터를 브레인에게 전송
    public override void CollectObservations(VectorSensor sensor)
    {

    }

    // 브레인으로 부터 명령을 전달 받을때 마다 호출
    public override void OnActionReceived(ActionBuffers actions)
    {

    }

    // 개발자 테스트용 입력
    public override void Heuristic(in ActionBuffers actionsOut)
    {

    }
}
