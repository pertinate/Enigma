using UnityEngine;
using UnityEngine.UI;
using System;

namespace Excelsion{
	public enum TimeSetting{
		System,
		Server
	}
	public class SystemTime : MonoBehaviour {
		private Text textTime;
		private TimeSetting thisType = TimeSetting.System;

		private void Awake(){
			textTime = GetComponent<Text>();
			textTime.text = "";
		}
		private void Update(){
			if(thisType == TimeSetting.System){
				textTime.text = DateTime.Now.ToShortTimeString() + "\n" + DateTime.Now.ToShortDateString();
			}
		}
	}
}