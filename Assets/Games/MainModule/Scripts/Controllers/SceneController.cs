using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using XFABManager;
using XFGameFramework;

public class SceneController : Controller
{
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="onFinished"></param>
    /// <param name="onProgress"></param>
    public void LoadScene(string sceneName, UnityAction onFinished, UnityAction<float> onProgress)
    {
        if (SceneManager.GetSceneByName(sceneName).IsValid())
        {
            //表示当前场景已经加载了
            onFinished?.Invoke();
            onProgress?.Invoke(1);
            return;
        }

        CoroutineStarter.Start(LoadSceneHandler(sceneName, onFinished, onProgress));
    }


    private IEnumerator LoadSceneHandler(string sceneName, UnityAction onFinished, UnityAction<float> onProgress)
    {
        //因为场景会被打包到AssetBundle，所以需要使用AssetBundleManager来加载
        LoadSceneRequest asyncOperation = AssetBundleManager.LoadSceneAsynchrony(Module.ProjectName, sceneName, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
            onProgress?.Invoke(asyncOperation.progress);
        }

        onFinished?.Invoke();
    }
}

