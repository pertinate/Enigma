using UnityEngine;
using Pathfinding;

namespace Excelsion.AI{
	public enum EntityType{
		Passive,
		Aggressive,
		Docile
	}
	[RequireComponent (typeof (Seeker))]
	public class AIBase : MonoBehaviour {
		#region Entity Stuff
		protected EntityType type;
		protected bool isAggressive(EntityType entity){
			return entity == EntityType.Aggressive;
		}
		protected bool isDocile(EntityType entity){
			return entity == EntityType.Docile;
		}
		protected bool isPassive(EntityType entity){
			return entity == EntityType.Passive;
		}
		protected bool canAggro(EntityType entity, Transform entityTransfrom){
			if(entity == EntityType.Aggressive){
				RaycastHit info;
				if(Physics.SphereCast(entityTransfrom.position, aggroRadius, entityTransfrom.forward, out info)){
					if(info.transform.tag == "Player")
						return true;
					else
						return false;
				} else {
					return false;
				}
			}
			return false;
		}
		protected Vector3 randomVector{
			get{return new Vector3(Random.Range(-20, 20), 0f, Random.Range(-20, 20));}
		}
		#endregion
		protected Seeker seeker;
		protected Path path;
		protected int currentWaypoint = 0;
		protected float nextWaypointDistance;
		protected float aggroRadius;


		protected virtual void OnPathComplete(Path p){
		}
		protected virtual void startExecuting(Vector3 start, Vector3 end, OnPathDelegate delFunc){
		}
	}
}