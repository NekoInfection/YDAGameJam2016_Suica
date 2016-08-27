using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMPlayer : MonoBehaviour {

    public struct BGMData
    {
        public BGMData(string resName):this()
        {
            this.resName = resName;
            clip = Resources.Load("BGM/" + resName) as AudioClip;
        }
        public string resName;
        public AudioClip clip;
    }

    private const float minVolume = 0;
    private const float maxVolume = 0.5f;
    private const float startFadeInVolume = 0.005f;
    private static AudioSource source = null;

    Dictionary<string, BGMData> audioMap = new Dictionary<string, BGMData>();

    FadeTimeData fadeTime;

    public bool IsPlaying
    {
        get
        {
            return source.isPlaying;
        }
    }

    private static BGMPlayer instance = null;
    public static BGMPlayer Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = new GameObject("BGMPlayer");
                instance = obj.AddComponent<BGMPlayer>();
                source = obj.AddComponent<AudioSource>();
                source.loop = true;
            }
            return instance;
        }
    }

    public bool IsPlayingByName(string resName)
    {
        if (source.clip == null) return false;
        if(source.clip.name == resName && source.isPlaying)
        {
            return true;
        }

        return false;
    }

    public void Play(string resName,FadeTimeData fadeTime)
    {
        if(!audioMap.ContainsKey(resName))
        {
            audioMap.Add(resName, new BGMData(resName));
        }

        this.fadeTime = fadeTime;
        source.clip = audioMap[resName].clip;
        source.Play();
        source.volume = startFadeInVolume;
        StartFadeIn(fadeTime.inTime);
    }

    public void Stop()
    {
        StartFadeOut(fadeTime.outTime);
    }

    public void ChangeVolume(float volume)
    {
        source.volume = volume;
    }

    void StartFadeIn(float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", startFadeInVolume, "to", maxVolume, "time", time, "onupdate", "UpdateHandler"));
    }

    void StartFadeOut(float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", maxVolume, "to", minVolume, "time", time, "onupdate", "UpdateHandler"));
    }

    void UpdateHandler(float value)
    {
        source.volume = value;
        if(source.volume <= 0)
        {
            source.Stop();
        }
    }
}
