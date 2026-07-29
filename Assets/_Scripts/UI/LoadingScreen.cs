using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    IEnumerator Start()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);

        while (operation.isDone == false)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }
}
