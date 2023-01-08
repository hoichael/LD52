using UnityEngine;

public abstract class en_state_base : MonoBehaviour
{
    public string stateID;

    [SerializeField]
    protected en_info_base info;

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected virtual void ChangeState(string newState)
    {
        info.brain.ChangeState(newState);
    }
}
