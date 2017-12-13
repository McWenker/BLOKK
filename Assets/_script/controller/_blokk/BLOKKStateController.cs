using UnityEngine;
using System.Collections;

public class BLOKKStateController : MonoBehaviour
{	
	public enum ActionState
	{
		Ready,
		Moving,
		Falling,
		Jumping,
		Attacking,
		Reloading,
		Dashing,
		Interacting,
		Knockback
	}

	public enum DashState
	{
		Ready,
		Dashing,
		Cooldown
	}

	public enum HealthState
	{
		Healthy,
		Hurt,
		Dead
	}

	public enum MoveState
	{
		Standing,
		Moving,
		Jumping,
		Falling
	}
}
