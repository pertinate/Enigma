using UnityEngine;

namespace Pertinate.Skills{
	public class Farming : SkillHandler {
		public static Farming instance;
		public bool doExp;
		public void Awake ()
		{
			instance = this;
			this.skill = Skill.Farming;
		}
		public void Start ()
		{
			this.realLevel = PlayerPrefs.GetInt("Farming");
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