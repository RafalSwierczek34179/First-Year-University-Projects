using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] Text MoneyValueText;
    [SerializeField] int CurrentMoney;
    public IntVariable startingMoney;

    void Start()
    {
        CurrentMoney = startingMoney.RuntimeValue;
        MoneyValueText.text = $"{CurrentMoney}";
    }
    
    /// <summary>
    /// Returns true if the player can afford the cost
    /// </summary>
    /// <param name="Cost"></param>
    /// <returns></returns>
    public bool CheckBalance(int Cost)
    {
        return !(CurrentMoney - Cost < 0);
    }

    public void GainMoney(int Amount)
    {
        CurrentMoney = CurrentMoney + Amount;
        MoneyValueText.text = $"{CurrentMoney}";
        //print($"Gain: +{Amount}");
    }

    public void SpendMoney(int Cost)
    {
        CurrentMoney = CurrentMoney - Cost;
        MoneyValueText.text = $"{CurrentMoney}";
        //print($"Lost: -{Cost}");
    }
}