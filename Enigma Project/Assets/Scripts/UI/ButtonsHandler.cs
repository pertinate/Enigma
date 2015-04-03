using UnityEngine;
using System;

namespace Pertinate.Interface{
	public class ButtonsHandler : MonoBehaviour {
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
	}
}