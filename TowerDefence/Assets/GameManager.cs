using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NCMB;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject selectPanel;
    [SerializeField] GameObject levelUpPanel;
    [SerializeField] List<GameObject> fieldButtons;
    GameObject currentFieldButton;
    [SerializeField] List<GameObject> levelUpButtons;
    [SerializeField] Text[] levelUpText;
    [SerializeField] GameObject[] playerPrefabs;
    bool[] playerExist = { false, false, false, false, false };
    GameObject[] player = { null, null, null, null, null };
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject nextLevelButton;
    [SerializeField] GameObject sendScoreButton;
    [SerializeField] GameObject toTitleButton;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject stageLevelText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameClearText;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject[] enemy;
    [SerializeField] Text scoreText;
    [SerializeField] Text coinText;
    [SerializeField] GameObject timer;

    int score = 0;
    int coin = 0;
    int stageLevel = 1;


    private void Start()
    {
        startText.SetActive(false);
        selectPanel.SetActive(false);
        levelUpPanel.SetActive(false);
        gameOverText.SetActive(false);
        gameClearText.SetActive(false);
        startButton.SetActive(true);
        restartButton.SetActive(false);
        sendScoreButton.SetActive(false);
        toTitleButton.SetActive(true);
        scoreText.text = string.Format("SCORE:{0}", score);
        scoreText.text = string.Format("COIN:{0}", coin);
        stageLevelText.GetComponent<Text>().text = "LEVEL " + stageLevel;
        stageLevelText.SetActive(false);


    }

    private void Update()
    {

    }

    public void OnClickFieldButton(int number)
    {
        selectPanel.SetActive(true);
        currentFieldButton = fieldButtons[number];
    }

    public void OnClickSelectButton(int number)
    {
        int index = fieldButtons.IndexOf(currentFieldButton);
        if (!playerExist[index])
        {
            player[index] = Instantiate(playerPrefabs[number], currentFieldButton.transform.position, Quaternion.identity);
            player[index].transform.position = new Vector3(player[index].transform.position.x - 0.2f, player[index].transform.position.y + 0.7f, 0);
            playerExist[index] = true;
        }
        else
        {
            Destroy(player[index]);
            player[index] = null;
            player[index] = Instantiate(playerPrefabs[number], currentFieldButton.transform.position, Quaternion.identity);
            player[index].transform.position = new Vector3(player[index].transform.position.x - 0.2f, player[index].transform.position.y + 0.7f, 0);
        }
        selectPanel.SetActive(false);
    }

    public void OnClickStartButton()
    {
        for (int i = 0; i < playerExist.Length; i++)
        {
            if (!playerExist[i]) return;
        }

        StartCoroutine("GameStart", stageLevel);
    }

    IEnumerator GameStart(int level)
    {
        foreach (GameObject button in fieldButtons)
        {
            button.SetActive(false);
        }
        startButton.SetActive(false);
        toTitleButton.SetActive(false);
        startText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        startText.SetActive(false);
        stageLevelText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        stageLevelText.SetActive(false);
        timer.SetActive(true);
        while (timer.GetComponent<Timer>().time > 0.5f)
        {
            float spawnTime = Random.Range(1, 4);
            Instantiate(enemy[level - 1], spawnPoint.transform, false);
            yield return new WaitForSeconds(spawnTime);
        }
        if (level == 3) GameClear();
        GameEnd();

    }

    void GameEnd()
    {
        if (timer.GetComponent<Timer>().time <= 0)
        {
            GameObject[] g;
            g = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in g)
            {
                Destroy(obj);
            }
        }
        timer.SetActive(false);
        timer.GetComponent<Timer>().Initialize();
        levelUpPanel.SetActive(true);
        nextLevelButton.SetActive(true);
        toTitleButton.SetActive(true);
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        sendScoreButton.SetActive(true);
        toTitleButton.SetActive(true);
        timer.SetActive(false);
        StopAllCoroutines();
        GameObject[] g;
        g = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in g)
        {
            Destroy(obj);
        }
        return;
    }

    public void GameClear()
    {
        levelUpPanel.SetActive(false);
        gameClearText.SetActive(true);
        sendScoreButton.SetActive(true);
        toTitleButton.SetActive(true);
    }

    public void OnClickRestartButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

    }

    public void UpdateInfo(int s, int c)
    {
        score += s;
        scoreText.text = string.Format("SCORE:{0}", score);
        coin += c;
        coinText.text = string.Format("COIN:{0}", coin);
    }

    public void OnClickLevelUpButton(int num)
    {
        foreach (GameObject playerObj in player)
        {
            if (playerObj.tag == "Player" + (num + 1) && coin >= 10)
            {
                coin -= 10;
                coinText.text = string.Format("COIN:{0}", coin);
                playerObj.GetComponent<PlayerManager>().LevelUp();
                playerObj.GetComponent<PlayerManager>().UpdataText(levelUpText[num]);

            }
        }
    }

    public void OnClickNextLevelButton()
    {
        stageLevel++;
        stageLevelText.GetComponent<Text>().text = "LEVEL " + stageLevel;
        levelUpPanel.SetActive(false);
        nextLevelButton.SetActive(false);
        StartCoroutine("GameStart", stageLevel);
    }

    public void OnClickSendScoreButton()
    {
        //API
        NCMBObject obj = new NCMBObject("Score");
        obj["score"] = score;

        obj.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                //エラー処理
                Debug.Log("failed");
            }
            else
            {
                //成功時の処理
                Debug.Log("success");
            }
        });
    }
}
