using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

[System.Serializable]
public class StateList
{
    public StateList()
    {
        Transitions = new List<AnimatorStateTransition>();
    }
    public List<AnimatorStateTransition> Transitions;
    
}

[System.Serializable]
public class TransitionGroupData : ScriptableObject {
    public List<string> TransitionGroupsNames;
    public List<StateList> TransitionGroups;
}
