using UnityEngine;

public struct SoundData
{
    private int soundID;

    public int SoundID
    {
        get => soundID;
        set => soundID = value;
    }
}

public class Sound : MonoBehaviour
{
    [SerializeField] private SoundData data;

    public void Play()
    {
        
    }

    public void Stop()
    {
        
    }
}