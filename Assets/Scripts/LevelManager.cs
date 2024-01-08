using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float loadDelay = 1.5f;

    // có scene Menu là scene 0 r nên index của level 1 là 1 
    int firstSceneIndex = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Dũng mới thoát game");
        }
        else { return; }
    }

    // Hàm bắt đầu Coroutine Reset Level
    public void StartResetLevel()
    {
        StartCoroutine(ResetLevel());
    }

    // Hàm bắt đầu Coroutine load Level kế, nếu không thì load lại level đầu tiên

    public void StartNextLevel()
    {
        StartCoroutine(NextLevel());
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(loadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(loadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        // nếu chưa làm menu intro và outro thì k cần trừ, nếu làm 1 menu thôi thì -1, làm 2 menu thì trừ 2
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(firstSceneIndex);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
