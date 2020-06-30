using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class LoadSceneManager {
    private class CoroutineCaller : MonoBehaviour { } // just a dummy class to call the coroutine from


    private static CoroutineCaller go= null;
    private static GameObject loadingScreen = null;
    private static bool isAlreadyLoading = false;


    public static void loadNewLevel(int newLevelIndex) {
        if (isAlreadyLoading) return;

        if (go == null)  go = new GameObject().AddComponent<CoroutineCaller>();
        if (loadingScreen == null) loadingScreen = Resources.Load<GameObject>("Screens/loadingScreen");
        isAlreadyLoading = true;


        GameObject xxx = GameObject.Instantiate(loadingScreen, Vector3.zero, Quaternion.identity);
        xxx.transform.SetParent(GameObject.Find("Canvas").transform);

        go.StartCoroutine(loading(newLevelIndex));
    }

    public static void loadNewLevel(string newLevelName) {
        loadNewLevel(SceneManager.GetSceneByName(newLevelName).buildIndex);
    }


    private static IEnumerator loading(int newLevelIndex) {
        AsyncOperation asyincOp = SceneManager.LoadSceneAsync(newLevelIndex);

        while (!asyincOp.isDone) { 

            float progress = asyincOp.progress;
            Debug.Log(progress);

            yield return new WaitForEndOfFrame();
        }
        isAlreadyLoading = false;


    }

}
