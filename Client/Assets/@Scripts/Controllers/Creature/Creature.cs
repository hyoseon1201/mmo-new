using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : BaseObject
{
    [SerializeField]
    protected EObjectState _objectState = EObjectState.None;
    public virtual EObjectState ObjectState
    {
        get { return PosInfo.State; }
        set
        {
            if (_objectState == value)
                return;

            _objectState = value;
            PosInfo.State = value;
            UpdateAnimation();
        }
    }

    [SerializeField]
    protected EMoveDir _dir = EMoveDir.None;
    public EMoveDir Dir
    {
        get { return PosInfo.MoveDir; }
        set
        {
            if (_dir == value)
                return;

            _dir = value;
            PosInfo.MoveDir = value;
            UpdateAnimation();
        }
    }

    protected Animator _animator;

    // TODO
    public float _moveSpeed = 5.0f;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    #region FSM
    protected virtual void UpdateAI()
    {
        switch (ObjectState)
        {
            case EObjectState.Idle:
                UpdateIdle();
                break;
            case EObjectState.Move:
                UpdateMove();
                break;
            case EObjectState.Skill:
                UpdateSkill();
                break;
            case EObjectState.Dead:
                UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle() { }

    protected virtual void UpdateMove() { }

    protected virtual void UpdateSkill() { }

    protected virtual void UpdateDead() { }
    #endregion

    #region Animation

    protected virtual void UpdateAnimation()
    {
        Debug.Log("»£√‚" + Dir);
        switch (ObjectState)
        {
            case EObjectState.Idle:
                if (Dir == EMoveDir.Up)
                {
                    _animator.Play("IDLE_BACK");
                    _sprite.flipX = false;
                }
                else if (Dir == EMoveDir.Down)
                {
                    _animator.Play("IDLE_FRONT");
                    _sprite.flipX = false;
                }
                else if (Dir == EMoveDir.Right)
                {
                    _animator.Play("IDLE_RIGHT");
                    _sprite.flipX = false;
                }
                else if (Dir == EMoveDir.Left)
                {
                    _animator.Play("IDLE_RIGHT");
                    _sprite.flipX = true;
                }
                break;
            case EObjectState.Move:
                if (Dir == EMoveDir.Up)
                {
                    _animator.Play("WALK_BACK");
                    _sprite.flipX = false;
                }
                else if (Dir == EMoveDir.Down)
                {
                    _animator.Play("WALK_FRONT");
                    _sprite.flipX = false;
                }
                else if (Dir == EMoveDir.Right)
                {
                    _animator.Play("WALK_RIGHT");
                    _sprite.flipX = false;
                }
                else if (Dir == EMoveDir.Left)
                {
                    _animator.Play("WALK_RIGHT");
                    _sprite.flipX = true;
                }
                break;
            case EObjectState.Skill:
                if (Dir == EMoveDir.Up)
                {
                    _animator.Play("ATTACK_WEAPON_BACK");
                    _sprite.flipX = false;
                }
                else if (Dir == EMoveDir.Down)
                {
                    _animator.Play("ATTACk_WEAPON_FRONT");
                    _sprite.flipX = false;
                }
                else if (Dir == EMoveDir.Right)
                {
                    _animator.Play("ATTACk_WEAPON_RIGHT");
                    _sprite.flipX = false;
                }
                else if (Dir == EMoveDir.Left)
                {
                    _animator.Play("ATTACk_WEAPON_RIGHT");
                    _sprite.flipX = true;
                }
                break;
            case EObjectState.Dead:
                //TODO
                break;
        }
    }

    #endregion

    #region Map

    public EFindPathResult FindPathToCellPos(Vector3 destWorldPos, int maxDepth, out List<Vector3Int> path, bool forceMoveCloser = false)
    {
        Vector3Int destCellPos = Managers.Map.World2Cell(destWorldPos);
        return FindPathToCellPos(destCellPos, maxDepth, out path, forceMoveCloser);
    }

    public EFindPathResult FindPathToCellPos(Vector3Int destCellPos, int maxDepth, out List<Vector3Int> path, bool forceMoveCloser = false)
    {
        path = new List<Vector3Int>();

        if (CellPos == destCellPos)
            return EFindPathResult.FailSamePosition;

        if (LerpCellPosCompleted == false)
            return EFindPathResult.FailLerpcell;

        // A*
        path = Managers.Map.FindPath(this, CellPos, destCellPos, maxDepth);
        if (path.Count < 2)
            return EFindPathResult.FailNoPath;

        if (forceMoveCloser)
        {
            Vector3Int diff1 = CellPos - destCellPos;
            Vector3Int diff2 = path[1] - destCellPos;
            if (diff1.sqrMagnitude <= diff2.sqrMagnitude)
                return EFindPathResult.FailNoPath;
        }

        Vector3Int dirCellPos = path[1] - CellPos;
        Vector3Int nextPos = CellPos + dirCellPos;

        if (Managers.Map.MoveTo(this, nextPos) == false)
            return EFindPathResult.FailMoveTo;

        return EFindPathResult.Success;
    }

    public bool MoveToCellPos(Vector3Int destCellPos, int maxDepth = 2, bool forceMoveCloser = false)
    {
        if (LerpCellPosCompleted == false)
            return false;

        return Managers.Map.MoveTo(this, destCellPos);
    }

    #endregion

   
}
