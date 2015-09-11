using UnityEngine;
using System;
using Pertinate.Skills;
using UnityEngine.UI;

namespace Pertinate.Interface{
	public enum Interface{
		None = -1,
		Skill = 0,
		Inventory = 1,
		Quest = 2,
		Pause = 3,
		Options = 4
	}
	public static class InterfaceExtensions{
		public static bool isOpen(this Interface en){
			for(int i = 0; i < Enum.GetNames(typeof(Interface)).Length - 1; i++){
				int s = (int)en;
				if(i == s){
					if(InterfaceHandler.instance.interfaces[s].activeSelf)
						return true;
					else
						return false;
				}
			}
			return false;
		}
		public static bool isAnyOpen(){
			foreach(Interface i in Enum.GetValues(typeof(Interface))){
				if(i.isOpen()){
					return true;
				}
			}
			return false;
		}
		public static void Close(this Interface en){
			for(int i = 0; i < Enum.GetNames(typeof(Interface)).Length - 1; i++){
				int s = (int)en;
				if(i == s){
					if(en.isOpen())
						InterfaceHandler.instance.interfaces[s].SetActive(false);
				}
			}
		}
		public static void CloseAll(){
			foreach(GameObject g in InterfaceHandler.instance.interfaces){
				g.SetActive(false);
			}
		}
	}
	public class InterfaceHandler : MonoBehaviour {
		public static Interface current;
		public static InterfaceHandler instance;
		public GameObject[] interfaces;
		public GameObject SkillInfo;
		public GameObject currSkill;
		public Text levelTXT;
		public Text currTXT;
		public Text nextTXT;
		public Text resTXT;
		public Text windTXT;

		public void Awake(){
			current = Interface.None;
			Cursor.visible = false;
			instance = this;
			if(resTXT != null)
				resTXT.text = "Resolution: " + Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height;
		}
		public void Start(){
			InterfaceExtensions.CloseAll();
		}
		public void Update(){
			if(!Interface.Pause.isOpen() && Time.timeScale == 0f)
				Time.timeScale = 1f;
			if(!Interface.Pause.isOpen()){
				if(Interface.Options.isOpen())
					Interface.Options.Close();
			}
			CheckKeyDowns();
			switch(current){
			case Interface.Inventory:
				if(!interfaces[1].activeSelf)
					interfaces[1].SetActive(true);
				current = Interface.None;
				break;
			case Interface.Quest:
				if(!interfaces[2].activeSelf)
					interfaces[2].SetActive(true);
				current = Interface.None;
				break;
			case Interface.Skill:
				if(!interfaces[0].activeSelf)
					interfaces[0].SetActive(true);
				current = Interface.None;
				break;
			case Interface.Pause:
				if(!interfaces[3].activeSelf)
					interfaces[3].SetActive(true);
				Time.timeScale = 0f;
				current = Interface.None;
				break;
			case Interface.Options:
				if(!interfaces[4].activeSelf)
					interfaces[4].SetActive(true);
				current = Interface.None;
				break;
			default:break;
			}
			if(currSkill != null){
				foreach(SkillHandler sh in Main.SkillObject.GetComponents<SkillHandler>()){
					SkillType s = sh.skill;
					if(s.ToString() == currSkill.name){
						SkillInfo.SetActive(true);
						levelTXT.text = sh.realLevel.ToString();
						currTXT.text = sh.realLevel.ToString();
						nextTXT.text = sh.nextLevelExperience(sh.realLevel).ToString();
					}
				}
			} else {
				SkillInfo.SetActive(false);
			}
		}
		public void SendInfo(GameObject go){
			currSkill = go;
		}
		public void CheckKeyDowns(){
			if(Input.GetKeyDown(KeyCode.I)){
				if(!Interface.Inventory.isOpen())
					current = Interface.Inventory;
				else
					Interface.Inventory.Close();
			} else if(Input.GetKeyDown(KeyCode.L)){
				if(!Interface.Quest.isOpen())
					current = Interface.Quest;
				else
					Interface.Quest.Close();
			} else if(Input.GetKeyDown(KeyCode.K)){
				if(!Interface.Skill.isOpen())
					current = Interface.Skill;
				else
					Interface.Skill.Close();
			}else if((Input.GetKeyDown(KeyCode.LeftAlt) || InterfaceExtensions.isAnyOpen()) && !Cursor.visible){
				Cursor.visible = true;
			} else if(Input.GetKeyDown(KeyCode.LeftAlt) && Cursor.visible){
				Cursor.visible = false;
			} else if(Input.GetKeyDown(KeyCode.Escape)){
				if(!Interface.Pause.isOpen())
					current = Interface.Pause;
				else
					Interface.Pause.Close();
			}
		}
	}
}