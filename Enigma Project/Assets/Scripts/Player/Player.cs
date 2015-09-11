
using UnityEngine;

namespace Pertinate.Player{
	public class Player : CharacterInput {
		public static float currentHealth;
		public static float maxHealth;
		public Animator anim;
		public CharacterController control;
		public Rigidbody body;
		private Vector3 moveVector;
		public float moveSpeed = 10;
		public float rotataion;
		public float jumpSpeed = 10;
		public float distToGround;
		public bool grounded{
			get{
				return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
			}
		}

		private void Awake(){
			anim = this.GetComponent<Animator>();
			control = this.GetComponent<CharacterController>();
			body = this.GetComponent<Rigidbody>();
		}
		private void Start(){
			distToGround = body.velocity.y;
		}
		public void FixedUpdate(){
			if(body.velocity.y < -0.2)
				Debug.Log("falling");
			UpdateMotor();
			if(GetJumpInput())
				control.Move(Vector3.up * jumpSpeed);
		}
		private void Move(){
			float deadZone = 0.1f;
			moveVector = Vector3.zero;
			if(GetDirAxis("Vertical") > deadZone || GetDirAxis("Vertical") < -deadZone){
				moveVector += new Vector3(0, 0, GetDirAxis("Vertical"));
				if(GetDirAxis("vertical") > deadZone)
					anim.SetFloat("Speed", 1);
				else if(GetDirAxis("vertical") < -deadZone)
					anim.SetFloat("Speed", -1);
			} else {
				anim.SetFloat("Speed", 0);
			}
			if(GetDirAxis("horizontal") > deadZone || GetDirAxis("horizontal") < -deadZone){
				moveVector += new Vector3(GetDirAxis("horizontal"), 0, 0);
				anim.SetFloat("Speed", 1);
			} else {
				anim.SetFloat("Direction", 0);
			}
		}
		private void SnapToCam(){
			if(GetDirAxis("vertical") < 0 || GetDirAxis("vertical") > 0 || GetDirAxis("horizontal") < 0 || GetDirAxis("horizontal") > 0){
				transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, transform.eulerAngles.z);
			}
		}
		private void ProcessMotion(){
			moveVector = transform.TransformDirection(moveVector);
			if(moveVector.magnitude > 1)
				moveVector = Vector3.Normalize(moveVector);
			moveVector *= moveSpeed;
			moveVector *= Time.deltaTime;
			control.Move(moveVector);
		}
		private void UpdateMotor(){
			SnapToCam();
			ProcessMotion();
			Move();
		}
	}
}