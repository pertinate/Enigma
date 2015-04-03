using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

namespace Pertinate.Skills{
	public class Melee : SkillHandler {
		public static Melee instance;
		public bool doExp;
		public void Awake ()
		{
			instance = this;
			this.skill = Skill.Melee;
		}
		public void Start ()
		{
			this.realLevel = PlayerPrefs.GetInt("Melee");
			if(this.realLevel <= 0)
				this.realLevel = 1;
		}
		public void Update ()
		{
			if(this.skillText != null)
				this.skillText.text = this.floatedLevel.ToString() + '/' + this.realLevel.ToString();
			if(this.experienceText != null)
				this.experienceText.text = this.currentExperience.ToString("N0" + '/' + this.maxExperience.ToString("N0"));
			this.maxExperience = nextLevelExperience(this.realLevel);
			if(this.currentExperience >= this.maxExperience && this.realLevel < 99){
				LevelUp(instance);
			}
			if(doExp)
				this.currentExperience += 50;
		}
	}
}