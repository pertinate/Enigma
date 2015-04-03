using UnityEngine;
using UnityEngine.UI;

namespace Excelsion{
	public class ChatManager : MonoBehaviour {
		public Text chatBox;
		public InputField chatInput;

		private void Start(){
			InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
			submitEvent.AddListener(Add);
			chatInput.onEndEdit = submitEvent;
		}
		public void Add(string text){
			string textHolder = chatBox.text;
			if(text != string.Empty){
				string hold = DoCommand(text).ToString();
				if(hold != string.Empty && text[0].ToString() == "/"){
					chatBox.text = textHolder + "\n" + hold;
				} else if(hold == string.Empty && text[0].ToString() == "/"){
					chatBox.text = textHolder + "\n" + "Please try a vaild command or enter the correct information.";
				} else if(hold == string.Empty && text[0].ToString() != "/"){
					chatBox.text = textHolder + "\n" + text;
				}
			}
		}
		public string DoCommand(string commandText){
			if(commandText[0].ToString() == "/"){
				string stringHolder = commandText.TrimStart('/').ToLower();
				if(stringHolder.IndexOf(" ") < 0)
					return string.Empty;
				string switchIndex = stringHolder.Substring(0, stringHolder.IndexOf(" "));
				switch(switchIndex){
				default:
					break;
				case "level":
					int levelIndex = int.Parse(stringHolder.Substring(stringHolder.IndexOf("=") + 1));

					if(levelIndex <= Application.levelCount)
						Application.LoadLevel(levelIndex);
					else
						return "Invalid level";
					break;
				case "help":
					break;
				case "skill":
					Debug.Log(commandText);
					break;
				}
				return string.Empty;
			} else {
				return string.Empty;
			}
		}
	}
}