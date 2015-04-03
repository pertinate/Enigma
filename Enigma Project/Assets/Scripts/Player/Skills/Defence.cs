using UnityEngine;

namespace Pertinate.Skills{
	public class Defence : SkillHandler {
		public static Defence instance;

		public void Awake ()
		{
			instance = this;
			this.skill = Skill.Defence;
		}
		public void Start(){
			this.realLevel = PlayerPrefs.GetInt("Defence");
			if(this.realLevel <= 0)
				this.realLevel = 1;
		}
		public void Update ()
		{
			if(this.skillText != null)
				this.skillText.text = this.floatedLevel.ToString() + '/' + this.realLevel.ToString();
			if(this.experienceText != null)
				this.experienceText.text = this.currentExperience.ToString("N0") + '/' + this.maxExperience.ToString("N0");
			this.maxExperience = nextLevelExperience(this.realLevel);
			if(this.currentExperience >= this.maxExperience){
				LevelUp(instance);
			}
		}
		public void OnApplicationQuit(){
			Save(instance, "int");
		}
	}
}