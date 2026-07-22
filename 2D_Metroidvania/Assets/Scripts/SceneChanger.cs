using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;

    public void ChangeSceneNow()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
