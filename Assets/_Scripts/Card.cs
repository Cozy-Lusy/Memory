using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private GameObject cardFront = null;
    public float ShowTime = 3f;

    private GameManager _gm;
    private bool _isUnrevial = true;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //Карты в начале открываются на ShowTime секунд
        ShowTime -= Time.deltaTime;
        if (ShowTime <= 0 && _isUnrevial)
        {
            Unrevealed();
            _isUnrevial = false;
        }
    }

    private void OnMouseDown()
    {
        if (!cardFront.activeSelf && _gm.CanReveal)
        {
            cardFront.SetActive(true);
            _gm.CardRevealed(this);
        }
    }

    public void Unrevealed()
    {
        cardFront.SetActive(false);
    }

    public void DestroyMatch()
    {
        Destroy(gameObject, 0.2f);
    }
}
