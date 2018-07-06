using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimationTransitionProperties : EditorWindow{
    
    AnimatorStateTransition[] AnimatorStateTransitions;
    AnimatorController ac;
    TransitionGroupData tgd;
    Vector2 scrollPos;
    bool showTransitions = false;
    int showTransitionsInGroup = -1;
    int editTransitionsInGroup = -1;
    bool showGroups = false;
    List<string> TransitionGroupsNames;
    List<StateList> TransitionGroups;
    string TransitionGroupName = "";
    bool hasExitTime;
    float exitTime;
    bool hasFixedDuration;
    float duration;
    float offset;
    bool F;
[MenuItem("Window/AnimationTransitionProperties")]
    public static void ShowWindow()
    {
        GetWindow<AnimationTransitionProperties>("AnimationTransitionProperties");
    }
    private void OnEnable()
    {
        tgd = CreateInstance<TransitionGroupData>();
        
        tgd = (TransitionGroupData)EditorGUIUtility.Load("TransitionGroupData.asset") as TransitionGroupData;
        
        if (tgd == null)
        {
            //tgd = CreateInstance<TransitionGroupData>();
            tgd.TransitionGroups = new List<StateList>();
            tgd.TransitionGroupsNames = new List<string>();
            if (!AssetDatabase.Contains(tgd))
            {
                AssetDatabase.CreateAsset(tgd, "Assets/Editor Default Resources/TransitionGroupData.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
            }
            Debug.Log("mo");
        }
        
        TransitionGroups = tgd.TransitionGroups;
        TransitionGroupsNames = tgd.TransitionGroupsNames;
    }
    private void OnDisable()
    {

        //tgd.TransitionGroups = TransitionGroups;
        //tgd.TransitionGroupsNames = TransitionGroupsNames;
        EditorUtility.SetDirty(tgd);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
    }
    
    private void OnGUI()
    {
        
        if(Selection.activeGameObject != null)
        {
            if (GUILayout.Button("Get AnimatorController from active GameObject"))
            {
                if (Selection.activeGameObject.GetComponent<Animator>())
                {
                    ac = Selection.activeGameObject.GetComponent<Animator>().runtimeAnimatorController as AnimatorController;

                }
                Debug.Log("AnimatorController: "+ac.name);
            }
        }
       
        if(ac != null)
        {
            if (GUILayout.Button("Get Transitions")) {
                int iTransitions = 0;
                AnimatorStateMachine sm = ac.layers[0].stateMachine;
                for (int i = 0; i < sm.states.Length; i++)
                {
                    AnimatorState state = sm.states[i].state;
                    for (int j = 0; j < state.transitions.Length; j++)
                    {
                        iTransitions++;
                    }
                }
                AnimatorStateTransitions = new AnimatorStateTransition[iTransitions];
                iTransitions = 0;
                for (int i = 0; i < sm.states.Length; i++)
                {
                    AnimatorState state = sm.states[i].state;
                    for (int j = 0; j < state.transitions.Length; j++)
                    {
                        AnimatorStateTransitions[iTransitions] = state.transitions[j];
                        iTransitions++;
                    }
                }
                Debug.Log("Found "+AnimatorStateTransitions.Length+" Transitions");
            }
        }
        if (AnimatorStateTransitions != null)
        {
            if (AnimatorStateTransitions.Length > 0)
            {

                if (!showTransitions)
                {
                    if (GUILayout.Button("Show All Transitions"))
                    {
                        showTransitions = true;
                        showTransitionsInGroup = -1;
                        editTransitionsInGroup = -1;
                    }
                }
                else
                {
                    if (GUILayout.Button("Hide All Transitions"))
                    {
                        showTransitions = false;

                    }
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                    for (int i = 0; AnimatorStateTransitions.Length > i; i++)
                    {

                        if (AnimatorStateTransitions[i] != null)
                        {

                            EditorGUILayout.LabelField(AnimatorStateTransitions[i].name);
                            EditorGUI.indentLevel++;
                            AnimatorStateTransitions[i].hasExitTime = EditorGUILayout.Toggle("HasExitTime:", AnimatorStateTransitions[i].hasExitTime);
                            AnimatorStateTransitions[i].exitTime = EditorGUILayout.FloatField("ExitTime:", AnimatorStateTransitions[i].exitTime);
                            AnimatorStateTransitions[i].hasFixedDuration = EditorGUILayout.Toggle("HasFixedDuration:", AnimatorStateTransitions[i].hasFixedDuration);
                            AnimatorStateTransitions[i].duration = EditorGUILayout.FloatField("Duration:", AnimatorStateTransitions[i].duration);
                            AnimatorStateTransitions[i].offset = EditorGUILayout.FloatField("Offset:", AnimatorStateTransitions[i].offset);
                            AnimatorStateTransitions[i].interruptionSource = (TransitionInterruptionSource)EditorGUILayout.EnumPopup(AnimatorStateTransitions[i].interruptionSource);
                            if (TransitionGroups != null)
                            {
                                EditorGUILayout.BeginHorizontal();
                                for(int j = 0; j < TransitionGroups.Count; j++)
                                {
                                        if (TransitionGroups[j].Transitions.Contains(AnimatorStateTransitions[i]))
                                        {
                                            if (GUILayout.Button("Remove From " + TransitionGroupsNames[j] + " Group"))
                                            {
                                                TransitionGroups[j].Transitions.Remove(AnimatorStateTransitions[i]);
                                            
                                            }
                                        }
                                        else
                                        {
                                            if (GUILayout.Button("Add To " + TransitionGroupsNames[j] + " Group"))
                                            {
                                                TransitionGroups[j].Transitions.Add(AnimatorStateTransitions[i]);
                                            
                                            }
                                        }
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            EditorGUI.indentLevel--;

                        }

                    }
                    EditorGUILayout.EndScrollView();
                }
                if (!showGroups)
                {
                    if (GUILayout.Button("Show Groups"))
                    {
                        showGroups = true;

                    }
                }
                else
                {
                    if (GUILayout.Button("Hide Groups"))
                    {
                        showGroups = false;

                    }
                    if (TransitionGroups != null)
                    {
                        for (int i = 0; i < TransitionGroups.Count; i++)
                        {
                            EditorGUILayout.LabelField(i + "." + TransitionGroupsNames[i]);
                            if (TransitionGroups[i].Transitions.Count > 0) {
                                if (showTransitionsInGroup == i)
                                {
                                    if (GUILayout.Button("Hide all Transitions on Group"))
                                    {
                                        showTransitionsInGroup = -1;
                                    }
                                }
                                else
                                {
                                    if (GUILayout.Button("Show all Transitions on Group"))
                                    {
                                        showTransitionsInGroup = i;
                                    }
                                }
                                if (editTransitionsInGroup == i)
                                {
                                    if (GUILayout.Button("Hide edit all Transitions on Group"))
                                    {
                                        editTransitionsInGroup = -1;
                                    }
                                }
                                else
                                {
                                    if (GUILayout.Button("Edit all Transitions on Group"))
                                    {
                                        editTransitionsInGroup = i;
                                        F = true;
                                    }
                                }
                            }
                            if (GUILayout.Button("Remove Group"))
                            {
                                TransitionGroups.Remove(TransitionGroups[i]);
                                TransitionGroupsNames.Remove(TransitionGroupsNames[i]);
                                
                            }
                            
                        }
                    }
                    TransitionGroupName = EditorGUILayout.TextField("TransitionGroup name", TransitionGroupName);
                    if (GUILayout.Button("Add Group"))
                    {
                        if (TransitionGroupName != "")
                        {
                            TransitionGroupsNames.Add(TransitionGroupName);
                            TransitionGroups.Add(new StateList());
                            TransitionGroupName = "";
                            
                        }
                    }
                    if(showTransitionsInGroup > -1 )
                    {
                        if(TransitionGroups[showTransitionsInGroup] != null) {
                            editTransitionsInGroup = -1;
                            showTransitions = false;
                            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                            for (int i = 0; TransitionGroups[showTransitionsInGroup].Transitions.Count > i; i++)
                            {

                                if (TransitionGroups[showTransitionsInGroup].Transitions[i] != null)
                                {

                                    EditorGUILayout.LabelField(TransitionGroups[showTransitionsInGroup].Transitions[i].name);
                                    EditorGUI.indentLevel++;
                                    TransitionGroups[showTransitionsInGroup].Transitions[i].hasExitTime = EditorGUILayout.Toggle("HasExitTime:", TransitionGroups[showTransitionsInGroup].Transitions[i].hasExitTime);
                                    TransitionGroups[showTransitionsInGroup].Transitions[i].exitTime = EditorGUILayout.FloatField("ExitTime:", TransitionGroups[showTransitionsInGroup].Transitions[i].exitTime);
                                    TransitionGroups[showTransitionsInGroup].Transitions[i].hasFixedDuration = EditorGUILayout.Toggle("HasFixedDuration:", TransitionGroups[showTransitionsInGroup].Transitions[i].hasFixedDuration);
                                    TransitionGroups[showTransitionsInGroup].Transitions[i].duration = EditorGUILayout.FloatField("Duration:", TransitionGroups[showTransitionsInGroup].Transitions[i].duration);
                                    TransitionGroups[showTransitionsInGroup].Transitions[i].offset = EditorGUILayout.FloatField("Offset:", TransitionGroups[showTransitionsInGroup].Transitions[i].offset);
                                    TransitionGroups[showTransitionsInGroup].Transitions[i].interruptionSource = (TransitionInterruptionSource)EditorGUILayout.EnumPopup(TransitionGroups[showTransitionsInGroup].Transitions[i].interruptionSource);
                                    if (TransitionGroups != null)
                                    {
                                        EditorGUILayout.BeginHorizontal();
                                        for (int j = 0; j < TransitionGroups.Count; j++)
                                        {
                                            
                                                if (TransitionGroups[j].Transitions.Contains(AnimatorStateTransitions[i]))
                                                {
                                                    if (GUILayout.Button("Remove From " + TransitionGroupsNames[j] + " Group"))
                                                    {
                                                        TransitionGroups[j].Transitions.Remove(AnimatorStateTransitions[i]);
                                                    }
                                                }
                                            
                                            else
                                            {
                                                if (GUILayout.Button("Add To " + TransitionGroupsNames[j] + " Group"))
                                                {
                                                    TransitionGroups[j].Transitions.Add(AnimatorStateTransitions[i]);
                                                }
                                            }
                                        }
                                        EditorGUILayout.EndHorizontal();
                                    }
                                    EditorGUI.indentLevel--;

                                }

                            }
                            EditorGUILayout.EndScrollView();
                        }
                    }
                    else if (editTransitionsInGroup > -1)
                    {
                        if (TransitionGroups[editTransitionsInGroup] != null)
                        {
                            showTransitionsInGroup = -1;
                            showTransitions = false;
                            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                            EditorGUILayout.LabelField("Edit every transitions properties in group");
                            EditorGUI.indentLevel++;
                            if (F) {
                                hasExitTime = TransitionGroups[editTransitionsInGroup].Transitions[0].hasExitTime;
                                exitTime = TransitionGroups[editTransitionsInGroup].Transitions[0].exitTime;
                                hasFixedDuration = TransitionGroups[editTransitionsInGroup].Transitions[0].hasFixedDuration;
                                duration = TransitionGroups[editTransitionsInGroup].Transitions[0].duration;
                                offset = TransitionGroups[editTransitionsInGroup].Transitions[0].offset;
                                F = false;
                            }
                            hasExitTime = EditorGUILayout.Toggle("HasExitTime:", hasExitTime);
                            exitTime = EditorGUILayout.FloatField("ExitTime:", exitTime);
                            hasFixedDuration = EditorGUILayout.Toggle("HasFixedDuration:", hasFixedDuration);
                            duration = EditorGUILayout.FloatField("Duration:", duration);
                            offset = EditorGUILayout.FloatField("Offset:", offset);
                            TransitionInterruptionSource interruptionSource = (TransitionInterruptionSource)EditorGUILayout.EnumPopup(TransitionGroups[editTransitionsInGroup].Transitions[0].interruptionSource);
                            EditorGUI.indentLevel--;
                            if (GUILayout.Button("!!!!!!SAVE!!!!!! NOT REVERSABLE"))
                            {
                                for (int i = 0; TransitionGroups[editTransitionsInGroup].Transitions.Count > i; i++)
                                {

                                    if (TransitionGroups[editTransitionsInGroup].Transitions[i] != null)
                                    {

                                        EditorGUILayout.LabelField(TransitionGroups[editTransitionsInGroup].Transitions[i].name);
                                        EditorGUI.indentLevel++;
                                        TransitionGroups[editTransitionsInGroup].Transitions[i].hasExitTime = hasExitTime;
                                        TransitionGroups[editTransitionsInGroup].Transitions[i].exitTime = exitTime;
                                        TransitionGroups[editTransitionsInGroup].Transitions[i].hasFixedDuration = hasFixedDuration;
                                        TransitionGroups[editTransitionsInGroup].Transitions[i].duration = duration;
                                        TransitionGroups[editTransitionsInGroup].Transitions[i].offset = offset;
                                        TransitionGroups[editTransitionsInGroup].Transitions[i].interruptionSource = interruptionSource;

                                        EditorGUI.indentLevel--;

                                    }

                                }
                            }
                            EditorGUILayout.EndScrollView();
                        }
                    }
                }
            }
        }
    }
}
