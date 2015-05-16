using UnityEngine;
using System.Collections;
using TD;

public class EthereNew : EnnemiUnit 
{
	// DAY
	public int timerRegenPv; // if i >= timerRegenPv pv increments by 1
	public int regenHp;
	protected int i;

	// NIGHT
	public int chanceToAvoid; // chance to avoid an attack
	protected float avoidance; // checks if he avoided the attack
	protected int j;

	private int loseLife;

	// Use this for initialization
	void Start () 
	{
		monObjetSuivi = GameObject.Find("Castle");
		SetEnnemiDestination ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update ();

		// Needed to lose lives
		if (HitSomething()) 
		{
			loseLife = 0-dommages;
			DayAndNightCycle.numberOfLifes -= dommages; // TEMPORAIRE --> POUR LE RENDU UNIQUEMENT
			controlledPlayer =(Player)GameObject.FindWithTag("Player").GetComponent(typeof(Player));
			controlledPlayer.AddResource (ResourceType.Power, loseLife);
			Destroy(this.gameObject);
		}

		// HP REGEN
		if (dayNightCycle == true && hitPoints < maxHitPoints)
		{
			i++;
			if (i >= timerRegenPv)
			{
				hitPoints += regenHp;
				i = 0;
			}
		}
	}

	// CHANCE TO AVOID
	public override void TakeDamage(int damage)
	{
		if (dayNightCycle == false)
		{
			avoidance = Random.Range (0.0f, 100.0f);
			if (avoidance <= chanceToAvoid)
			{
				Debug.Log("J'esquive, au calme.");
				damage = 0;
			}
			
			if (avoidance > chanceToAvoid)
			{
				hitPoints -= damage;
			}
		}
		
		if (dayNightCycle == true)
		{
			hitPoints -= damage;
		}

		if(hitPoints <= 0) Death();
	}
}