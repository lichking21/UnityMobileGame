using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject Vrag;
    public GameObject enemy2;
    public GameObject enemy3;

    public GameObject streekObj;
    public GameObject pauseMenu;
    public GameObject continueBtn;
    public GameObject exitBtn;
    public GameObject muteBtn;
    public GameObject unMuteBtn;
    public GameObject restartBtn;
    public GameObject DeathWindow;

    public Transform[] spawnPoints;

    public Text textScore;
    public Text textStreek;
    public Text textFinalScore;
    public Text textRecord;
    public Text textGameRecord;

    public int lastScore;
    public int score;
    public int streek;
    public int finalScore;
    public int record;

    Enemy enemy = new Enemy();
    public Animator streekAnim;
    private Animator dAnim;
    private GameObject player;
    public int wasDead;

    private void Start()
    {
        AudioListener.volume = 1f;

        continueBtn.SetActive(false);
        restartBtn.SetActive(false);
        exitBtn.SetActive(false);
        unMuteBtn.SetActive(false);
        pauseMenu.SetActive(false);

        dAnim = DeathWindow.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnEnemy2());

        if (PlayerPrefs.HasKey("record"))
            record = PlayerPrefs.GetInt("record", record);
    }
    private void Update()
    {
        textScore.text = score.ToString();
        textStreek.text = streek.ToString();
        textFinalScore.text = finalScore.ToString();
        textRecord.text = record.ToString();
        textGameRecord.text = record.ToString();

        

        PlayerLife();
    }
    private void PlayerLife()
    {
        if (player.GetComponent<Player>().currHp > 0)
        {
            dAnim.SetBool("isDead", false);
        }
        else if (player.GetComponent<Player>().currHp <= 0)
        {
            StopAllCoroutines();

            dAnim.SetBool("isDead", true);

            continueBtn.SetActive(true);
            restartBtn.SetActive(true);
            exitBtn.SetActive(true);

            finalScore = score;
            if (finalScore > record)
            {
                record = finalScore;
            }

            PlayerPrefs.SetInt("score", finalScore);
            PlayerPrefs.SetInt("record", record);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (enemy == null)
            {
                // Enemy 1
                if (streek < 10)
                {
                    yield return new WaitForSeconds(2.5f);
                    FirstTypeEnemy();
                }   
                else if (streek >= 10 && streek <= 20)
                {
                    yield return new WaitForSeconds(1.5f);
                    FirstTypeEnemy();
                }
                else if (streek >= 20 && streek <= 30)
                {
                    yield return new WaitForSeconds(1f);
                    FirstTypeEnemy();
                }
                else if (streek >= 30)
                {
                    yield return new WaitForSeconds(0.8f);
                    FirstTypeEnemy();
                }

                // Enemy 2
                if (streek >= 10 && streek <= 20)
                {
                    yield return new WaitForSeconds(2f);
                    SecondTypeEnemy();
                }
                else if (streek >= 20 && streek <= 35)
                {
                    yield return new WaitForSeconds(1.3f);
                    SecondTypeEnemy();
                }
                else if (streek > 35)
                {
                    yield return new WaitForSeconds(1f);
                    SecondTypeEnemy();
                }

                // Enemy 3
                if (streek >= 30 && streek <= 45)
                {
                    yield return new WaitForSeconds(1.8f);
                    ThirdTypeEnemy();
                }
                else if (streek > 45)
                {
                    yield return new WaitForSeconds(1.3f);
                    ThirdTypeEnemy();
                }
            }
        }
    }
    private IEnumerator SpawnEnemy2()
    {
        while (true)
        {
            if (enemy == null)
            {
                if (streek < 10)
                {
                    yield return new WaitForSeconds(2.5f);
                    FirstTypeEnemy1();
                }
                else if (streek >= 10 && streek <= 20)
                {
                    yield return new WaitForSeconds(1.5f);
                    FirstTypeEnemy1();
                }
                else if (streek >= 20 && streek <= 30)
                {
                    yield return new WaitForSeconds(1f);
                    FirstTypeEnemy1();
                }
                else if (streek >= 30)
                {
                    yield return new WaitForSeconds(0.8f);
                    FirstTypeEnemy1();
                }

                // Enemy 2
                if (streek >= 10 && streek <= 20)
                {
                    yield return new WaitForSeconds(2f);
                    SecondTypeEnemy1();
                }
                else if (streek >= 20 && streek <= 35)
                {
                    yield return new WaitForSeconds(1.3f);
                    SecondTypeEnemy1();
                }
                else if (streek > 35)
                {
                    yield return new WaitForSeconds(1f);
                    SecondTypeEnemy1();
                }
            }
        }
    }

    private void FirstTypeEnemy()
    {
        GameObject enemy_ = null;
        Quaternion rot = new Quaternion(0, 0, 0, 0);

        if (enemy == null)
        {
            if (Random.value < 0.5) // 1 enemy spawn point
            {
                if (Random.value < 0.5)
                    enemy_ = Instantiate(Vrag, spawnPoints[0].position, rot); //right spawn point
                else
                    enemy_ = Instantiate(Vrag, spawnPoints[1].position, rot); // left spawn point
            }
            else // 2 enemyes spawn points
            {
                if (Random.value < 0.5) 
                {
                    enemy_ = Instantiate(Vrag, spawnPoints[0].position, rot); //right spawn point
                    enemy_ = Instantiate(Vrag, spawnPoints[2].position, rot); //right spawn point
                }
                else
                {
                    enemy_ = Instantiate(Vrag, spawnPoints[1].position, rot); // left spawn point
                    enemy_ = Instantiate(Vrag, spawnPoints[3].position, rot); // left spawn point
                }
            }
        }

        if (enemy_ != null && enemy_.gameObject.activeSelf == false)
            enemy_ = null;
    }
    private void SecondTypeEnemy()
    {
        GameObject enemy2_ = null;
        Quaternion rot = new Quaternion(0, 0, 0, 0);

        if (enemy == null)
        {
            if (Random.value < 0.5)
            {
                if (Random.value < 0.5)
                {
                    enemy2_ = Instantiate(enemy2, spawnPoints[4].position, rot);
                }
                else
                {
                    enemy2_ = Instantiate(Vrag, spawnPoints[5].position, rot);
                }
            }
            else
            {
                if (Random.value < 0.5)
                {
                    enemy2_ = Instantiate(enemy2, spawnPoints[4].position, rot);
                    enemy2_ = Instantiate(enemy2, spawnPoints[6].position, rot);
                }
                else
                {
                    enemy2_ = Instantiate(Vrag, spawnPoints[5].position, rot);
                    enemy2_ = Instantiate(Vrag, spawnPoints[7].position, rot);
                }
            }
        }

        if (enemy2_ != null && enemy2_.gameObject.activeSelf == false)
            enemy2_ = null;
    }
    private void ThirdTypeEnemy()
    {
        GameObject enemy3_ = null;
        Quaternion rot = new Quaternion(0, 0, 0, 0);

        if (enemy == null)
        {
            if (Random.value < 0.5)
            {
                enemy3_ = Instantiate(enemy3, spawnPoints[8].position, rot);
            }
            else
            {
                enemy3_ = Instantiate(enemy3, spawnPoints[9].position, rot);
            }
        }

        if (enemy3_ != null && enemy3_.gameObject.activeSelf == false)
            enemy3_ = null;
    }

    private void FirstTypeEnemy1()
    {
        GameObject enemy_ = null;
        Quaternion rot = new Quaternion(0, 0, 0, 0);

        if (enemy == null)
        {
            if (Random.value < 0.5) // 1 enemy spawn point
            {
                if (Random.value < 0.5)
                    enemy_ = Instantiate(Vrag, spawnPoints[10].position, rot); //right spawn point
                else
                    enemy_ = Instantiate(Vrag, spawnPoints[11].position, rot); // left spawn point
            }
        }

        if (enemy_ != null && enemy_.gameObject.activeSelf == false)
            enemy_ = null;
    }
    private void SecondTypeEnemy1()
    {
        GameObject enemy2_ = null;
        Quaternion rot = new Quaternion(0, 0, 0, 0);

        if (enemy2_ == null)
        {
            if (Random.value < 0.5)
            {
                enemy2_ = Instantiate(enemy2, spawnPoints[12].position, rot);
            }
            else
            {
                enemy2_ = Instantiate(Vrag, spawnPoints[13].position, rot);
            }
        }

        if (enemy2_ != null && enemy2_.gameObject.activeSelf == false)
            enemy2_ = null;
    }

}
