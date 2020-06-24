using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadNewScene(int indexScene)
    {
        //Загрузить сцену по индексу
        if (indexScene >= 0 && indexScene <= SceneManager.sceneCount)
        {
            SceneManager.LoadScene(indexScene);
        }
    }
}
