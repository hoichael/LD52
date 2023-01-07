using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_health_manager : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] DevManager devManager;

    private void Start()
    {
        refs.state.hp = refs.settings.maxHP;
    }

    public void HandleDamage(int dmgAmount)
    {
        print("Damage Taken! (" + dmgAmount + ")");
        refs.state.hp -= dmgAmount;
        if(refs.state.hp <= 0)
        {
            devManager.Reload();
        }
    }
}
