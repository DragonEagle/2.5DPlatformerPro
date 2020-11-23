using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _livesText;
    [SerializeField]
    private Text _coinsText;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (_player)
        {
            if (_livesText)
            {
                _livesText.text = "Lives: " + _player.Lives;
            }
            if (_coinsText)
            {
                _coinsText.text = "Coins: " + _player.PlayerCoins;
            }
        }
    }
}
