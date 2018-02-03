using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrader : MonoBehaviour
{
    public static Upgrader instance;

    public Button frequency;
    public Button lifeTime;
    public Button grPower;
    public Button grFreq;
    public Button jBoard;
    public Button fBoard;


    private int shootingExhaustLevel = 0;
    private int bulletLifeTimeLevel = 0;
    private int grenadeFreqLevel = 0;
    private int grenadePowerLevel = 0;
    private bool jumpBoard = false;
    private bool floatBoard = false;

    private float[] shootingExhaustCosts;
    private float[] bulletLifeTimeCosts;
    private float[] grenadeFreqCosts;
    private float[] grenadePowerCosts;
    private float jumpBoardCost;
    private float floatBoardCost;

    private float[] shootingExhaustValues;
    private float[] bulletLifeTimeValues;
    private float[] grenadeFreqValues;
    private float[] grenadePowerValues;


    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
            Destroy(gameObject);

        shootingExhaustCosts = new float[5] { 0, 50, 200, 500, 0 };
        bulletLifeTimeCosts = new float[5] { 0, 50, 200, 500, 0 };
        grenadeFreqCosts = new float[5] { 0, 50, 200, 500, 0 };
        grenadePowerCosts = new float[5] { 0, 50, 200, 500, 0 };
        jumpBoardCost = 200;
        floatBoardCost = 200;

        shootingExhaustValues = new float[5] { 0, 0.37f, 0.24f, 0.11f, 0 };
        bulletLifeTimeValues = new float[5] { 0, 0.35f, 0.5f, 0.65f, 0 };
        grenadeFreqValues = new float[5] { 0, 5, 3, 2, 0 };
        grenadePowerValues = new float[5] { 0, 30, 24, 18, 0 };

        TextManager.instance.UpdateShootingExhaustText(shootingExhaustLevel, shootingExhaustCosts[shootingExhaustLevel + 1]);
        TextManager.instance.UpdatePiercingShootText(bulletLifeTimeLevel, bulletLifeTimeCosts[bulletLifeTimeLevel + 1]);
        TextManager.instance.UpdateGrenadeFreq(grenadeFreqLevel, grenadeFreqCosts[grenadeFreqLevel + 1]);
        TextManager.instance.UpdateGrenadePowerText(grenadePowerLevel, grenadePowerCosts[grenadePowerLevel + 1]);
        TextManager.instance.UpdateJumpBoard(jumpBoard, jumpBoardCost);
        TextManager.instance.UpdateFloatBoard(floatBoard, floatBoardCost);
    }

    public void ChooseUpgradeFunction(int value)
    {
        switch(value)
        {
            case 0:
                UpgradeShootingExhaust();
                break;
            case 1:
                UpgradeBulletLifeTime();
                break;
            case 2:
                UpgradeGrenadePower();
                break;
            case 3:
                UpgradeGrenadeFreq();
                break;
            case 4:
                UpgradeJumpBoard();
                break;
            case 5:
                UpgradeFloatBoard();
                break;
            case 6:
                EndShopping();
                break;
        }
    }

    void UpgradeShootingExhaust()
    {
        if (shootingExhaustLevel >= 3)
            return;

        if (GameManager.money >= shootingExhaustCosts[shootingExhaustLevel + 1])
        {
            GameManager.money -= shootingExhaustCosts[shootingExhaustLevel + 1];
            shootingExhaustLevel++;
            GameManager.shootingExhaust = shootingExhaustValues[shootingExhaustLevel];

            if (shootingExhaustLevel == 3)
                frequency.enabled = false;
            TextManager.instance.UpdateShootingExhaustText(shootingExhaustLevel, shootingExhaustCosts[shootingExhaustLevel + 1]);
        }
    }

    void UpgradeBulletLifeTime()
    {
        if (bulletLifeTimeLevel >= 3)
            return;


        if (GameManager.money >= bulletLifeTimeCosts[bulletLifeTimeLevel + 1])
        {
            GameManager.money -= bulletLifeTimeCosts[bulletLifeTimeLevel + 1];
            bulletLifeTimeLevel++;
            GameManager.bulletLifeTime = bulletLifeTimeValues[bulletLifeTimeLevel];

            if (bulletLifeTimeLevel == 3)
                lifeTime.enabled = false;

            TextManager.instance.UpdatePiercingShootText(bulletLifeTimeLevel, bulletLifeTimeCosts[bulletLifeTimeLevel + 1]);
        }
    }

    void UpgradeGrenadeFreq()
    {
        if (grenadeFreqLevel >= 3)
            return;

        if (GameManager.money >= grenadeFreqCosts[grenadeFreqLevel + 1])
        {
            GameManager.money -= grenadeFreqCosts[grenadeFreqLevel + 1];
            grenadeFreqLevel++;
            GameManager.grenadeFreq = grenadeFreqValues[grenadeFreqLevel];

            if (grenadeFreqLevel == 3)
            grFreq.enabled = false;

            TextManager.instance.UpdateGrenadeFreq(grenadeFreqLevel, grenadeFreqCosts[grenadeFreqLevel + 1]);
        }
    }

    void UpgradeGrenadePower()
    {
        if (grenadePowerLevel >= 3)
            return;

        if (GameManager.money >= grenadePowerCosts[grenadePowerLevel + 1])
        {
            GameManager.money -= grenadePowerCosts[grenadePowerLevel + 1];
            grenadePowerLevel++;
            GameManager.grenadePow = grenadePowerValues[grenadePowerLevel];

            if (grenadePowerLevel == 3)
            grPower.enabled = false;

            TextManager.instance.UpdateGrenadePowerText(grenadePowerLevel, grenadePowerCosts[grenadePowerLevel + 1]);
        }
    }

    void UpgradeJumpBoard()
    {
        if (jumpBoard)
            return;

        if (GameManager.money >= jumpBoardCost)
        {
            GameManager.money -= jumpBoardCost;
            jumpBoard = true;
            jBoard.enabled = false;
            GameManager.jumpBoard = jumpBoard;

            TextManager.instance.UpdateJumpBoard(jumpBoard, jumpBoardCost);
        }
    }

    void UpgradeFloatBoard()
    {
        if (floatBoard)
            return;

        if (GameManager.money >= floatBoardCost)
        {
            GameManager.money -= floatBoardCost;
            floatBoard = true;
            fBoard.enabled = false;
            GameManager.jumpBoard = floatBoard;

            TextManager.instance.UpdateFloatBoard(floatBoard, floatBoardCost);
        }
    }

    void EndShopping()
    {

    }
}