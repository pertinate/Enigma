using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Pertinate.Quest{
	public enum QuestLog{
		Current = -1,
		One = 0,
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven,
		Eight,
		Nine,
		Ten
	}
	public static class QLE{
		public static void setQuest(this QuestLog en, QuestContainer qc){
			if(qc.completed == false){
				for(int i = 0; i < Quest.instance.activeQuests.Length; i++){
					if(qc != Quest.instance.activeQuests[i]){
						Quest.instance.activeQuests[(int)en] = qc;
					}
				}
			}
		}

		public static string description(this QuestLog en){
			if(Quest.instance.activeQuests[(int)en] != null){
				if(Quest.instance.activeQuests[(int)en].description != string.Empty){
					return Quest.instance.activeQuests[(int)en].description;
				}
			}
			return string.Empty;
		}
		public static string name(this QuestLog en){
			if(Quest.instance.activeQuests[(int)en] != null){
				if(Quest.instance.activeQuests[(int)en].name != string.Empty){
					return Quest.instance.activeQuests[(int)en].name;
				}
			}
			return string.Empty;
		}
		public static void progress(this QuestLog en){
			if(Quest.instance.activeQuests[(int)en] != null){
				if(Quest.instance.activeQuests[(int)en].currentProgression < Quest.instance.activeQuests[(int)en].totalProgression){
					Quest.instance.activeQuests[(int)en].currentProgression++;
					if(Quest.instance.activeQuests[(int)en].currentProgression >= Quest.instance.activeQuests[(int)en].totalProgression){
						Quest.instance.activeQuests[(int)en].completed = true;
					}
				}
			}
		}
		public static int currentProgress(this QuestLog en){
			if(Quest.instance.activeQuests[(int)en] != null){
				return Quest.instance.activeQuests[(int)en].currentProgression;
			}
			return -1;
		}
		public static int totalProgress(this QuestLog en){
			if(Quest.instance.activeQuests[(int)en] != null){
				return Quest.instance.activeQuests[(int)en].totalProgression;
			}
			return -1;
		}
	}
	[Serializable]
	public class Quest : MonoBehaviour {
		public static string path;
		public string Path = Application.dataPath + "/quest.xml";
		public static Quest instance;
		public bool loadXML;
		public QuestContainer[] activeQuests;
		public QuestLog other;
		[SerializeField]
		public QuestList ql = new QuestList();
		public QuestProgression progessor;

		private void Awake(){
			instance = this;
			path = Path;
			activeQuests = new QuestContainer[10];
			if(loadXML){
				try{
					ql = ql.Load(path);
				} catch (Exception e){
					Debug.Log(e);
				}
			}
		}
		private void Start(){
			QuestLog.One.setQuest(ql.quests[0]);
		}
		private void OnApplicationQuit(){
			ql.Save(path);
		}
	}
	[Serializable]
	[XmlRoot("List")]
	public class QuestList{
		[XmlArray("Quests"), XmlArrayItem("Quest")]
		public QuestContainer[] quests;

		public void Save(string path){
			var serializer = new XmlSerializer(typeof(QuestList));
			using(var stream = new FileStream(path, FileMode.Create)){
				serializer.Serialize(stream, this);
			}
		}
		public QuestList Load(string path){
			var serializer = new XmlSerializer(typeof(QuestList));
			using(var stream = new FileStream(path, FileMode.Open)){
				return serializer.Deserialize(stream) as QuestList;
			}
		}
		public QuestList LoadWWW(string text){
			var serializer = new XmlSerializer(typeof(QuestList));
			return serializer.Deserialize(new StringReader(text)) as QuestList;
		}
	}
	[Serializable]
	public class QuestContainer{
		[XmlAttribute("name")]
		public string name = "Quest";
		public string description = "Description";
		public int currentProgression;
		public int totalProgression;
		public bool completed = false;
	}
}