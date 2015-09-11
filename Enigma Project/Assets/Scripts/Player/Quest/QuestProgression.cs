using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Pertinate.Quest{
	public class QuestProgression : MonoBehaviour {
		public QuestContainer qc;
		public int questNumber;

		public void Awake(){

		}
		public void Start(){
			qc = Quest.instance.ql.quests[questNumber];
			QuestLog.One.progress();
		}
	}
}
