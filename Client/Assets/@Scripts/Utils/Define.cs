using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const char MAP_TOOL_WALL = '0';
    public const char MAP_TOOL_NONE = '1';

    public const int HERO_DEFAULT_MOVE_DEPTH = 8;
    public enum EScene
    {
        Unknown,
        TitleScene,
        GameScene,
    }

    public enum ETouchEvent
    {
        PointerUp,
        PointerDown,
        Click,
        Pressed,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum ELayer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Dummy1 = 3,
        Water = 4,
        UI = 5,
        Hero = 6,
        Monster = 7,
        Boss = 8,
        //
        Env = 11,
        Obstacle = 12,
        //
        Projectile = 20,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}
