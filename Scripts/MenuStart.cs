using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
