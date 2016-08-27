using UnityEngine;
using System.Collections;

/// <summary>
/// フェードインやフェードアウトを管理するクラス
/// </summary>

public class FadeManager : MonoBehaviour {

    private Texture2D texture = null;
    private float fadeAlpha = 0;
    private float fadeTime = 0;
    
    public bool IsFading
    {
        get;
        private set;
    }


    void Awake()
    {
        texture = new Texture2D(32, 32, TextureFormat.RGB24, false);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
    }

    void OnGUI()
    {
        GUI.color = new Color(0, 0, 0, fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
    }


    /// <summary>
    /// フェードインの開始
    /// </summary>
    /// <param name="time">時間</param>
    public void StartFadeIn(float time)
    {
        StartFade(time, 1, 0);
    }

    /// <summary>
    /// フェードアウトの開始
    /// </summary>
    /// <param name="time">時間</param>
    public void StartFadeOut(float time)
    {
        StartFade(time, 0, 1);
    }

    /// <summary>
    /// フェード処理**
    /// (iTweenの利用)
    /// </summary>
    /// <param name="time">フェードする時間</param>
    /// <param name="begin">開始する値</param>
    /// <param name="end">終了する値</param>
    private void StartFade(float time,float begin,float end)
    {
        IsFading = true;
        fadeTime = time;
        iTween.ValueTo(gameObject, iTween.Hash("from", begin, "to", end, "time", fadeTime, "onupdate", "UpdateHandler"));
    }

    private void UpdateHandler(float value)
    {
        fadeAlpha = value;
        fadeTime -= Time.deltaTime;
        if(fadeTime <= 0)
        {
            IsFading = false;
        }
    }
}
