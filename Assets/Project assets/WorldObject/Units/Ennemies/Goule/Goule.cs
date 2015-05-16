using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TD;

public class Goule : EnnemiUnit 
{
	public GameObject enemyToSpawn;
	public float resurrectionRange;
	public int chanceToResurrect;

	private bool isResurrecting;
	private int resurrectionValue;
	private float distance;

	private int loseLife;
	
	void Start () 
	{
		monObjetSuivi = GameObject.Find("Castle");
		SetEnnemiDestination ();
	}

	void Update () 
	{
		base.Update();
	
		// Needed to lose lifes
		if (HitSomething()) 
		{
			loseLife = 0-dommages;
			DayAndNightCycle.numberOfLifes -= dommages; // TEMPORAIRE --> POUR LE RENDU UNIQUEMENT
			controlledPlayer =(Player)GameObject.FindWithTag("Player").GetComponent(typeof(Player));
			controlledPlayer.AddResource (ResourceType.Power, loseLife);
			Destroy(this.gameObject);
		}

			if (isDead == true && isResurrecting == false) // if an enemy dies
			{
				isResurrecting = true;
				resurrectionValue = Random.Range(0, 100); // check if it resurrects
				Debug.Log ("Resurrection value: " + resurrectionValue);

				if (resurrectionValue < chanceToResurrect) // if yes, check distance
				{
					distance = Vector3.Distance (this.transform.position, positionOfDeadEnemy); // get distance between goule and dying enemy
					Debug.Log ("Distance: " + distance);

					if (distance < resurrectionRange && isResurrecting == true) // if the goule has the range
					{
						isResurrecting = true;
						GouleResurrection(); // RESURRECTION
					}
				}
			}

			else
			{
				isDead = false;
				isResurrecting = false;
			}
		//}
	}

	void GouleResurrection()
	{
		Debug.Log ("HOLY MADA RESURRECTION");
		isDead = false;
		Instantiate(enemyToSpawn, positionOfDeadEnemy, transform.rotation);
		Debug.Log ("IS DEAD 2: " + isDead);
		isResurrecting = false;
	}
}