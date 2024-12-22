using System;
using SingleApp;
using UnityEngine;

[DefaultExecutionOrder(-99)]
public class CAudioManager : Singleton<CAudioManager>
{
    public AudioSource musicSource;
    public float _timePlay = 0.6f;

    private AudioClip _audioClip;
    private bool _isPlaying;
    private int _crtId = -1;

    public bool IsPlaying => _isPlaying;

    public void SetTimeCount(float time)
    {
        Play();
        AGameManager.SetTimeStop?.Invoke(time);
    }

    private void Start()
    {
        _isPlaying = false;
        _crtId = GameDataManager.Instance.currentID;
        
    }

    public void PlaySong(int id)
    {
        GameDataManager.Instance.SetCurrentSongID(id);

        _crtId = id;
      
        musicSource.clip = _audioClip;
        musicSource.Play();
        _isPlaying = true;
        AGameManager.OnPlayMusic?.Invoke(_isPlaying);
    }

    public void ClickPlayBtn()
    {
        if (!_isPlaying)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
            AGameManager.SetTimeStop.Invoke(0);
        }

        _isPlaying = !_isPlaying;

        AGameManager.OnPlayMusic?.Invoke(_isPlaying);
        Debug.Log($"PLaying : {_isPlaying}");
    }

    public void Stop()
    {
        musicSource.Stop();
        _isPlaying = false;

        AGameManager.OnPlayMusic.Invoke(_isPlaying);
    }

    public void Play()
    {
        if (!_isPlaying)
        {
            musicSource.Play();
            _isPlaying = true;
        }

        AGameManager.OnPlayMusic.Invoke(_isPlaying);
    }
}