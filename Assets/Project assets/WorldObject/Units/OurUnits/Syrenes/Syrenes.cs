using UnityEngine;
using System.Collections;
using TD;

public class Syrenes : Unit {
	
	
	private Quaternion targetRotation;
	protected Quaternion aimRotation;
	protected DayAndNightCycle prefab;
	
	
	protected override void Start () {
		base.Start ();
		actions = new string[] { "EnterTower", "Blank", "Blank", "Blank", "Sirene" };
		GameObject prefabX = GameObject.Find("DayNight");
		prefab = (DayAndNightCycle)prefabX.GetComponent<DayAndNightCycle> ();
	}
	
	protected override void Update () {
		base.Update();
		if(aiming) {	 //Gère la rotation de l'unité en direction de l'ennemi lorsqu'il le vise.
			transform.rotation = Quaternion.RotateTowards(transform.rotation, aimRotation, weaponAimSpeed);
			CalculateBounds();
			//sometimes it gets stuck exactly 180 degrees out in the calculation and does nothing, this check fixes that
			Quaternion inverseAimRotation = new Quaternion(-aimRotation.x, -aimRotation.y, -aimRotation.z, -aimRotation.w);
			if(transform.rotation == aimRotation || transform.rotation == inverseAimRotation) {
				aiming = false;
			}
		}

	}
	public override bool CanAttack() {	 //définis que cette unité peut attaquer.
		return true;
	}
	protected override void AimAtTarget () { 	//Définis qu'il doit viser la cible
		base.AimAtTarget();
		aimRotation = Quaternion.LookRotation (target.transform.position - transform.position);
	}
	protected override void UseWeapon () {		//Définis ce qu'il se passe lorsqu'on demande à l'unité d'attaquer.
		base.UseWeapon();
		Vector3 spawnPoint = transform.position;
		spawnPoint.x += (2.1f * transform.forward.x);
		spawnPoint.y += (2.1F * transform.forward.y);
		spawnPoint.z += (2.1f * transform.forward.z);
		GameObject gameObject = (GameObject)Instantiate(ResourceManager.GetWorldObject("SoldierProjectile"), spawnPoint, transform.rotation);
		Projectile projectile = gameObject.GetComponentInChildren< Projectile >();
		projectile.SetRange(0.9f * weaponRange);
		projectile.SetTarget(target);
	}
	public void StartMove(Vector3 destination) {	//Lance la rotation (peut etre modifié pour se déplacer vers la cible).
		this.destination = destination;
		aimRotation = Quaternion.LookRotation (destination - transform.position);
		rotating = true;
		moving = false;
	}

	//Function du changement jour/nuit
	//D'apres Keumar, le code peut etre simplifié en utilisant une seule variable booléenne qui agit comme un toggle jour/nuit. C'EST REGLE

	public void DayNightToggle () {
		if (DayAndNightCycle.day == true) {
			prefab.timerDayAndNight = prefab.dayTime ;
		}
		if (DayAndNightCycle.day == false) {
			prefab.timerDayAndNight = prefab.nightTime ;

		}
	}
	
	public override void PerformAction(string actionToPerform) { //définis quoi faire pour chaque action
		base.PerformAction(actionToPerform);
		if (actionToPerform == "Sirene") {
			DayNightToggle();
		}
	}
	
}
