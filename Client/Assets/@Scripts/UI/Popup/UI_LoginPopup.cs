using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using WebPacket;

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

        GetObject((int)EGameObjects.LoginButton).BindEvent(OnClickLoginButton);
    }

    public void SetInfo(Action<bool> action)
    {
        _onClosePopup = action; 
    }

    void OnClickLoginButton(PointerEventData evt)
    {
        string username = GetObject((int)EGameObjects.ID).GetComponentInChildren<TMP_InputField>().text;
        string password = GetObject((int)EGameObjects.PW).GetComponentInChildren<TMP_InputField>().text;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        Managers.Web.SendPostRequestForm<LoginResponse>("api/account/login", form, OnLoginRequestComplete);
    }

    void OnLoginRequestComplete(LoginResponse response)
    {
        Managers.AccountDbId = response.accountDbId;
        Managers.AccessToken = response.accessToken;
        Managers.RefreshToken = response.refreshToken;

        Debug.Log(response);

        if (response.success)
        {
            _onClosePopup?.Invoke(true);
            ClosePopupUI();
        }
        else
        {
            _onClosePopup?.Invoke(false);
        }
    }
}
