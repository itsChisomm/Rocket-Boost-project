using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{

    [SerializeField] private float levelLoadDelay = 1f;

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartFinishSequence();                
                break;
            default:
                StartCrashSequence();                
                break;
        }
    }

    private void StartFinishSequence()
    {
        GetComponent<PlayerController>().enabled = false; // disable player control
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        GetComponent<PlayerController>().enabled = false; // disable player control 
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // if it's the last level
        {
            nextSceneIndex = 0; // go back to the first level
        }

        SceneManager.LoadScene(nextSceneIndex); // load the next level

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
