using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class UI_TitleScene : UI_Scene
{
    private enum EGameObjects
    {
        ServerConnectButton,
        StartButton,
    }

    private enum ETexts
    {
        StatusText
    }

    private enum ETitleSceneState
    {
        None,
        // ���� �ε�
        AssetLoading,
        AssetLoaded,
        // ����
        LoginSuccess,
        LoginFailure,
        // ���� ����
        ConnectingToServer,
        ConnectedToServer,
        FailedToConnectToServer
    }

    ETitleSceneState _state = ETitleSceneState.None;
    ETitleSceneState State
    {
        get { return _state; }
        set
        {
            _state = value;
            
            switch (value)
            {
                case ETitleSceneState.None:
                    break;
                case ETitleSceneState.AssetLoading:
                    GetText((int)ETexts.StatusText).text = $"TODO �ε���";
                    break;
                case ETitleSceneState.AssetLoaded:
                    GetText((int)ETexts.StatusText).text = "TODO �ε� �Ϸ�";
                    break;
                case ETitleSceneState.LoginSuccess:
                    GetText((int)ETexts.StatusText).text = "TODO �α��� ����!";
                    break;
                case ETitleSceneState.LoginFailure:
                    GetText((int)ETexts.StatusText).text = "TODO �α��� ����";
                    break;
                case ETitleSceneState.ConnectingToServer:
                    GetText((int)ETexts.StatusText).text = "TODO ���� ������";
                    break;
                case ETitleSceneState.ConnectedToServer:
                    GetText((int)ETexts.StatusText).text = "TODO ���� ���� ����";
                    break;
                case ETitleSceneState.FailedToConnectToServer:
                    GetText((int)ETexts.StatusText).text = "TODO ���� ���� ����";
                    break;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        BindObjects(typeof(EGameObjects));
        BindTexts(typeof(ETexts));

        GetObject((int)EGameObjects.StartButton).BindEvent((evt) =>
        {
            //Managers.Scene.LoadScene(Define.EScene.GameScene);
            UI_SelectCharacterPopup popup = Managers.UI.ShowPopupUI<UI_SelectCharacterPopup>();
        });

        GetObject((int)EGameObjects.ServerConnectButton).BindEvent((evt) =>
        {
            ConnectToGameServer();
        });


        GetObject((int)EGameObjects.StartButton).gameObject.SetActive(false);
        GetObject((int)EGameObjects.ServerConnectButton).gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        State = ETitleSceneState.AssetLoading;

        Managers.Resource.LoadAllAsync<Object>("Preload", (key, count, totalCount) =>
        {
            Debug.Log("count");
            GetText((int)ETexts.StatusText).text = $"������ �ε��� : {key} {count}/{totalCount}";

            if (count == totalCount)
            {
                OnAssetLoaded();
            }
        });
    }

    private void OnAssetLoaded()
    {
        State = ETitleSceneState.AssetLoaded;
        Managers.Data.Init();

        UI_LoginPopup popup = Managers.UI.ShowPopupUI<UI_LoginPopup>();
        popup.SetInfo(OnLoginSuccess);
    }

    private void OnLoginSuccess(bool isSuccess)
    {
        if (isSuccess)
        {
            State = ETitleSceneState.LoginSuccess;
            GetObject((int)EGameObjects.ServerConnectButton).gameObject.SetActive(true);
        }
        else
            State = ETitleSceneState.LoginFailure;
    }

    private void ConnectToGameServer()
    {
        State = ETitleSceneState.ConnectingToServer;
        IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
        Managers.Network.GameServer.Connect(endPoint, OnConnectionSuccess, OnConnectionFailed);
        GetObject((int)EGameObjects.ServerConnectButton).gameObject.SetActive(false);
    }

    private void OnConnectionSuccess()
    {
        Debug.Log("Connected To Server");
        State = ETitleSceneState.ConnectedToServer;

        GetObject((int)EGameObjects.StartButton).gameObject.SetActive(true);
    }

    private void OnConnectionFailed()
    {
        Debug.Log("Failed To Connect To Server");
        State = ETitleSceneState.FailedToConnectToServer;
    }

    public void OnAuthResHandler(S_AuthRes resPacket)
    {
        if (State != ETitleSceneState.ConnectedToServer)
            return;

        if (resPacket.Success == false)
            return;

        // ���Ӽ����� ���� ��� ���ָ� ĳ���� ��� ��û.
        //UI_SelectCharacterPopup popup = Managers.UI.ShowPopupUI<UI_SelectCharacterPopup>();
        //popup.SendHeroListReqPacket();
    }
}
