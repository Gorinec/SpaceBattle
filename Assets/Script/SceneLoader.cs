using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour

{

    // Загрузка сцены по индексу
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Перезапуск текущей сцены
    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

   
}

