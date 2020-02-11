using System.Collections;

using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]

public class EnemyControl : MonoBehaviour {

	// What to chase
	private Transform target;

	// references to player and patrol points
	public Transform Player;
	public Transform[] patrols;
	private bool isPatrolling = true;
	private int patrolMarker;
	private bool patrolsReversed;

	// How many times per second we are updating path
	public float updateRate;
	
	// Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	// calculated path
	public Path path;

	// Speed / sec
	public float speed;

	[HideInInspector]
	public bool pathIsEnded;

	// Max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;

	// waypoint we are currently moving towards
	private int currentWaypoint = 0;

	void Start () {
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

		target = patrols[0];

		if (target == null) {
			return;
		}

		// Start a new path to the target position, return the result to the onPathComplete callback
		seeker.StartPath(transform.position, target.position, onPathComplete);

		StartCoroutine(UpdatePath());

	}

	IEnumerator UpdatePath () {
		if (target == null) {
			yield break;
		}

		seeker.StartPath(transform.position, target.position, onPathComplete);


		yield return new WaitForSeconds ( 1/ updateRate );
		// don't understand this
        StartCoroutine(UpdatePath());
	}


	public void onPathComplete (Path p) {
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
		
	}

	void Update () {
		if (target == null) {
			return;
		}

		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}
			
			pathIsEnded = true;
			
			return;
		}
		pathIsEnded = false;

		Vector3 dir = ( path.vectorPath[currentWaypoint] - transform.position ).normalized;
		// dir *= speed * Time.fixedDeltaTime;


        rb.MovePosition(new Vector2((transform.position.x + dir.x * speed * Time.deltaTime),
                        transform.position.y + dir.y * speed * Time.deltaTime));


		// Move the AI
		// rb.AddForce (dir, fMode);

		float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}

	public void startPatrol() {
		isPatrolling = true;
		target = patrols[0];
        patrolMarker = 0;
	}

	public void chasePlayer() {
		target = Player;
	}

	// handle patrol waypointing
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "patrolpoint" && isPatrolling && col.gameObject.name == patrols[patrolMarker].name) {
			if (patrolMarker >= patrols.Length - 1)
			{
				patrolsReversed = true;
				patrolMarker--;
			}
			else if (patrolMarker <= 0 && patrolsReversed)
			{
				patrolsReversed = false;
				patrolMarker = 1;
			}
			else if (patrolsReversed)
			{
				patrolMarker--;
			}
			else
			{
				patrolMarker++;
			}
			target = patrols[patrolMarker];
			currentWaypoint = 0;
		}

		else if(col.gameObject.name == "Spotlight2dCollider") {
		}
	}



	
}

