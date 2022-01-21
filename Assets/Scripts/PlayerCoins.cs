using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoins : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    
    private int _coinsValue = 0;

    public void AddCoins(int value)
    {
        _coinsValue += value;
        _coinsText.text = _coinsValue.ToString();
    }
}
