using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    protected override void Awake()
    {
        base.Awake();

        Managers.UI.SetCanvas(gameObject, false);
    }
}
