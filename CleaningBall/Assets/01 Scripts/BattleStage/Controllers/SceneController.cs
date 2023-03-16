using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ChangeScene(string _sceneName)
    {
        DataManager.Instance.SaveData();
        DataManager.Instance.ClearObserverList();
        SceneManager.LoadScene(_sceneName);
    }
}
