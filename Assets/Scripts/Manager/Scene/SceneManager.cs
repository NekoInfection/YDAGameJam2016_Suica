using UnityEngine;
using System.Collections;

/// <summary>
/// シーン管理用クラス
/// </summary>

public class SceneManager : ManagerBase {

    /// <summary>
    /// 状態
    /// </summary>
    enum State
    {
        None,
        Change,
        Loading,
    }

    /// <summary>
    /// シーンのプレハブフォルダー名
    /// </summary>
    private const string scenePrefabFolder = "ScenePrefabs/";

    /// <summary>
    /// ローディングのフォルダー名
    /// </summary>
    private const string loadingFolder = "Loading";

    private static SceneManager instance = null;
    public static SceneManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.Log("Create SceneManager!!");
                var obj = new GameObject("SceneMangaer");
                instance = obj.AddComponent<SceneManager>();
                instance.GetComponent<SceneManager>().enabled = true;
                instance.gameObject.AddComponent<FadeManager>();
            }
            return instance;
        }
    }

    /// <summary>
    /// フェードマネージャー
    /// </summary>
    private FadeManager fadeManager = null;


    /// <summary>
    /// フェードタイム
    /// </summary>
    private FadeTimeData fadeTime = new FadeTimeData(1, 1);

    /// <summary>
    /// 現在のシーンオブジェクト
    /// </summary>
    private GameObject scene = null;

    /// <summary>
    /// ローディングインスタンス
    /// </summary>
    private GameObject loadingInstance = null;

    /// <summary>
    /// ローディング管理
    /// </summary>
    private LoadingManager loadingManager = null;

    /// <summary>
    /// 現在のシーン
    /// </summary>
    private SceneNameManager.Scene nowScene = SceneNameManager.Scene.TitleScene;

    /// <summary>
    /// 次のシーン
    /// </summary>
    private SceneNameManager.Scene nextScene = SceneNameManager.Scene.TitleScene;

    /// <summary>
    /// シーン管理の状態
    /// </summary>
    [SerializeField]
    private State state = State.None;

    /// <summary>
    /// ローディングするか判定
    /// </summary>
    private bool isLoading = false;

    /// <summary>
    /// ステージ番号の管理用だったけどロード画面のスプライトナンバーになってる
    /// </summary>
    public static int gameStage = 0;

    /// <summary>
    /// ロード画面用
    /// </summary>
    public static float WaitTimeData = 2.5f;
    
    
    /// <summary>
    /// 初期化するよ
    /// </summary>
    void Start()
    {
        InitManager(this, ManagerID.SCENE);

        fadeManager = GetComponent<FadeManager>();
        fadeManager.StartFadeIn(fadeTime.inTime);

        scene = GameObject.Find(nowScene.ToString());
        //Debug.Log("nowScene = " + scene);
    }

    /// <summary>
    /// いろいろ監視しなきゃいけない。面倒である
    /// </summary>
    void Update()
    {
        //Debug.Log("SceneManager Update!!");
        ChangeScene();
        LoadingFade();
    }

    /// <summary>
    /// シーンを切り替える準備
    /// </summary>
    /// <param name="scene">次に切り替えるシーンの名前</param>
    /// <param name="isLoading">ローディングするかどうか</param>
    public void ChangeSceneLoad(SceneNameManager.Scene scene,bool isLoading)
    {
        if (state != State.None) return;

        fadeManager.StartFadeOut(fadeTime.outTime);
        nextScene = scene;
        state = State.Change;
        this.isLoading = isLoading;
    }

    /// <summary>
    /// ローディングのフェードイン
    /// </summary>
    private void LoadingFade()
    {
        if (state != State.Loading) return;
        if (GameObject.Find(nowScene.ToString()) == null) return;

        loadingManager.StartFade();
        isLoading = false;
        state = State.None;
    }

    /// <summary>
    /// シーンの切り替え
    /// </summary>
    private void ChangeScene()
    {
        if (state != State.Change) return;
        if (fadeManager.IsFading) return;

        UnLoadDestroy();
        fadeManager.StartFadeIn(fadeTime.inTime);

        state = CheckLoadingScene();
        nowScene = nextScene;
    }


    /// <summary>
    /// ローディングかどうかのチェック
    /// ローディングしている場合は、ローディング画面を生成->次のシーンの生成
    /// ローディングしてない場合は、そのまま次のシーンの生成
    /// </summary>
    /// <returns></returns>
    private State CheckLoadingScene()
    {
        if(isLoading)
        {
            loadingInstance = Instantiate(Resources.Load(scenePrefabFolder + loadingFolder)) as GameObject;
            loadingManager = loadingInstance.GetComponent<LoadingManager>();
            loadingManager.SetLoadingData(fadeManager, fadeTime);
            StartCoroutine("WaitCreateScene");
            return State.Loading;
        }
        else
        {
            CreateScene();
            return State.None;
        }
    }

    /// <summary>
    /// データの削除
    /// </summary>
    private void UnLoadDestroy()
    {
        Destroy(scene);
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// シーンの生成
    /// </summary>
    private void CreateScene()
    {
        Debug.Log("---Called CreateScene!! " + scenePrefabFolder + nextScene.ToString());
        scene = Instantiate(Resources.Load(scenePrefabFolder + nextScene.ToString()), Vector3.zero, Quaternion.identity) as GameObject;
        scene.name = nextScene.ToString();
    }

    IEnumerator WaitCreateScene()
    {
        yield return new WaitForSeconds(WaitTimeData);
        CreateScene();
    }
}
