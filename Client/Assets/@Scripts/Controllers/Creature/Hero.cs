using Data;
using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // FSM 상태관리
        UpdateAI();

        UpdateLerpToCellPos(_moveSpeed);
    }

    protected override void UpdateMove()
    {
        base.UpdateMove();

        if (LerpCellPosCompleted)
        {
            ObjectState = EObjectState.Idle;
            return;
        }
    }

    public virtual void SetInfo(int templateId)
    {
        if (Managers.Data.HeroDic.TryGetValue(templateId, out HeroData heroData) == false)
            return;
    }
}
