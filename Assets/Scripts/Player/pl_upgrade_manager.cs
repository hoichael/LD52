using System.Collections.Generic;
using UnityEngine;

public class pl_upgrade_manager : MonoBehaviour
{
    [SerializeField] pl_state state;
    [SerializeField] pl_refs refs;
    [SerializeField] List<pl_upgrade> healthUpgrades;
    [SerializeField] List<pl_upgrade> moveUpdgrades;
    [SerializeField] List<pl_upgrade> jumpUpgrades;

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
            return new pl_upgrade_message(false, "Insufficent Funds");
        }

        info.currentUpgradeStep++;
        state.money -= info.upgradeList[info.currentUpgradeStep].cost;

        ApplyUpgrades();

        return new pl_upgrade_message(false, key + " Upgraded To Level " + info.currentUpgradeStep);
    }

    private void ApplyUpgrades() // upgrade/set all because fuck needless conditionals :^)
    {
        refs.settings.moveForceBase = upgradeInfoDict["Move"].upgradeList[upgradeInfoDict["Move"].currentUpgradeStep].value;
        refs.settings.jumpForceBase = upgradeInfoDict["Jump"].upgradeList[upgradeInfoDict["Jump"].currentUpgradeStep].value;
        refs.settings.maxHP = upgradeInfoDict["Health"].upgradeList[upgradeInfoDict["Health"].currentUpgradeStep].value;
        g_refs.Instance.plHealth.UpdateUI();
    }

    public pl_upgrade_message Hover()
    {
        return new pl_upgrade_message(true, "yoyo whaddup");
    }

    private void SetupData()
    {
        pl_upgrade_info infoHealth = new pl_upgrade_info(healthUpgrades);
        pl_upgrade_info infoMove = new pl_upgrade_info(moveUpdgrades);
        pl_upgrade_info infoJump = new pl_upgrade_info(jumpUpgrades);

        upgradeInfoDict = new Dictionary<string, pl_upgrade_info>();
        upgradeInfoDict.Add("Health", infoHealth);
        upgradeInfoDict.Add("Hove", infoMove);
        upgradeInfoDict.Add("Hump", infoJump);
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
