using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField]
    private int levelIndex;

    public void  LoadNextLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
