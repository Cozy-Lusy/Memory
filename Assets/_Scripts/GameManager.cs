using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private GameObject[] cardPrefabs = null;
    [SerializeField] private Score score = null;
    [SerializeField] private GameObject winPanel = null;
    [SerializeField] private GameObject gameOverPanel = null;
    [SerializeField] private HighScore highScore = null;
    public int Height = 5;
    public int Width = 3;
    public float Offset = 1.5f;

    private Card _firstRevealed;
    private Card _secondRevealed;
    private Card _thirdRevealed;

    private void Start()
    {
        Time.timeScale = 1;
        SetUp();
    }

    private void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }

        if (score.Life <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    private void SetUp()
    {
        Vector3 startPos = new Vector3(0, 6, 0);

        int[] numbers = { 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4 };
        numbers = ShaffleArray(numbers);

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                //Создаем сетку из карточек, которые предварительно перемешали
                int index = j*Width + i;
                int id = numbers[index];
                GameObject card = Instantiate(cardPrefabs[id]);
                card.transform.parent = this.transform;
                card.name = "(" + i + ", " + j + ")";

                float posX = (Offset * i) + startPos.x;
                float posY = -(Offset * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, 0);
            }
        }
    }

    //Алгоритм тассования карт
    private int[] ShaffleArray(int[] num)
    {
        int[] newArray = num.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    public void CardRevealed(Card card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else if (_secondRevealed == null)
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
        else
        {
            _thirdRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.tag == _secondRevealed.tag && _thirdRevealed == null)
        {
            score.ScoreIncrement(1);
            yield break; //Не даем обнулить две карты если они совпали, чтобы открыть третью
        }
        else if (_firstRevealed.tag == _secondRevealed.tag && _secondRevealed.tag == _thirdRevealed.tag)
        {
            score.ScoreIncrement(3);

            //Запомнить высшее достижение
            if (score.ScoreCount > highScore.Score)
            {
                highScore.Score = score.ScoreCount;
            }

            _firstRevealed.DestroyMatch();
            _secondRevealed.DestroyMatch();
            _thirdRevealed.DestroyMatch();
        }
        else if (_firstRevealed.tag != _secondRevealed.tag)
        {
            yield return new WaitForSeconds(.5f);

            _firstRevealed.Unrevealed();
            _secondRevealed.Unrevealed();

            score.LifeDecrement();
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            _firstRevealed.Unrevealed();
            _secondRevealed.Unrevealed();
            _thirdRevealed.Unrevealed();

            score.LifeDecrement();
        }

        _firstRevealed = null;
        _secondRevealed = null;
        _thirdRevealed = null;
    }

    public bool CanReveal
    {
        //Условие открывания новой карты
        get
        {
            if (_firstRevealed == null || _secondRevealed == null)
            {
                return _thirdRevealed == null;
            }
            else
            {
                return _firstRevealed.tag == _secondRevealed.tag && _thirdRevealed == null;
            }
        }
    }
}
