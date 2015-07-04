using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Pertinate.Interface{
	public class ToggleHandler : MonoBehaviour {
		private Resolution res;
		public Toggle[] resTog;
		public Toggle fullscreen;

		public void Awake(){
			Check();
			fullscreen.isOn = Screen.fullScreen;
			res = Screen.currentResolution;
		}

		public void setRes(){
			Screen.SetResolution(res.width, res.height, Screen.fullScreen);
			InterfaceHandler.instance.resTXT.text = "Resolution: " + res.width + "x" + res.height;
			Check();
		}
		public void ChangeWidth(int width){
			res.width = width;
		}
		public void ChangeHeight(int height){
			res.height = height;
		}
		public void Fullscreen(){
			Screen.fullScreen = !Screen.fullScreen;
		}
		private void Check(){
			foreach(Toggle t in resTog){
				if(t.name == Screen.currentResolution.width + "x" + Screen.currentResolution.height){
					t.isOn = true;
				} else {
					t.isOn = false;
				}
			}
		}
	}
}