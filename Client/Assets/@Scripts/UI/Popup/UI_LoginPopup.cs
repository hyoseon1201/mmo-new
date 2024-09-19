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

        //TODO : 인증서버로 로그인

        GetObject((int)EGameObjects.LoginButton).BindEvent(OnClickLoginButton);
    }

    public void SetInfo(Action<bool> action)
    {
        _onClosePopup = action; 
    }

    void OnClickLoginButton(PointerEventData evt)
    {
        // 1) TODO : 인증서버로 인증 요청
        // 2) TDOO : 인증성공시 DB ID값과 JWT 받아서 게임서버에 연결 시도

        // TEMP (인증성공했다고 가정하고 AccountDbId와 jwt 넘겨줌)
        Managers.AccountDbId = 0;
        Managers.AccessToken = "";

        _onClosePopup?.Invoke(true);
        ClosePopupUI();
    }
}
