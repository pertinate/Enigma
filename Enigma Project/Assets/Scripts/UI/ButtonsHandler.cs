using UnityEngine;
using System;

namespace Pertinate.Interface{
	public class ButtonsHandler : MonoBehaviour {
		public static bool isWindowed;
		public int width;
		public int height;
		public int currentMonitor;

		public void Awake(){
			isWindowed = Screen.fullScreen;
			currentMonitor = PlayerPrefs.GetInt("UnitySelectMonitor");
		}
		public void changeWindow(){
			isWindowed = !isWindowed;
			Screen.SetResolution(Screen.width, Screen.height, isWindowed);
			InterfaceHandler.instance.windTXT.text = isWindowed ? "Windowed" : "Fullscreen";
		}
		public void changeMontior(){
			try{
				currentMonitor++;
				PlayerPrefs.SetInt("UnitySelectMonitor", currentMonitor);
				Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreen);
			} catch(Exception e) {
				currentMonitor = 0;
				PlayerPrefs.SetInt("UnitySelectMonitor", currentMonitor);
				Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreen);
			}
		}
		public void changeResolution(string dim){
			int w = dim.IndexOf("x");
			int l = dim.IndexOf("x");
			l++;
			int.TryParse(dim.Substring(0, w), out width);
			int.TryParse(dim.Substring(l, dim.Length - l), out height);
			Screen.SetResolution(width, height, isWindowed);
			if(InterfaceHandler.instance.resTXT != null)
				InterfaceHandler.instance.resTXT.text = "Resolution: " + this.width.ToString() + "x" + this.height.ToString();
		}
		public void LoadLevel(int levelID){
			Application.LoadLevel(levelID);
		}
		public void CloseInterface(string s){
			Interface i = (Interface)Enum.Parse(typeof(Interface), s);
			i.Close();
		}
		public void OpenInterface(string s){
			Interface i = (Interface)Enum.Parse(typeof(Interface), s);
			InterfaceHandler.current = i;
		}
		public void EnableDisableGO(GameObject o){
			if(o.activeSelf){
				o.SetActive(false);
			} else {
				o.SetActive(true);
			}
		}
	}
}