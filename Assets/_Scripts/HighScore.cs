using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private TextMeshProUGUI[] highscoreText = null;
    [SerializeField] private GameObject newHighscoreText = null;

    [Header("Set Dynamically")]
    public int Score = 0;

    private void Awake()
    {
        //Если значение HighScore уже существует в PlayerPrefs, прочитать его
        if (PlayerPrefs.HasKey("HighScore"))
        {
            Score = PlayerPrefs.GetInt("HighScore");
        }
        PlayerPrefs.SetInt("HighScore", Score); //Сохранить высшее достижение в PlayerPrefs
    }

    void Update()
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = "Лучший результат: " + Score;
        }

        //Обновить HighScore в PlayerPrefs, если необходимо
        if (Score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", Score);

            //Сообщение о новом рекорде
            if (newHighscoreText != null)
            {
                newHighscoreText.SetActive(true);
            }
        }
    }
}
