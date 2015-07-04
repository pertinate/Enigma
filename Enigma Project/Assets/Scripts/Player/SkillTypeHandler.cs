using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Pertinate.Settings;

namespace Pertinate.Skills{
	[System.Serializable]
	public enum SkillType{
		Melee,
		Defence,
		Range,
		Prayer,
		Magic,
		Summoning,
		Runecrafting,
		Construction,
		Agility,
		Herblore,
		Thieving,
		Crafting,
		Fletching,
		Slayer,
		Hunter,
		Mining,
		Smithing,
		Fishing,
		Cooking,
		Firemaking,
		Woodcutting,
		Farming
	}
	public static class SkillExtensions{
		public static long currentExperience(this SkillType en){
			foreach(SkillHandler sh in Main.SkillObject.GetComponents<SkillHandler>()){
				if(en == sh.skill){
					return sh.currentExperience;
				}
			}
			return 0;
		}
		public static int level(this SkillType en){
			foreach(SkillHandler sh in Main.SkillObject.GetComponents<SkillHandler>()){
				if(en == sh.skill){
					return sh.realLevel;
				}
			}
			return 0;
		}
		public static void AddExperience(this SkillType en, long amount){
			foreach(SkillHandler sh in Main.SkillObject.GetComponents<SkillHandler>()){
				if(en == sh.skill){
					sh.currentExperience += amount;
				}
			}
		}
	}
	public class SkillHandler : MonoBehaviour{
		public SkillType skill;
		public long currentExperience;
		public long maxExperience;
		public Text skillText;
		public Text experienceText;
		public long nextLevelExperience(int level){
			long a = 83;
			for(int x = 1; x < level; x++){
				a = a + (long)((x + 300 * (long)(Mathf.Pow(2, (level / 7)))));
			}
			long result = (long)Mathf.Floor(a/3.7f);
			return (long)result;
		}
		public int floatedLevel{
			get{return realLevel;}
		}
		public int realLevel;
		public void LevelUp(SkillHandler s){
			s.realLevel++;
			long hold = s.currentExperience - s.maxExperience;
			s.currentExperience = hold;
			s.maxExperience = s.nextLevelExperience(s.realLevel);
		}
		public void Save(SkillHandler skill, string type){
			if(type.ToLower() == "int"){
				PlayerPrefs.SetInt(skill.skill.ToString(), skill.realLevel);
			}
			if(type.ToLower() == "string"){

			}
			if(type.ToLower() == "float"){

			}
		}
		public static void SaveAll(){
		}
	}
}