using UnityEngine;

namespace Pertinate{
	public class CameraControl : MonoBehaviour {
		public static CameraControl instance;
		public Transform target;
		public float distance = 5f;
		public float distanceMin = 3f;
		public float distanceMax = 10f;
		public float distanceSmooth = 0.05f;
		public float xMouseSensitivity = 5f;
		public float yMouseSensitivity = 5f;
		public float mouseWheelSensitivty = 5f;
		public float yMinLimit = -40f;
		public float yMaxlimit = 80f;
		public float xSmooth = 0.05f;
		public float ySmooth = 0.1f;

		private float mouseX = 0f;
		private float mouseY = 0f;
		private float velX = 0f;
		private float velY = 0f;
		private float velZ = 0f;
		private float velDistance = 0f;
		private float startDistance = 0f;
		private Vector3 position = Vector3.zero;
		private Vector3 desiredPosition = Vector3.zero;
		private float desiredDistance = 0f;

		private void Awake(){
			instance = this;
		}
		private void Start(){
			distance = Mathf.Clamp(distance, distanceMin, distanceMax);
			startDistance = distance;
			Reset();
		}
		private void FixedUpdate(){
			if(target == null)
				return;
			HandlePlayerInput();
			Calculate();
			UpdatePosition();
		}
		public void Reset(){
			mouseX = 0f;
			mouseY = 10f;
			distance = startDistance;
			desiredDistance = distance;
		}
		private void HandlePlayerInput(){
			var deadZone = 0.01f;
			mouseX += Input.GetAxis("Mouse X") * xMouseSensitivity;
			mouseY -= Input.GetAxis("Mouse Y") * yMouseSensitivity;
			mouseY = CameraHelper.ClampAngle(mouseY, yMinLimit, yMaxlimit);
			if(Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone){
				desiredDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivty, distanceMin, distanceMax);
			}
		}
		private void Calculate(){
			distance = Mathf.SmoothDamp(distance, desiredDistance, ref velDistance, distanceSmooth);
			desiredPosition = CalculatePosition(mouseY, mouseX, distance);
		}
		private Vector3 CalculatePosition(float x, float y, float distance){
			Vector3 direction = new Vector3(0, 0, -distance);
			Quaternion rotation = Quaternion.Euler(x, y, 0);
			return target.position + rotation * direction;
		}
		private void UpdatePosition(){
			var posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, xSmooth);
			var posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, ySmooth);
			var posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, xSmooth);
			position = new Vector3(posX, posY, posZ);
			transform.position = position;
			transform.LookAt(target);
		}
	}
}