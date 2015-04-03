using UnityEngine;
using Pertinate.Utils;

namespace Pertinate.Player{
	public class CharacterInput : MonoBehaviour {
		public static bool GetDirRightInput(){
			if(Input.GetKey(KeyManager.Get("dRight")))
				return true;
			else
				return false;
		}
		public static bool GetDirLeftInput(){
			if(Input.GetKey(KeyManager.Get("dLeft")))
				return true;
			else
				return false;
		}
		public static bool GetDirForwardInput(){
			if(Input.GetKey(KeyManager.Get("dForward")))
				return true;
			else
				return false;
		}
		public static bool GetDirBackInput(){
			if(Input.GetKey(KeyManager.Get("dBack")))
				return true;
			else
				return false;
		}
		public static bool GetJumpInput(){
			if(Input.GetKeyDown(KeyManager.Get("jump"))){
				return true;
			} else {
				return false;
			}
		}
		public static float GetDirAxis(string axis){
			if(axis.ToLower() == "horizontal"){
				if(GetDirRightInput())
					return 1.0f;
				else if(GetDirLeftInput())
					return -1.0f;
			} else if(axis.ToLower() == "vertical"){
				if(GetDirForwardInput())
					return 1.0f;
				else if(GetDirBackInput())
					return -1.0f;
			}
			return 0f;
		}
	}
}