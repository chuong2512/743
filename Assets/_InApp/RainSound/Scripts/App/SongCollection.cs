using System.Collections.Generic;
using UnityEngine;

public class SongCollection : MonoBehaviour
{
    [SerializeField] private SongButton _songButton;
    [SerializeField] private Transform _content;

    private SongButton _currentSong;
    private List<SongButton> _songButtons = new List<SongButton>();

    void Start()
    {
        ShowAllSongButtons();
        AGameManager.OnChooseSong += OnChooseSong;
    }

    private void OnChooseSong(int i)
    {
        ChooseItem(i);
    }

    private void ShowAllSongButtons()
    {

    }

    public SongButton GetCurrentSong()
    {
        _currentSong = _songButtons.Find(song => song.SongID == GameDataManager.Instance.currentID);
        return _currentSong;
    }

    public void Refresh()
    {
        foreach (var song in _songButtons)
        {
            song.Refresh();
        }
    }

    public void ChooseItem(int songID)
    {
        _currentSong?.Choose(false);
        GetCurrentSong();
        _currentSong?.Choose(true);
    }
}