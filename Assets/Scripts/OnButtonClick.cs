using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonClick : MonoBehaviour
{
    public GameObject LevelMan;

    public void OnClick(int value)
    {
        Upgrader.instance.ChooseUpgradeFunction(value);
    }

    public void LoadNewLevel()
    {
        LevelMan.SendMessage("LoadLevel");
    }
}
