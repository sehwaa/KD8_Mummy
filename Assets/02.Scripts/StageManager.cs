using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject goodItem;
    public GameObject badItem;

    [Range(10, 50)]
    public float goodItemCount = 30;

    [Range(10, 50)]
    public float badItemCount = 20;

    public void InitStage()
    {
        // 기존에 생성됐던 아이템 삭제

        // GoodItem 생성
        for (int i = 0; i < goodItemCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-22.0f, 22.0f)
                                    , 0.0f
                                    , Random.Range(-22.0f, 22.0f));
        }

        // BadItem 생성
    }


    // Start is called before the first frame update
    void Start()
    {

    }

}
