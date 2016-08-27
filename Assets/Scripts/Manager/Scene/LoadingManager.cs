using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadingManager : MonoBehaviour {

    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();

    private Image image = null;

    private FadeManager fadeManager = null;
    private FadeTimeData fadeTime;
    private bool isFadeOut = false;

    /// <summary>
    /// ローディングの情報設定
    /// </summary>
    /// <param name="fadeManager">フェードする管理インスタンス</param>
    /// <param name="fadeTime">フェードタイム情報</param>
    public void SetLoadingData(FadeManager fadeManager,FadeTimeData fadeTime)
    {
        this.fadeManager = fadeManager;
        this.fadeTime = fadeTime;

        image = FindObjectOfType<Image>();
        Debug.Log("Loading...SpriteNumber = " + SceneManager.gameStage);
        Debug.Log("Loading...SpriteName = " + sprites[SceneManager.gameStage].name);
        image.sprite = sprites[SceneManager.gameStage];
    }

    /// <summary>
    /// フェードスタート
    /// </summary>
    public void StartFade()
    {
        fadeManager.StartFadeOut(fadeTime.outTime);
        isFadeOut = true;
    }

    /// <summary>
    /// ｱｯﾌﾟﾃﾞｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴﾄ
    /// </summary>
    void Update()
    {
        if (!isFadeOut) return;
        if (fadeManager.IsFading) return;

        fadeManager.StartFadeIn(fadeTime.inTime);
        Destroy(gameObject);
    }
}