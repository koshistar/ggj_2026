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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Load(int sceneNum)
    {
        StartCoroutine(LoadCoroutine(sceneNum));
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
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;

        transitionCanvas.SetActive(true);

        transitionImage.gameObject.SetActive(true);
        transitionImage.color =
            new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 1.0f);
        while (loadingOperation.progress < 0.9f)
            yield return null;
        yield return new WaitForSeconds(1.0f);
        //
        transitionImage.DOFade(0f, 1.0f);

        yield return new WaitForSeconds(1f);

        //Debug.Log("Load");
        loadingOperation.allowSceneActivation = true;

        transitionImage.gameObject.SetActive(false);

        transitionCanvas.SetActive(false);
    }

    IEnumerator LoadCoroutine(int sceneNum)
    {
        var loadingOperation = SceneManager.LoadSceneAsync(sceneNum);
        loadingOperation.allowSceneActivation = false;

        transitionCanvas.SetActive(true);

        transitionImage.gameObject.SetActive(true);
        transitionImage.color =
            new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 1.0f);

        Debug.Log(loadingOperation.progress);
        while (loadingOperation.progress < 0.9f)
            yield return null;
        yield return new WaitForSeconds(1.0f);

        transitionImage.DOFade(0f, 1.0f);

        yield return new WaitForSeconds(1f);

        Debug.Log("Load");
        loadingOperation.allowSceneActivation = true;

        transitionImage.gameObject.SetActive(false);

        transitionCanvas.SetActive(false);
    }
}
