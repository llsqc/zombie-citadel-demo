using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;
    public Toggle tglMusic;
    public Toggle tglSound;
    public Slider sldMusic;
    public Slider sldSound;

    protected override void Init()
    {
        MusicData  musicData = GameDataMgr.Instance.musicData;
        tglMusic.isOn = musicData.isMusicOpen;
        tglSound.isOn = musicData.isSoundOpen;
        sldMusic.value = musicData.musicValue;
        sldSound.value = musicData.soundValue;
        
        btnClose.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.SaveMusicData();
            UIManager.Instance.HidePanel<SettingPanel>();
        });

        tglMusic.onValueChanged.AddListener(isOn =>
        {
            BKMusic.Instance.SetIsOpen(isOn);
            GameDataMgr.Instance.musicData.isMusicOpen = isOn;
        });

        tglSound.onValueChanged.AddListener(isOn => { GameDataMgr.Instance.musicData.isSoundOpen = isOn; });

        sldMusic.onValueChanged.AddListener(value =>
        {
            BKMusic.Instance.SetVolume(value);
            GameDataMgr.Instance.musicData.musicValue = value;
        });

        sldSound.onValueChanged.AddListener(value => { GameDataMgr.Instance.musicData.soundValue = value; });
    }
}