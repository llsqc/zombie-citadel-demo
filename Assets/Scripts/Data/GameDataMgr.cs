using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr _instance = new GameDataMgr();
    public static GameDataMgr Instance => _instance;

    public RoleInfo nowSelectRole;

    public MusicData musicData;
    public PlayerData playerData;
    public List<RoleInfo> roleInfoList;
    public List<SceneInfo> sceneInfoList;
    public List<MonsterInfo> monsterInfoList;
    public List<TowerInfo> towerInfoList;

    private GameDataMgr()
    {
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        monsterInfoList = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
        towerInfoList = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
    }

    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }

    public void PlaySound(string resName)
    {
        GameObject musicObj = new GameObject();
        AudioSource audioSource = musicObj.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(resName);
        audioSource.volume = musicData.soundValue;
        audioSource.mute = !musicData.isSoundOpen;
        audioSource.Play();

        Object.Destroy(musicObj, audioSource.clip.length + 1);
    }
}