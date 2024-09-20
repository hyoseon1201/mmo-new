using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CharacterSlotItem : UI_Base
{
    private enum GameObjects
    {

    }

    private enum Texts
    {
        CharacterNameText,
        ClassText,
        LevelText,
    }

    private enum Images
    {
        CharacterFrameImage,
        CharacterImage,
        CharacterLevelFrameImage,
        SelectHeroImage
    }

    int _index;
    MyHeroInfo _info;
    bool _selected = false;
    Action<int> _onHeroSelected;

    protected override void Awake()
    {
        base.Awake();

        BindObjects(typeof(GameObjects));
        BindTexts(typeof(Texts));
        BindImages(typeof(Images));

        GetImage((int)Images.SelectHeroImage).gameObject.BindEvent(OnClickSelectHeroImage);
    }

    public void RefreshUI()
    {
        if (_info == null)
            return;

        GetText((int)Texts.CharacterNameText).text = _info.HeroInfo.Name;
        GetText((int)Texts.ClassText).text = _info.HeroInfo.ClassType.ToString();
        GetText((int)Texts.LevelText).text = _info.HeroInfo.CreatureInfo.StatInfo.Level.ToString();

        if (_selected)
            GetImage((int)Images.SelectHeroImage).color = new Color(0.8f, 0.8f, 0.15f, 0.15f);
        else
            GetImage((int)Images.SelectHeroImage).color = new Color(0.8f, 0.8f, 0.15f, 0);
    }

    private void OnClickSelectHeroImage(PointerEventData evt)
    {
        _onHeroSelected.Invoke(_index);
        RefreshUI();
    }

    internal void SetInfo(int i, MyHeroInfo myHeroInfo, bool v, Action<int> onHeroSelected)
    {
        throw new NotImplementedException();
    }
}
