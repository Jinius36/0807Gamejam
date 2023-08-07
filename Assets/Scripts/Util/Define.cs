using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Enemy,
    }
    public enum State
    {
        Idle,
        Walk,
        Run
    }
    public enum UIEvent
    {
        Click,
        BeginDrag,
        Drag,
        DragEnd,
        Slider
    }
    public enum MouseEvent
    {
        Press,
        Click
    }

    public enum Scene
    {
        Unknown,
        StartScene,
        GameScene1,
        GameScene2,
        GameScene3
    }
    public enum Sound
    {
        Master,
        BGM,
        SFX,
        MaxCount
    }
}