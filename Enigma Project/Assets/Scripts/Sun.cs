using UnityEngine;

namespace Pertinate{
	public class Sun : MonoBehaviour {
		public void Update(){
			Quaternion rot = transform.localRotation;
			rot.x += (float)(Time.deltaTime * 0.03);
			transform.localRotation = rot;
		}
	}
}