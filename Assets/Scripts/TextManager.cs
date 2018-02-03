using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;
    public Text HealthText;
    public Text MoneyText;
    public Text WaveText;
    public Text PreparationText;
    public GameObject panel;

    public Text ShootingFreqText;
    public Text PiercingText;
    public Text GrenadePowerText;
    public Text GrenadeFreqText;
    public Text JumpBoardText;
    public Text FloatBoardText;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
            Destroy(gameObject);

        UpdateMoneyText(0);
    }

    public void ShowStartingInfo()
    {
        StartCoroutine(ShowPreparationText());
    }

    public void UpdateHealthText(float hp, float maxHp)
    {
        int roundedHP = (int)(hp / maxHp * 100);
        string text = string.Format("HEALTH: {0}%", roundedHP);
        HealthText.text = text;
    }

    public void UpdateMoneyText(float value)
    {
        GameManager.money += value;
        string text = string.Format("RESOURCES: {0}", GameManager.money);
        MoneyText.text = text;
    }

    public void UpdateWaveText(int wave)
    {
        string text = string.Format("WAVE: {0}", wave);
        WaveText.text = text;
    }

    public IEnumerator ShowPreparationText()
    {
        PreparationText.text = "PREPARE FOR THE BATTLE!";
        yield return new WaitForSeconds(2);

        PreparationText.text = "THE BATTLE BEGINS!";
        yield return new WaitForSeconds(2);

        PreparationText.text = "3";
        yield return new WaitForSeconds(1);

        PreparationText.text = "2";
        yield return new WaitForSeconds(1);

        PreparationText.text = "1";
        yield return new WaitForSeconds(1);

        PreparationText.text = "START!";
        yield return new WaitForSeconds(1);

        GameManager.gameStarted = true;
        PreparationText.text = string.Empty;
    }

    public void ShowLevelCompleted()
    {
        StartCoroutine(LevelCompleted());
    }

    IEnumerator LevelCompleted()
    {
        PreparationText.text = "LEVEL COMPLETED!";
        yield return new WaitForSeconds(2);

        PreparationText.text = string.Empty;

        yield return new WaitForSeconds(2);
        EnablePanel(true);
    }

    public void EnablePanel(bool value)
    {
        panel.SetActive(value);
    }

    public void UpdateShootingExhaustText(float level, float cost)
    {
        if (level < 3)
            ShootingFreqText.text = string.Format("FREQUENCY: {0}", cost);
        else
            ShootingFreqText.text = string.Format("FREQUENCY: MAX");
    }

    public void UpdatePiercingShootText(float level, float cost)
    {
        if (level < 3)
            PiercingText.text = string.Format("PIERCING: {0}", cost);
        else
            PiercingText.text = string.Format("PIERCING: MAX");
    }

    public void UpdateGrenadeFreq(float level, float cost)
    {
        if (level < 3)
            GrenadeFreqText.text = string.Format("FREQUENCY: {0}", cost);
        else
            GrenadeFreqText.text = string.Format("FREQUENCY: MAX");
    }

    public void UpdateGrenadePowerText(float level, float cost)
    {
        if (level < 3)
            GrenadePowerText.text = string.Format("POWER: {0}", cost);
        else
            GrenadePowerText.text = string.Format("POWER: MAX");
    }
    
    public void UpdateJumpBoard(bool value, float cost)
    {
        if (!value)
            JumpBoardText.text = string.Format("JUMPBOARD: {0}", cost);
        else
            JumpBoardText.text = string.Format("JUMPBOARD\nBOUGHT");
    }

    public void UpdateFloatBoard(bool value, float cost)
    {
        if (!value)
            FloatBoardText.text = string.Format("FLOATBOARD: {0}", cost);
        else
            FloatBoardText.text = string.Format("FLOATBOARD\nBOUGHT");
    }
}
