using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Songs ", menuName = "Data/Scriptable Object/Song Infor SO")]
public class SongSO : ScriptableObject
{
    public SongInfor[] songInfors;

    public SongInfor GetSongWithID(int id)
    {
        int n = songInfors.Length;
        for (int i = 0; i < n; i++)
        {
            if (id == songInfors[i].songID) return songInfors[i];
        }

        return null;
    }

    public void SetID()
    {
        int n = songInfors.Length;
        for (int i = 0; i < n; i++)
        {
            songInfors[i] = new SongInfor
            {
                songID = i
            };
        }
    }


#if UNITY_EDITOR
    [Header("Get Audio Clips")] public string folderPath;

    public void SetAllDatas()
    {
        var spriteGUIDs = UnityEditor.AssetDatabase.FindAssets("t:sprite", new[] {folderPath});

        songInfors = new SongInfor[spriteGUIDs.Length];

        SetID();

        int n = songInfors.Length;

        for (int i = 0; i < n; i++)
        {
            string audioPath =
                UnityEditor.AssetDatabase.GUIDToAssetPath(spriteGUIDs[i]); // Chuyển đổi GUID sang đường dẫn
            var sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(audioPath); // Tải T từ đường dẫn

            songInfors[i].icon = sprite;
        }

        var assets = UnityEditor.AssetDatabase.FindAssets("t:audioclip", new[] {folderPath});

        for (int i = 0; i < n; i++)
        {
            string audioPath = UnityEditor.AssetDatabase.GUIDToAssetPath(assets[i]); // Chuyển đổi GUID sang đường dẫn
            var audioClip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath); // Tải T từ đường dẫn

            songInfors[i].song = audioClip;
        }
    }
#endif
}

[Serializable]
public class SongInfor
{
    public int songID;
    public string name;
    public int price = 200;
    public AudioClip song;
    public Sprite icon;
    public GameObject musicalObject;
}