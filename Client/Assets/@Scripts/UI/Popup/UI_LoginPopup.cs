using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_LoginPopup : UI_Popup
{
    enum EGameObjects
    {
        LoginButton
    }

    enum ETexts
    {
        ID,
        PW
    }

    Action<bool> _onClosePopup;

    protected override void Awake()
    {
        base.Awake();

        BindObjects(typeof(EGameObjects));
        BindObjects(typeof(ETexts));

        //TODO : ���������� �α���

        GetObject((int)EGameObjects.LoginButton).BindEvent(OnClickLoginButton);
    }

    public void SetInfo(Action<bool> action)
    {
        _onClosePopup = action; 
    }

    void OnClickLoginButton(PointerEventData evt)
    {
        // 1) TODO : ���������� ���� ��û
        // 2) TDOO : ���������� DB ID���� JWT �޾Ƽ� ���Ӽ����� ���� �õ�

        _onClosePopup?.Invoke(true);
        ClosePopupUI();
    }
}
