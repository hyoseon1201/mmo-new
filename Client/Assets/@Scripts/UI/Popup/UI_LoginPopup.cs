using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_LoginPopup : UI_Popup
{
    private enum EGameObjects
    {
        LoginButton,
        ID,
        PW
    }

    private enum ETexts
    {
        IDText,
        PWText
    }

    Action<bool> _onClosePopup;

    protected override void Awake()
    {
        base.Awake();

        BindObjects(typeof(EGameObjects));
        BindTexts(typeof(ETexts));

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

        // TEMP (���������ߴٰ� �����ϰ� AccountDbId�� jwt �Ѱ���)
        Managers.AccountDbId = 0;
        Managers.AccessToken = "";

        _onClosePopup?.Invoke(true);
        ClosePopupUI();
    }
}
