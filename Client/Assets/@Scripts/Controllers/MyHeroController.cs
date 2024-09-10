using Google.Protobuf.Protocol;
using UnityEngine;

public class MyHeroController : HeroController
{
    //protected override void Init()
    //{
    //    base.Init();
    //}

    //protected override void UpdateController()
    //{
    //    switch (State)
    //    {
    //        case CreatureState.Idle:
    //            GetDirInput();
    //            break;
    //        case CreatureState.Moving:
    //            GetDirInput();
    //            break;
    //    }
    //    base.UpdateController();
    //}

    //protected override void UpdateIdle()
    //{
    //    // 이동상태로 갈지 확인
    //    if (Dir != MoveDir.None)
    //    {
    //        State = CreatureState.Moving;
    //        return;
    //    }

    //    // 스킬 상태로 갈지 확인
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        State = CreatureState.Skill;
    //        //_coSkill = StartCoroutine("CoStartSword");
    //    }
    //}

    //protected void GetDirInput()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        //transform.position += Vector3.up * Time.deltaTime * _speed;
    //        Dir = MoveDir.Up;
    //    }
    //    else if (Input.GetKey(KeyCode.S))
    //    {
    //        //transform.position += Vector3.down * Time.deltaTime * _speed;
    //        Dir = MoveDir.Down;
    //    }
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        //transform.position += Vector3.right * Time.deltaTime * _speed;
    //        Dir = MoveDir.Right;
    //    }
    //    else if (Input.GetKey(KeyCode.A))
    //    {
    //        //transform.position += Vector3.left * Time.deltaTime * _speed;
    //        Dir = MoveDir.Left;
    //    }
    //    else
    //    {
    //        Dir = MoveDir.None;
    //    }
    //}

    //void LateUpdate()
    //{
    //    Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    //}

    //protected override void MoveToNextPos()
    //{
    //    if (Dir == MoveDir.None)
    //    {
    //        State = CreatureState.Idle;
    //        CheckUpdatedFlag();
    //        return;
    //    }

    //    Vector3Int destPos = CellPos;

    //    switch (Dir)
    //    {
    //        case MoveDir.Up:
    //            destPos += Vector3Int.up;
    //            break;
    //        case MoveDir.Down:
    //            destPos += Vector3Int.down;
    //            break;
    //        case MoveDir.Left:
    //            destPos += Vector3Int.left;
    //            break;
    //        case MoveDir.Right:
    //            destPos += Vector3Int.right;
    //            break;
    //    }

    //    if (Managers.Map.CanGo(destPos))
    //    {
    //        if (Managers.Object.Find(destPos) == null)
    //        {
    //            CellPos = destPos;
    //        }
    //    }

    //    CheckUpdatedFlag();
    //}

    //void CheckUpdatedFlag()
    //{
    //    if (_updated)
    //    {
    //        C_Move movePacket = new C_Move();
    //        movePacket.PosInfo = PosInfo;
    //        Managers.Network.Send(movePacket);
    //        _updated = false;
    //    }
    //}
}
