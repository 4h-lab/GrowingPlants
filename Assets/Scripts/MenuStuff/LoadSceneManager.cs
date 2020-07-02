using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class LoadSceneManager {
    private class CoroutineCaller : MonoBehaviour { } // just a dummy class to call the coroutine from


    private static CoroutineCaller go= null;
    private static GameObject loadingScreen = Resources.Load<GameObject>("Screens/loadingScreen");
    private static bool isAlreadyLoading = false;


    public static void loadNewLevel(int newLevelIndex) {
        loadnewlevel_managestuffandloadingscreen();
        go.StartCoroutine(loading(newLevelIndex));
    }

    public static void loadNewLevel(string newLevelName) {
        loadnewlevel_managestuffandloadingscreen();
        go.StartCoroutine(loading(newLevelName)); 
    }

    private static void loadnewlevel_managestuffandloadingscreen() {
        if (isAlreadyLoading) return;

        if (go == null) go = new GameObject().AddComponent<CoroutineCaller>();
        if (loadingScreen == null) loadingScreen = Resources.Load<GameObject>("Screens/loadingScreen");
        isAlreadyLoading = true;


        GameObject xxx = GameObject.Instantiate(loadingScreen, Vector3.zero, Quaternion.identity);
        RectTransform xxx_rt = xxx.GetComponent<RectTransform>();

        //put the ibject in the canvas.............
        xxx.transform.SetParent(GameObject.Find("Canvas").transform);
        xxx_rt.offsetMin = Vector2.zero;
        xxx_rt.offsetMax = Vector2.zero;
        xxx_rt.localScale = new Vector3(.9f, .9f, 1);
    }


    private static IEnumerator loading(int newLevelIndex) {
        yield return new WaitForSeconds(1f);
        AsyncOperation asyincOp = SceneManager.LoadSceneAsync(newLevelIndex);
        while (!asyincOp.isDone) { 
            float progress = asyincOp.progress;
            Debug.Log(progress);
            yield return new WaitForEndOfFrame();
        }
        isAlreadyLoading = false;
    }

    private static IEnumerator loading(string newLevelName) {
        yield return new WaitForSeconds(1f);
        AsyncOperation asyincOp = SceneManager.LoadSceneAsync(newLevelName);
        while (!asyincOp.isDone) {
            float progress = asyincOp.progress;
            yield return new WaitForEndOfFrame();
        }
        isAlreadyLoading = false;
    }

}
