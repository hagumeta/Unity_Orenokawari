using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シングルトン　クラス
/// 基本的にstaticMethodを用いる
/// フェードエフェクトを用いたシーン遷移を行う．
/// </summary>
public class SceneTransitionManager : MonoBehaviour {
    //シーン遷移の際に使用するエフェクトに掛ける時間
    //シーン遷移全体の時間は fadeTime(暗転遷移) + waitTime(真っ暗のまま待機) + fadeTime(明転遷移)
    [SerializeField]
    private float fadeTime, waitTime;
    public static float FadeTime {
        get => instance.fadeTime;
        set => instance.fadeTime = Mathf.Clamp(value, 0, 100000);
    }
    public static float WaitTime{
        get => instance.waitTime;
        set => instance.waitTime = Mathf.Clamp(value, 0, 100000);
    }

    private static SceneTransitionManager instance;
    private Fade fade;
    private bool isNowTransitioning = false;


    /// <summary>
    /// シングルトンの維持
    /// </summary>
    private void Awake()
    {
        if (instance == null) {
            this.fade = this.GetComponentInChildren<Fade>();
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// 指定シーンへ遷移する
    /// 遷移エフェクトの時間などを別途指定することも可能．
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="transitionTime"></param>
    /// <param name="WaitTime"></param>
    public static void GotoScene(string sceneName, float transitionTime, float WaitTime)
    {
        instance.StartCoroutine(instance.transitionFade(sceneName, transitionTime, WaitTime));
    }
    public static void GotoScene(Scene scene, float transitionTime, float WaitTime)
    {
        instance.StartCoroutine(instance.transitionFade(scene.name, transitionTime, WaitTime));
    }
    /// <summary>
    /// 指定シーンへ遷移
    /// </summary>
    /// <param name="sceneName"></param>
    public static void GotoScene(string sceneName)
    {
        GotoScene(sceneName, FadeTime, WaitTime);
    }
    /// <summary>
    /// 指定シーンへ遷移
    /// </summary>
    /// <param name="scene"></param>
    public static void GotoScene(Scene scene)
    {
        GotoScene(scene.name, FadeTime, WaitTime);
    }



    /// <summary>
    /// 遷移エフェクト管理を行う．
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="FadeTime"></param>
    /// <param name="WaitTime"></param>
    /// <returns></returns>
    private IEnumerator transitionFade(string sceneName, float FadeTime, float WaitTime)
    {
        if (!this.isNowTransitioning) {
            this.isNowTransitioning = true;

            this.fade.FadeIn(FadeTime);
            yield return new WaitForSeconds(FadeTime + WaitTime);
            SceneManager.LoadScene(sceneName);

            yield return new WaitForSeconds(Time.deltaTime*3);
            this.fade.FadeOut(FadeTime);


            yield return new WaitForSeconds(FadeTime);
            this.isNowTransitioning = false;
        }
    }
}