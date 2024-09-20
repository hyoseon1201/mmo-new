using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SubItem : UI_Base
{
    [SerializeField]
    protected ScrollRect _parentScrollRect;

    protected override void Awake()
    {
        base.Awake();

        _parentScrollRect = Utils.FindAncestor<ScrollRect>(gameObject);
    }
}
