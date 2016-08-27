using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SEPlayer : MonoBehaviour {

    public struct SEData
    {
        public SEData(string resName):this()
        {
            this.resName = resName;
            clip = Resources.Load("SE/" + resName) as AudioClip;
        }
        public string resName;
        public AudioClip clip;
    }

    private List<AudioSource> m_sources = new List<AudioSource>();
    private Dictionary<string, SEData> m_audioMap = new Dictionary<string, SEData>();
    public const float m_maxVolume = 0.6f;

    private static SEPlayer m_instance = null;
    public static SEPlayer Instance
    {
        get
        {
            if(m_instance == null)
            {
                var obj = new GameObject("SEPlayer");
                m_instance = obj.AddComponent<SEPlayer>();
            }
            return m_instance;
        }
    }

    public void Play(string resName,float pitch = 1.0f,bool loop = false)
    {
        if (!m_audioMap.ContainsKey(resName)) { m_audioMap.Add(resName,new SEData(resName));}

        m_sources.Add(gameObject.AddComponent<AudioSource>());
        var index = m_sources.Count - 1;

        m_sources[index].clip = m_audioMap[resName].clip;
        m_sources[index].pitch = pitch;
        m_sources[index].loop = loop;
        m_sources[index].volume = m_maxVolume;
        m_sources[index].Play();
    }

    public void ChangeVolume(string resName,float volume)
    {
        foreach(var source in m_sources)
        {
            if(source.clip.name == resName)
            {
                source.volume = volume;
                break;
            }
        }
    }

    public void Stop(string resName,float time = 0.0f)
    {
        StartCoroutine(WaitStop(resName,time));
    }

    IEnumerator WaitStop(string resName,float time)
    {
        yield return new WaitForSeconds(time);

        foreach(var source in m_sources)
        {
            if(source.clip.name == resName)
            {
                source.Stop();
                break;
            }
        }
    }

    public void AllStop()
    {
        foreach(var source in m_sources)
        {
            source.Stop();
        }
    }

    void Update()
    {
        foreach(var source in m_sources)
        {
            if(!source.isPlaying)
            {
                Destroy(source);
                m_sources.Remove(source);
                break;
            }
        }
    }

    public bool IsPlaying(string resName)
    {
        foreach(var source in m_sources)
        {
            if(source.isPlaying && source.clip.name == resName)
            {
                return true;
            }
        }
        return false;
    }
}
