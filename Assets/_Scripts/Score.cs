using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private TextMeshProUGUI[] scoreText = null;
    [SerializeField] private TextMeshProUGUI lifeText = null;
    [SerializeField] private int life = 5;

    private int _score = 0;

    public void ScoreIncrement(int point)
    {
        _score += point * life;

        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = $"{_score}";
        }
    }

    public void LifeDecrement()
    {
        life--;
        lifeText.text = "Жизни: " + Life;
    }

    public int Life
    {
        get { return life; }
    }

    public int ScoreCount
    {
        get { return _score; }
    }
}
