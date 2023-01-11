using System.Collections.Generic;
using UnityEngine;

public class en_brain : MonoBehaviour
{
    [SerializeField]
    private List<en_state_base> states;

    public en_state_base currentState;
    private string currentStateID;

    [SerializeField]
    private en_state_base initialState;

    protected virtual void OnEnable()
    {
        Init();
    }

    protected virtual void Init()
    {
        currentState = initialState;
        currentStateID = initialState.stateID;
        currentState.enabled = true;
    }

    public void ChangeState(string newStateID)
    {
        en_state_base newState;

        try
        {
            newState = GetStateByID(newStateID);
        }
        catch
        {
            print("Invalid stateID passed to ChangeState");
            return;
        }

        currentState.enabled = false;
        currentState = newState;
        currentState.enabled = true;
        currentStateID = newStateID;
    }

    private en_state_base GetStateByID(string ID)
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].stateID == ID)
            {
                return states[i];
            }
        }

        return null;
    }
}
