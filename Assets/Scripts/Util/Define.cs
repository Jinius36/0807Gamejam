using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
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
        MainScene,
        GameScene,
        StageSelect
    }
    public enum Sound
    {
        Master,
        BGM,
        SFX,
        MaxCount
    }
}