using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TMP_Text score;

    public TMP_Text hp;
    public TMP_Text damage;
    public TMP_Text speed;
    public TMP_Text exp;

    public GameObject GameOverPanel;
    public TMP_Text GameOverScore;

    int scoreByTime = 0;
    int scoreByOther = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hp.text = System.Math.Round(GameManager.instance.player.hullHitPoints) + "/" + System.Math.Round(GameManager.instance.player.hullMaxStrength);
        damage.text = System.Math.Round(GameManager.instance.player.BulletDamage(), 1).ToString() + " (rate:" + System.Math.Round(1 / GameManager.instance.player.fireRate, 3).ToString() + ")";
        speed.text = System.Math.Round(GameManager.instance.player.speed, 1).ToString();
        exp.text = GameManager.instance.player.exp.ToString();




        if (GameManager.instance.player.state == CharacterMovement.eState.DEAD)
        {
            GameOverPanel.SetActive(true);
        }
        else
        {
            scoreByTime = (int)System.Math.Round(GameManager.instance.getPlayTime()) * 100;
            score.text = GetScore().ToString();
            GameOverScore.text = "Score: " + GetScore();
        }

    }

    public int GetScore()
    {
        return scoreByTime + scoreByOther;
    }

    public void ScoreIncrement(int p_value)
    {
        scoreByOther += p_value;
    }
}
