using UnityEngine;

public abstract class en_state_base : MonoBehaviour
{
    public string stateID;

    [SerializeField]
    private en_brain brain;

    [SerializeField]
    protected en_info_base info;

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected virtual void ChangeState(string newState)
    {
        brain.ChangeState(newState);
    }
}
