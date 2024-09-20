using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class UI_SelectCharacterPopup : UI_Popup
{
    enum GameObjects
    {
        StartButton,
        CreateCharacterButton,
        DeleteCharacterButton,
        CloseButton,
        UI_CharacterSlotItem1,
        UI_CharacterSlotItem2,
        UI_CharacterSlotItem3,
    }

    List<MyHeroInfo> _heroes = new List<MyHeroInfo>();
    int _selectedHeroIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        BindObjects(typeof(GameObjects));

        GetObject((int)GameObjects.StartButton).BindEvent(OnClickStartButton);
        GetObject((int)GameObjects.CreateCharacterButton).BindEvent(OnClickCreateCharacterButton);
        GetObject((int)GameObjects.DeleteCharacterButton).BindEvent(OnClickDeleteCharacterButton);
        GetObject((int)GameObjects.CloseButton).BindEvent(OnClickCloseButton);


    }


    private void OnHeroSelected(int index)
    {
        _selectedHeroIndex = index;
    }

    private void OnClickStartButton(PointerEventData evt)
    {

    }

    private void OnClickCloseButton(PointerEventData evt)
    {
        throw new NotImplementedException();
    }

    private void OnClickDeleteCharacterButton(PointerEventData evt)
    {
        throw new NotImplementedException();
    }

    private void OnClickCreateCharacterButton(PointerEventData evt)
    {
        throw new NotImplementedException();
    }
}
