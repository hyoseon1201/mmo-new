using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public int ObjectId { get; set; }

    public float _speed = 5.0f;

    protected bool _updated = false;

    PositionInfo _positionInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _positionInfo; }
        set
        {
            if (_positionInfo.Equals(value))
                return;

            CellPos = new Vector3Int(value.PosX, value.PosY, 0);
            State = value.State;
            Dir = value.MoveDir;
        }
    }

    public Vector3Int CellPos
    {
        get
        {
            return new Vector3Int(PosInfo.PosX, PosInfo.PosY, 0);
        }

        set
        {
            if (PosInfo.PosX == value.x && PosInfo.PosY == value.y)
                return;

            PosInfo.PosX = value.x;
            PosInfo.PosY = value.y;
            _updated = true;
        }
    }

    protected Animator _animator;
    protected SpriteRenderer _sprite;

    public virtual EObjectState State
    {
        get { return PosInfo.State; }
        set
        {
            if (PosInfo.State == value)
                return;

            PosInfo.State = value;
            UpdateAnimation();
            _updated = true;
        }
    }

    protected EMoveDir _lastDir = EMoveDir.Down;
    public EMoveDir Dir
    {
        get { return PosInfo.MoveDir; }
        set
        {
            if (PosInfo.MoveDir == value)
                return;

            PosInfo.MoveDir = value;
            if (value != EMoveDir.None)
                _lastDir = value;

            UpdateAnimation();
            _updated = true;
        }
    }

    public EMoveDir GetDirFromVec(Vector3Int dir)
    {
        if (dir.x > 0)
            return EMoveDir.Right;
        else if (dir.x < 0)
            return EMoveDir.Left;
        else if (dir.y > 0)
            return EMoveDir.Up;
        else if (dir.y < 0)
            return EMoveDir.Down;
        else
            return EMoveDir.None;
    }

    public Vector3Int GetFrontCellPos()
    {
        Vector3Int cellPos = CellPos;

        switch (_lastDir)
        {
            case EMoveDir.Up:
                cellPos += Vector3Int.up;
                break;
            case EMoveDir.Down:
                cellPos += Vector3Int.down;
                break;
            case EMoveDir.Left:
                cellPos += Vector3Int.left;
                break;
            case EMoveDir.Right:
                cellPos += Vector3Int.right;
                break;
        }

        return cellPos;
    }

    protected virtual void UpdateAnimation()
    {
        if (State == EObjectState.Idle)
        {
            switch (_lastDir)
            {
                case EMoveDir.Up:
                    _animator.Play("IDLE_BACK");
                    _sprite.flipX = false;
                    break;
                case EMoveDir.Down:
                    _animator.Play("IDLE_FRONT");
                    _sprite.flipX = false;
                    break;
                case EMoveDir.Left:
                    _animator.Play("IDLE_RIGHT");
                    _sprite.flipX = true;
                    break;
                case EMoveDir.Right:
                    _animator.Play("IDLE_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else if (State == EObjectState.Move)
        {
            switch (Dir)
            {
                case EMoveDir.Up:
                    _animator.Play("WALK_BACK");
                    _sprite.flipX = false;
                    break;
                case EMoveDir.Down:
                    _animator.Play("WALK_FRONT");
                    _sprite.flipX = false;
                    break;
                case EMoveDir.Left:
                    _animator.Play("WALK_RIGHT");
                    _sprite.flipX = true;
                    break;
                case EMoveDir.Right:
                    _animator.Play("WALK_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else if (State == EObjectState.Skill)
        {
            switch (_lastDir)
            {
                case EMoveDir.Up:
                    _animator.Play("ATTACK_WEAPON_BACK");
                    _sprite.flipX = false;
                    break;
                case EMoveDir.Down:
                    _animator.Play("ATTACK_WEAPON_FRONT");
                    _sprite.flipX = false;
                    break;
                case EMoveDir.Left:
                    _animator.Play("ATTACK_WEAPON_RIGHT");
                    _sprite.flipX = true;
                    break;
                case EMoveDir.Right:
                    _animator.Play("ATTACK_WEAPON_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else
        {

        }
    }

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        Vector3 pos = Managers.Map.Cell2World(CellPos) + new Vector3(0.5f, 0.5f);
        transform.position = pos;

        State = EObjectState.Idle;
        Dir = EMoveDir.None;
        UpdateAnimation();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        UpdateController();
    }

    protected virtual void UpdateController()
    {
        switch (State)
        {
            case EObjectState.Idle:
                UpdateIdle();
                break;
            case EObjectState.Move:
                UpdateMoving();
                break;
            case EObjectState.Skill:
                UpdateSkill();
                break;
            case EObjectState.Dead:
                UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle()
    {
    }

    // 스르륵 이동하는 것을 처리
    protected virtual void UpdateMoving()
    {
        Vector3 destPos = Managers.Map.Cell2World(CellPos) + new Vector3(0.5f, 0.5f);
        Vector3 moveDir = destPos - transform.position;

        // 도착 여부 체크
        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            MoveToNextPos();
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            State = EObjectState.Move;
        }
    }

    protected virtual void MoveToNextPos()
    {

    }

    protected virtual void UpdateSkill()
    {

    }

    protected virtual void UpdateDead()
    {

    }

    public virtual void OnDamaged()
    {

    }

    public void SetCellPos(Vector3Int cellPos, bool forceMove = false)
    {
        CellPos = cellPos;
        _updated = false;

        if (forceMove)
        {
            transform.position = Managers.Map.Cell2World(CellPos);
            _updated = true;
        }
    }

    public void SyncWorldPosWithCellPos()
    {
        Managers.Map.MoveTo(this, CellPos, forceMove: true);
    }
}
