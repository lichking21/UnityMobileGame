using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public static Buttons instance;

    public void ContinueBtn()
    {
        InterstitialAds.S.ShowAd();
        GameManager.instance.continueBtn.SetActive(false);
        DontDestroy.restart = false;
    }
    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DontDestroy.restart = true;
    }
    public void ExitBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseBtn()
    {
        GameManager.instance.pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeBtn()
    {
        GameManager.instance.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MuteBtn()
    {
        AudioListener.volume = 0f;
        GameManager.instance.muteBtn.SetActive(false);
        GameManager.instance.unMuteBtn.SetActive(true);
    }
    public void UnMuteBtn()
    {
        AudioListener.volume = 1f;
        GameManager.instance.muteBtn.SetActive(true);
        GameManager.instance.unMuteBtn.SetActive(false);
    }
}
