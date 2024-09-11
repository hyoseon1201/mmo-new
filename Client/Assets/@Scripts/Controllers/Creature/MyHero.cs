using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyHero : Hero
{
    // VisionCells 범위그리기
    private Color _lineColor = Color.red;
    private float _lineWidth = 0.1f;
    private int _visionCells = 10;
    private LineRenderer _lineRenderer;

	// 이동패킷 더티플래그
	protected bool _sendMovePacket = false;

	Vector3? _desiredDestPos;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        _lineRenderer = gameObject.GetOrAddComponent<LineRenderer>();
        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = _lineColor;
        _lineRenderer.endColor = _lineColor;
        _lineRenderer.sortingOrder = 800;

        CameraController cc = Camera.main.GetOrAddComponent<CameraController>();
        if (cc != null)
            cc.Target = this;

        DrawCollision();
    }

    protected override void Update()
    {
		// 입력처리
		UpdateInput();

		// fsm 및 이동보정처리
        base.Update();
		
		// 희망 좌표 변경시 서버 전송
		UpdateSendMovePacket();

		// Debuging
		DrawVisionCells();
    }

    #region FSM

    protected override void UpdateIdle()
    {
        base.UpdateIdle();

		if (Dir != EMoveDir.None)
		{
			ObjectState = EObjectState.Move;
			return;
		}

		if (Input.GetKey(KeyCode.Space))
		{
			ObjectState = EObjectState.Skill;
		}
    }

    protected override void UpdateMove()
    {
        base.UpdateMove();
    }

    protected override void UpdateSkill()
    {
        base.UpdateSkill();
    }

    protected override void UpdateDead()
    {
        base.UpdateDead();
    }

    #endregion

    #region 이동 동기화

	void UpdateInput()
	{
        if (Input.GetKey(KeyCode.W))
        {
            Dir = EMoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = EMoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = EMoveDir.Right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Dir = EMoveDir.Left;
        }
        else
        {
            Dir = EMoveDir.None;
        }
    }



    void UpdateSendMovePacket()
	{

	}

    #endregion

    #region 디버깅
    void DrawVisionCells()
	{
		Vector3Int bottomLeft = CellPos + new Vector3Int(-_visionCells, -_visionCells, 0);
		Vector3Int bottomRight = CellPos + new Vector3Int(_visionCells, -_visionCells, 0);
		Vector3Int topLeft = CellPos + new Vector3Int(-_visionCells, _visionCells, 0);
		Vector3Int topRight = CellPos + new Vector3Int(_visionCells, _visionCells, 0);

		Vector3 worldBottomLeft = Managers.Map.Cell2World(bottomLeft);
		Vector3 worldBottomRight = Managers.Map.Cell2World(bottomRight);
		Vector3 worldTopLeft = Managers.Map.Cell2World(topLeft);
		Vector3 worldTopRight = Managers.Map.Cell2World(topRight);

		Vector3[] positions = new Vector3[5];
		positions[0] = worldBottomLeft;
		positions[1] = worldBottomRight;
		positions[2] = worldTopRight;
		positions[3] = worldTopLeft;
		positions[4] = worldBottomLeft; 

		_lineRenderer.positionCount = positions.Length;
		_lineRenderer.SetPositions(positions);
	}
    
    void DrawCollision()
    {
	    for (int y = Managers.Map.MinY; y <  Managers.Map.MaxY; y++) 
	    {
		    for (int x = Managers.Map.MinX; x <  Managers.Map.MaxX; x++)
		    {
			    DrawObject(new(x, y, 0));
		    }
	    }    
    }
    
    void DrawObject(Vector3Int tilePos)
    {
	    Vector3 worldPos = Managers.Map.Cell2World(tilePos);

	    if (Managers.Map.CanGo(this, tilePos))
	    {
	    }
	    else
	    {
		    GameObject parentObject = GameObject.Find("Test");
		    if (parentObject == null)
		    {
			    parentObject = new GameObject("Test");
		    }
		    
		    GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		    obj.transform.position = worldPos;
		    obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); // 크기 조정
		    obj.transform.SetParent(parentObject.transform); // 부모 설정

	    }
    }
	#endregion
}
