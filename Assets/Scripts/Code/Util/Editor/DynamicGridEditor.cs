using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using static UnityEngine.UI.GridLayoutGroup;

[CustomEditor(typeof(DynamicGridLayoutGroup), true)]
[CanEditMultipleObjects]
public class DynamicGridEditor : Editor
{
    SerializedProperty paddingSize;
    SerializedProperty paddingRatio;
    SerializedProperty m_StartCorner;
    SerializedProperty m_StartAxis;
    SerializedProperty m_ChildAlignment;
    SerializedProperty spacingRatio;
    SerializedProperty spacingFix;

    SerializedProperty m_Constraint;
    SerializedProperty m_ConstraintCount;

    SerializedProperty isExtendWidth;
    SerializedProperty isExtendHeight;
    SerializedProperty cellRatio;

    protected virtual void OnEnable()
    {
        paddingSize = serializedObject.FindProperty("paddingSize");
        paddingRatio = serializedObject.FindProperty("paddingRatio");
        m_StartCorner = serializedObject.FindProperty("m_StartCorner");
        m_StartAxis = serializedObject.FindProperty("m_StartAxis");
        m_ChildAlignment = serializedObject.FindProperty("m_ChildAlignment");
        spacingRatio = serializedObject.FindProperty("spacingRatio");
        spacingFix = serializedObject.FindProperty("spacingFix");

        m_Constraint = serializedObject.FindProperty("m_Constraint");
        m_ConstraintCount = serializedObject.FindProperty("m_ConstraintCount");
        isExtendWidth = serializedObject.FindProperty("isExtendWidth");
        isExtendHeight = serializedObject.FindProperty("isExtendHeight");
        cellRatio = serializedObject.FindProperty("cellRatio");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(paddingSize, true);
        EditorGUILayout.PropertyField(paddingRatio, true);
        EditorGUILayout.PropertyField(m_StartCorner, true);
        EditorGUILayout.PropertyField(m_StartAxis, true);
        EditorGUILayout.PropertyField(m_ChildAlignment, true);
        EditorGUILayout.PropertyField(spacingRatio, true);
        EditorGUILayout.PropertyField(spacingFix, true);
        EditorGUILayout.PropertyField(m_Constraint, true);
        EditorGUILayout.PropertyField(m_ConstraintCount, true);
        EditorGUILayout.PropertyField(isExtendWidth, true);
        EditorGUILayout.PropertyField(isExtendHeight, true);
        EditorGUILayout.PropertyField(cellRatio, true);
        serializedObject.ApplyModifiedProperties();
    }
}
