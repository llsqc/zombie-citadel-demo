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

    private GameDataMgr()
    {
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
    }

    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }
}