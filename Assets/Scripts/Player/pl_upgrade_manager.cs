using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pl_upgrade_manager : MonoBehaviour
{
    [SerializeField] pl_state state;
    [SerializeField] pl_refs refs;
    [SerializeField] List<pl_upgrade> healthUpgrades;
    [SerializeField] List<pl_upgrade> moveUpdgrades;
    [SerializeField] List<pl_upgrade> jumpUpgrades;

    [SerializeField] TextMeshPro moneyTextEl;

    Dictionary<string, pl_upgrade_info> upgradeInfoDict;

    private void Start()
    {
        SetupData();
        ApplyUpgrades();
    }

    public pl_upgrade_message TryUpgrade(string key)
    {
        pl_upgrade_info info = upgradeInfoDict[key];

        if (info.currentUpgradeStep >= info.upgradeList.Count - 1)
        {
            return new pl_upgrade_message(false, key + " Already Maxed Out");
        }

        if(state.money < info.upgradeList[info.currentUpgradeStep + 1].cost)
        {
            return new pl_upgrade_message(false, "Insufficent Funds (" + info.upgradeList[info.currentUpgradeStep + 1].cost + " needed)");
        }

        info.currentUpgradeStep++;
        state.money -= info.upgradeList[info.currentUpgradeStep].cost;
        moneyTextEl.text = "CASH: " + state.money;

        ApplyUpgrades();

        return new pl_upgrade_message(false, key + " Upgraded To Level " + info.currentUpgradeStep);
    }

    private void ApplyUpgrades() // upgrade/set all because fuck """needless""" conditionals :^)
    {
        refs.settings.moveForceBase = upgradeInfoDict["Move"].upgradeList[upgradeInfoDict["Move"].currentUpgradeStep].value;
        g_refs.Instance.sessionData.upgradeLevelMove = upgradeInfoDict["Move"].currentUpgradeStep;

        refs.settings.jumpForceBase = upgradeInfoDict["Jump"].upgradeList[upgradeInfoDict["Jump"].currentUpgradeStep].value;
        g_refs.Instance.sessionData.upgradeLevelJump = upgradeInfoDict["Jump"].currentUpgradeStep;

        refs.settings.maxHP = upgradeInfoDict["Health"].upgradeList[upgradeInfoDict["Health"].currentUpgradeStep].value;
        g_refs.Instance.sessionData.upgradeLevelHealth = upgradeInfoDict["Health"].currentUpgradeStep;
        g_refs.Instance.plHealth.UpdateUI();
    }

    public pl_upgrade_message Hover()
    {
        return new pl_upgrade_message(true, "yoyo whaddup");
    }

    private void SetupData()
    {
        pl_upgrade_info infoHealth = new pl_upgrade_info(healthUpgrades);
        infoHealth.currentUpgradeStep = g_refs.Instance.sessionData.upgradeLevelHealth;

        pl_upgrade_info infoMove = new pl_upgrade_info(moveUpdgrades);
        infoMove.currentUpgradeStep = g_refs.Instance.sessionData.upgradeLevelMove;

        pl_upgrade_info infoJump = new pl_upgrade_info(jumpUpgrades);
        infoJump.currentUpgradeStep = g_refs.Instance.sessionData.upgradeLevelJump;

        upgradeInfoDict = new Dictionary<string, pl_upgrade_info>();
        upgradeInfoDict.Add("Health", infoHealth);
        upgradeInfoDict.Add("Move", infoMove);
        upgradeInfoDict.Add("Jump", infoJump);
    }
}

[System.Serializable]
public class pl_upgrade
{
    public int value;
    public int cost;
}

public class pl_upgrade_info
{
    public List<pl_upgrade> upgradeList;
    public int currentUpgradeStep;

    public pl_upgrade_info(List<pl_upgrade> upgradeList)
    {
        this.upgradeList = upgradeList;
        currentUpgradeStep = 0;
    }
}

public class pl_upgrade_message
{
    public bool success;
    public string message;

    public pl_upgrade_message(bool success, string message)
    {
        this.success = success;
        this.message = message;
    }
}
