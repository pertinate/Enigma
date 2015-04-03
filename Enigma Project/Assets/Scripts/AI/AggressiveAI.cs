using UnityEngine;
using Excelsion.AI;
using Pathfinding;

namespace Excelsion{
	public class AggressiveAI : AIBase {
		public EntityType entityType;

		private void Awake(){
			this.type = entityType;
			this.seeker = GetComponent<Seeker>();
			this.aggroRadius = 10f;
		}
		private void Start(){
			startExecuting(transform.position, randomVector, OnPathComplete);
		}
		protected void FixedUpdate (){
			if(this.path == null)
				return;
			if(this.currentWaypoint >= this.path.vectorPath.Count)
				return;

			Vector3 dir = (this.path.vectorPath[this.currentWaypoint] - transform.position).normalized;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(dir), 1000f * Time.deltaTime);
			transform.Translate(dir * 0.01f);
		


			if(Vector3.Distance(transform.position, this.path.vectorPath[this.currentWaypoint]) < nextWaypointDistance){
				this.currentWaypoint++;
				return;
			}
		}
		protected override void OnPathComplete (Path p){
			base.OnPathComplete (p);
			if(p.error){
				Debug.Log("There was an error with the path. Error:" + p.error);
			} else {
				this.path = p;
				this.currentWaypoint = 0;
			}
		}
		protected override void startExecuting (Vector3 start, Vector3 end, OnPathDelegate delFunc){
			base.startExecuting (start, end, delFunc);
			this.seeker.StartPath(start, end, delFunc);
		}
		private void OnDrawGizmos(){
			if(isAggressive(this.type)){
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(transform.position, aggroRadius);
			}
		}
	}
}