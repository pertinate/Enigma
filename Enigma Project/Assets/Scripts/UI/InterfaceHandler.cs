﻿using UnityEngine;
using System;
using Pertinate.Skills;
using UnityEngine.UI;

namespace Pertinate.Interface{
	public enum Interface{
		None = -1,
		Skill = 0,
		Inventory = 1,
		Quest = 2,
		Pause = 3
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

		public void Awake(){
			current = Interface.None;
			Cursor.visible = false;
			instance = this;
		}
		public void Start(){
			InterfaceExtensions.CloseAll();
		}
		public void Update(){
			if(!Interface.Pause.isOpen() && Time.timeScale == 0f)
				Time.timeScale = 1f;
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
			default:break;
			}
			if(currSkill != null){
				switch(currSkill.name.ToString()){
				case "Melee":
					SkillInfo.SetActive(true);
					levelTXT.text = Skill.Melee.level().ToString("N0");
					currTXT.text = Skill.Melee.currentExperience().ToString("N0");
					nextTXT.text = Melee.instance.maxExperience.ToString("N0");
					break;
				case "Defence":
					SkillInfo.SetActive(true);
					levelTXT.text = Skill.Defence.level().ToString("N0");
					currTXT.text = Skill.Defence.currentExperience().ToString("N0");
					nextTXT.text = Defence.instance.maxExperience.ToString("N0");
					break;
				default:
					break;
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