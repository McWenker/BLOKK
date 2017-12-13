using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(Equipped))]
public class EquippedEditor : Editor
{
	private SerializedProperty equippedImagesProperty;
	private SerializedProperty equippedProperty;
    private SerializedProperty slotNamesProperty;
	
	
	private bool[] showEquippedSlots = new bool[Equipped.numEquippedSlots];
	
	
	private const string inventoryPropEquippedImagesName = "equippedImages";
	private const string inventoryPropEquippedName = "equipped";
    private const string inventoryPropColorName = "slotNames";
	
	
	private void OnEnable()
	{
		equippedImagesProperty = serializedObject.FindProperty(inventoryPropEquippedImagesName);
		equippedProperty = serializedObject.FindProperty(inventoryPropEquippedName);
        slotNamesProperty = serializedObject.FindProperty(inventoryPropColorName);
	}
	
	private void EquippedSlotGUI(int index)
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUI.indentLevel++;

		if (index==0)
			showEquippedSlots[index] = EditorGUILayout.Foldout(showEquippedSlots[index], "Weapon slot 1");
		else if (index==1)
			showEquippedSlots[index] = EditorGUILayout.Foldout(showEquippedSlots[index], "Weapon slot 2");
		else if (index==2)
			showEquippedSlots[index] = EditorGUILayout.Foldout(showEquippedSlots[index], "Shield slot");
		else if (index==3)
			showEquippedSlots[index] = EditorGUILayout.Foldout(showEquippedSlots[index], "Helmet slot");		
		else if (index==4)
			showEquippedSlots[index] = EditorGUILayout.Foldout(showEquippedSlots[index], "Armor slot");			
		else if (index==5)
			showEquippedSlots[index] = EditorGUILayout.Foldout(showEquippedSlots[index], "Trinket slot 1");				
		else if (index==6)
			showEquippedSlots[index] = EditorGUILayout.Foldout(showEquippedSlots[index], "Trinket slot 2");

		if(showEquippedSlots[index])
		{
			EditorGUILayout.PropertyField(equippedImagesProperty.GetArrayElementAtIndex(index));
			EditorGUILayout.PropertyField(equippedProperty.GetArrayElementAtIndex(index));
            EditorGUILayout.PropertyField(slotNamesProperty.GetArrayElementAtIndex(index));
		}
		
		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical();
	}
	
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		for(int i = 0; i < showEquippedSlots.Length; i++)
		{
			EquippedSlotGUI(i);
		}
		
		serializedObject.ApplyModifiedProperties();
	}
	
}