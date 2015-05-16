using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TD;

public class Simurgh : EnnemiUnit 
{
	public float basicSpeed; // basic speed of the unit
	public float slowedSpeed; // speed when fallen == true
	
	public float minimumTimeToFall;
	public float maximumTimeToFall;

	public float minimumTimeToStandUp;
	public float maximumTimeToStandUp;

	protected float fallingTimer; // time before the enemy falls
	protected int fallingTimerINC; // if reaches falling timer --> fallen == true
	
	protected float standUpTimer; // time before the enemy gets up
	protected int standUpTimerINC; // if reaches standUpTimer --> fallen == false

	protected bool fallen;

	private int loseLife;

	// Use this for initialization
	void Start ()
	{
		//agent = this.GetComponent<NavMeshAgent>();
		monObjetSuivi = GameObject.Find("Castle");
		SetEnnemiDestination ();
		fallingTimer = Random.Range (minimumTimeToFall, maximumTimeToFall);
		standUpTimer = Random.Range (minimumTimeToStandUp, maximumTimeToStandUp);
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update ();

		// Needed to lose lifes
		if (HitSomething()) 
		{
			loseLife = 0-dommages;
			DayAndNightCycle.numberOfLifes -= dommages; // TEMPORAIRE --> POUR LE RENDU UNIQUEMENT
			controlledPlayer =(Player)GameObject.FindWithTag("Player").GetComponent(typeof(Player));
			controlledPlayer.AddResource (ResourceType.Power, loseLife);
			Destroy(this.gameObject);
		}

		// THE ENEMY STANDS UP
		if (fallen == false) fallingTimerINC++;
		if (fallingTimerINC >= fallingTimer)
		{
			fallen = true;
			fallingTimer = Random.Range (minimumTimeToFall, maximumTimeToFall);
			fallingTimerINC = 0;
			agent.speed = slowedSpeed;
			movementSpeed = slowedSpeed;
		}

		// THE ENEMY "FALLS"
		if (fallen == true) standUpTimerINC++;
		if (standUpTimerINC > standUpTimer)
		{
			fallen = false;
			standUpTimer = Random.Range (minimumTimeToStandUp, maximumTimeToStandUp);
			standUpTimerINC = 0;
			agent.speed = basicSpeed;
			movementSpeed = basicSpeed;
		}
	}
}