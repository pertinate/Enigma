using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Pertinate.Quest{
	public static class EditorList {
		public static void Show(SerializedProperty list){
			EditorGUILayout.PropertyField(list);
			EditorGUI.indentLevel += 1;
			if(list.isExpanded){
				for(int i = 0; i < list.arraySize; i++){
					EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(list.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue), true);
					GUILayoutOption bw = GUILayout.Width(60f);
					GUIStyle gs = EditorStyles.miniButton;
					gs.margin = new RectOffset(45, 0, 0, 0);
					if(GUILayout.Button("Remove", gs, bw)){
						list.DeleteArrayElementAtIndex(i);
					}
					bw = GUILayout.Width(100f);
					if(GUILayout.Button("Add Progressor", gs, bw)){
						if(GameObject.Find("-Progressors-") == null){
							PrefabUtility.InstantiatePrefab(new GameObject());
							GameObject go = GameObject.Find("New Game Object");
							go.transform.parent = GameObject.FindObjectOfType<Quest>().transform;
							go.name = "-Progressors-";
							go.AddComponent<QuestProgression>();
							QuestProgression qp = go.GetComponent<QuestProgression>();
							qp.qc = GameObject.Find("MonoObjects").GetComponent<Quest>().ql.quests[i];
							qp.questNumber = i;
						} else {
							GameObject go = GameObject.Find("-Progressors-");
							go.AddComponent<QuestProgression>();
							foreach(QuestProgression qp in go.GetComponents<QuestProgression>()){
								if(qp.qc.name == "Quest"){
									qp.qc.name = list.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue;
									qp.qc.description = list.GetArrayElementAtIndex(i).FindPropertyRelative("description").stringValue;
									qp.qc.currentProgression = list.GetArrayElementAtIndex(i).FindPropertyRelative("currentProgression").intValue;
									qp.qc.totalProgression = list.GetArrayElementAtIndex(i).FindPropertyRelative("totalProgression").intValue;
									qp.qc.completed = list.GetArrayElementAtIndex(i).FindPropertyRelative("completed").boolValue;
								}
							}
						}
					}
				}
			}
			EditorGUI.indentLevel -= 1;
		}
	}
}
