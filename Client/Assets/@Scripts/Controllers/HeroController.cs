using Google.Protobuf.Protocol;
using System.Collections;
using UnityEngine;

public class HeroController : CreatureController
{
    //protected bool _rangedSkill = false;

    //protected override void Init()
    //{
    //    base.Init();
    //}

    //protected override void UpdateAnimation()
    //{
    //    if (State == CreatureState.Idle)
    //    {
    //        switch (_lastDir)
    //        {
    //            case MoveDir.Up:
    //                _animator.Play("IDLE_BACK");
    //                _sprite.flipX = false;
    //                break;
    //            case MoveDir.Down:
    //                _animator.Play("IDLE_FRONT");
    //                _sprite.flipX = false;
    //                break;
    //            case MoveDir.Right:
    //                _animator.Play("IDLE_RIGHT");
    //                _sprite.flipX = false;
    //                break;
    //            case MoveDir.Left:
    //                _animator.Play("IDLE_RIGHT");
    //                _sprite.flipX = true;
    //                break;
    //        }
    //    }
    //    else if (State == CreatureState.Moving)
    //    {
    //        switch (Dir)
    //        {
    //            case MoveDir.Up:
    //                _animator.Play("WALK_BACK");
    //                _sprite.flipX = false;
    //                break;
    //            case MoveDir.Down:
    //                _animator.Play("WALK_FRONT");
    //                _sprite.flipX = false;
    //                break;
    //            case MoveDir.Right:
    //                _animator.Play("WALK_RIGHT");
    //                _sprite.flipX = false;
    //                break;
    //            case MoveDir.Left:
    //                _animator.Play("WALK_RIGHT");
    //                _sprite.flipX = true;
    //                break;
    //            case MoveDir.None:
    //                break;
    //        }
    //    }
    //    else if (State == CreatureState.Skill)
    //    {
    //        switch (_lastDir)
    //        {
    //            case MoveDir.Up:
    //                _animator.Play(_rangedSkill ? "ATTACK_WEAPON_BACK" : "ATTACK_WEAPON_BACK");
    //                _sprite.flipX = false;
    //                break;
    //            case MoveDir.Down:
    //                _animator.Play(_rangedSkill ? "ATTACK_WEAPON_FRONT" : "ATTACK_WEAPON_FRONT");
    //                _sprite.flipX = false;
    //                break;
    //            case MoveDir.Right:
    //                _animator.Play(_rangedSkill ? "ATTACK_WEAPON_RIGHT" : "ATTACK_WEAPON_RIGHT");
    //                _sprite.flipX = false;
    //                break;
    //            case MoveDir.Left:
    //                _animator.Play(_rangedSkill ? "ATTACK_WEAPON_RIGHT" : "ATTACK_WEAPON_RIGHT");
    //                _sprite.flipX = true;
    //                break;
    //            case MoveDir.None:
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        // TODO dead
    //    }
    //}

    //protected override void UpdateController()
    //{
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
    //}

    //public override void OnDamaged()
    //{
    //    Debug.Log("Player Hit!!");
    //}
}
