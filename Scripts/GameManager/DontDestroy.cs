using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public GameObject[] gameObjects;
    public static int checkLoad;
    public static bool restart;

    private static DontDestroy D;
    private void Awake()
    {
        if (DontDestroy.D != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
         if (SceneManager.GetActiveScene().isLoaded)
         {
            GameManager.instance.wasDead++;
            checkLoad++;

            if (PlayerPrefs.HasKey("score") && checkLoad > 1 && restart == false)
                GameManager.instance.score = PlayerPrefs.GetInt("score", GameManager.instance.finalScore);

            Debug.Log(checkLoad);
         }
    }
}
