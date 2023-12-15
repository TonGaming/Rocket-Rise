using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float loadDelay = 1.5f;

    int firstSceneIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Hàm bắt đầu Coroutine Reset Level
    public void StartResetLevel()
    {
        StartCoroutine(ResetLevel());
    }

    // Hàm bắt đầu Coroutine load Level kế, nếu không thì load lại level đầu tiên

    public void StartLoadNextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(loadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(loadDelay);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        // nếu chưa làm menu intro và outro thì k cần trừ, nếu làm 1 menu thôi thì -1, làm 2 menu thì trừ 2
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings )
        {
            SceneManager.LoadScene(nextSceneIndex);
        } 
        else
        {
            SceneManager.LoadScene(firstSceneIndex);
        }
    }
}
