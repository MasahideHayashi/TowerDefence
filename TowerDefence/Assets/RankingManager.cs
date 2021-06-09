using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class RankingManager : MonoBehaviour
{
    [SerializeField] Text scoreText;


    void Start()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Score");

        query.OrderByDescending("score");
        query.Limit = 5;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

            //検索成功したら
            if (e == null)
            {
                scoreText.text = "";
                if (objList.Count == 0)
                {
                    return;
                }
                if (objList.Count == 1)
                {   
                scoreText.text += "1st Score: " + objList[0]["score"] + "\n";
                }
                if (objList.Count == 2)
                {
                    scoreText.text += "1st Score: " + objList[0]["score"] + "\n";
                    scoreText.text += "2nd Score: " + objList[1]["score"] + "\n";
                }
                if (objList.Count == 3)
                {
                    scoreText.text += "1st Score: " + objList[0]["score"] + "\n";
                    scoreText.text += "2nd Score: " + objList[1]["score"] + "\n";
                    scoreText.text += "3rd Score: " + objList[2]["score"] + "\n";
                }
                if (objList.Count == 4)
                {
                    scoreText.text += "1st Score: " + objList[0]["score"] + "\n";
                    scoreText.text += "2nd Score: " + objList[1]["score"] + "\n";
                    scoreText.text += "3rd Score: " + objList[2]["score"] + "\n";
                    scoreText.text += "4th Score: " + objList[3]["score"] + "\n";
                }
                if (objList.Count == 5)
                {
                    scoreText.text += "1st Score: " + objList[0]["score"] + "\n";
                    scoreText.text += "2nd Score: " + objList[1]["score"] + "\n";
                    scoreText.text += "3rd Score: " + objList[2]["score"] + "\n";
                    scoreText.text += "4th Score: " + objList[3]["score"] + "\n";
                    scoreText.text += "5th Score: " + objList[4]["score"] + "\n";
                }
            }
        });
    }

    
}
