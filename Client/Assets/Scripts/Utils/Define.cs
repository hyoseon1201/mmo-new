using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const char MAP_TOOL_WALL = '0';
    public const char MAP_TOOL_NONE = '1';

    public enum CreatureState
    {
        Idle,
        Moving,
        Skill,
        Dead
    }

    public enum MoveDir
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        SubBgm,
        Effect,
        Max,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}
