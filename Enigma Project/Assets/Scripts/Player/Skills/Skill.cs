using UnityEngine;

namespace Pertinate.Skills{
	public class Skill : SkillHandler {
		public bool doExp;
		public void Awake ()
		{
		}
		public void Start ()
		{
			this.realLevel = PlayerPrefs.GetInt(this.skill.ToString() + "Level");
			this.currentExperience = PlayerPrefs.GetInt(this.skill.ToString() + "Exp");
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
				LevelUp(this);
			}
			if(doExp)
				this.currentExperience += 50;
		}
		public void OnApplicationQuit(){
			PlayerPrefs.SetInt(this.skill.ToString() + "Level", this.realLevel);
			PlayerPrefs.SetFloat(this.skill.ToString() + "Exp", this.currentExperience);
		}
	}
}