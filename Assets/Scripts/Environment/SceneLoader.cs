using System.Collections;
using SKCell;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    [SerializeField] private GameObject transitionCanvas;
    [SerializeField] private SKImage transitionImage;

    [SerializeField] private float fadeTime = 1f;

    private Coroutine loadCoroutine;
    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transitionCanvas);
        transitionCanvas.SetActive(false);
        transitionImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Load(int sceneNum)
    {
        if (loadCoroutine != null)
        {
            StopCoroutine(loadCoroutine);
        }

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
        loadCoroutine = StartCoroutine(LoadCoroutine(sceneNum));
    }

    public void Load(string sceneName)
    {
        if (loadCoroutine != null)
        {
            StopCoroutine(loadCoroutine);
        }

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        loadCoroutine = StartCoroutine(LoadCoroutine(sceneName));
    }

    IEnumerator LoadCoroutine(string sceneName)
    {
        // 1. 准备工作
        transitionCanvas.SetActive(true);
        transitionImage.gameObject.SetActive(true);
    
        // 初始化透明度（防止之前残留的 alpha 值影响）
        Color c = transitionImage.color;
        transitionImage.color = new Color(c.r, c.g, c.b, 0f);

        // 2. 执行淡入动画
        // 使用 .SetUpdate(true) 确保即使游戏暂停也能播放动画
        yield return transitionImage.DOFade(1.0f, fadeTime).SetUpdate(true).WaitForCompletion();
    
        // 3. 开始异步加载
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;

        // 4. 等待加载完成 (0.9f 表示场景已准备就绪)
        while (loadingOperation.progress < 0.9f)
        {
            yield return null;
        }

        // 5. 激活场景
        loadingOperation.allowSceneActivation = true;

        // 必须等待一帧，确保新场景已经初始化，否则旧场景的组件可能会干扰后续操作
        yield return null; 

        // 6. 执行淡出动画
        yield return transitionImage.DOFade(0f, fadeTime).SetUpdate(true).WaitForCompletion();

        // 7. 清理
        transitionImage.gameObject.SetActive(false);
        transitionCanvas.SetActive(false);
        loadCoroutine = null;
    }

    IEnumerator LoadCoroutine(int sceneNum)
    {
        var loadingOperation = SceneManager.LoadSceneAsync(sceneNum);
        loadingOperation.allowSceneActivation = false;

        transitionCanvas.SetActive(true);

        transitionImage.gameObject.SetActive(true);
        // transitionImage.color =
        //     new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 1.0f);
        transitionImage.DOFade(1.0f, 1.0f);
        

        Debug.Log(loadingOperation.progress);
        while (loadingOperation.progress < 0.9f)
            yield return null;
        yield return new WaitForSeconds(1.0f);

        transitionImage.DOFade(0f, 1.0f);
        
        Debug.Log("Load");
        loadingOperation.allowSceneActivation = true;
        yield return new WaitForSeconds(1f);

        transitionImage.gameObject.SetActive(false);

        transitionCanvas.SetActive(false);
    }
}
