using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public int ObjectId { get; private set; }
    public virtual EGameObjectType ObjectType { get { return EGameObjectType.None; } }
    protected SpriteRenderer _sprite;

    public int ExtraCells = 0;

    PositionInfo _positionInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _positionInfo; }
        set
        {
            if (_positionInfo.Equals(value))
                return;

            CellPos = new Vector3Int(value.PosX, value.PosY, 0);
        }
    }

    protected virtual void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {

    }

    #region Map
    public bool LerpCellPosCompleted { get; protected set; }

    [SerializeField] Vector3Int _cellPos;
    public Vector3Int CellPos
    {
        get { return _cellPos; }
        protected set
        {
            _cellPos = value;
            LerpCellPosCompleted = false;
        }
    }

    public void SetCellPos(Vector3Int cellPos, bool forceMove = false)
    {
        CellPos = cellPos;
        LerpCellPosCompleted = false;

        if (forceMove)
        {
            transform.position = Managers.Map.Cell2World(CellPos);
            LerpCellPosCompleted = true;
        }
    }

    public void UpdateLerpToCellPos(float moveSpeed)
    {
        if (LerpCellPosCompleted)
            return;

        Vector3 destPos = Managers.Map.Cell2World(CellPos);
        Vector3 dir = destPos - transform.position;

        float moveDist = moveSpeed * Time.deltaTime;
        if (dir.magnitude < moveDist)
        {
            // 다 이동 했으면 맵 그리드 갱신
            SyncWorldPosWithCellPos();
            transform.position = destPos;
            LerpCellPosCompleted = true;
            return;
        }

        transform.position += dir.normalized * moveDist;
    }

    public void SyncWorldPosWithCellPos()
    {
        Managers.Map.MoveTo(this, CellPos, forceMove: true);
    }
    #endregion
}
