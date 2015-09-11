using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

namespace Pertinate.Quest{
	[CustomEditor(typeof(Quest))]
	public class QuestInspector : Editor {
		public override void OnInspectorGUI ()
		{
			serializedObject.Update();
			SerializedProperty sp = serializedObject.FindProperty("ql");
			SerializedProperty spp = sp.FindPropertyRelative("quests");
			EditorGUILayout.PropertyField(serializedObject.FindProperty("loadXML"), new GUIContent("Load XML or WWW"));
			if(serializedObject.FindProperty("loadXML").boolValue == true){
				EditorGUILayout.PropertyField(serializedObject.FindProperty("Path"), new GUIContent("XML/WWW Path"));
			}
			if(GUILayout.Button("Add Quest")){
				spp.arraySize++;
			}
			EditorList.Show(spp);
			serializedObject.ApplyModifiedProperties();
		}
	}
}