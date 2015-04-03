using UnityEngine;
using UnityEngine.UI;

namespace Pertinate.Character{
	public class CharacterLeveling : MonoBehaviour {
		public Text levelTXT;
		public Text expTXT;
		public Text maxTXT;
		public Scrollbar expBar;

		public static int charLevel;
		public long charExp;
		public static long charMaxExp{
			get{return nextLevelExperience(charLevel);}
		}
		public static long nextLevelExperience(int level){
			long a = 83;
			for(int x = 1; x < level; x++){
				a = a + (long)((x + 300 * (long)(Mathf.Pow(2, (level / 7)))));
			}
			long result = (long)Mathf.Floor(a/5f);
			return (long)result;
		}
		public void Awake(){
		}
		public void Start(){
			if(charLevel < 1)
				charLevel = 1;
		}
		public void Update(){
			if(levelTXT != null)
				levelTXT.text = charLevel.ToString();
			if(expTXT != null)
				expTXT.text = charExp.ToString("N0");
			if(maxTXT != null)
				maxTXT.text = charMaxExp.ToString("N0");
			if(expBar != null)
				expBar.size = (float)((double)charExp/(double)charMaxExp);
			if(charExp >= charMaxExp){
				LevelUp();
			}
		}
		public void LevelUp(){
			charLevel++;
			long hold = charExp - charMaxExp;
			charExp = hold;
		}
	}
}