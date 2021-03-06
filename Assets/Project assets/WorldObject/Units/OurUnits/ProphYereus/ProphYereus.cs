﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TD;

public class ProphYereus : Unit {
	
	protected float timer;
	public int goldNight;
	public float delayGold;
	
	private Quaternion targetRotation;
	protected Quaternion aimRotation;

	public Spawner spawner;
	
	
	
	protected override void Start () {
		base.Start ();
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
		timer += Time.deltaTime;
		SoulGenerate ();
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
		if (dayNightCycle){
			Vector3 spawnPoint = transform.position;
			spawnPoint.x += (2.1f * transform.forward.x);
			spawnPoint.y += (2.1F * transform.forward.y);
			spawnPoint.z += (2.1f * transform.forward.z);
			GameObject gameObject = (GameObject)Instantiate(ResourceManager.GetWorldObject("ProphYereusProjectile"), spawnPoint, transform.rotation);
			Projectile projectile = gameObject.GetComponentInChildren< Projectile >();
			projectile.SetRange(0.9f * weaponRange);
			projectile.SetTarget(target);
		}
	}
	public void StartMove(Vector3 destination) {	//Lance la rotation (peut etre modifié pour se déplacer vers la cible).
		this.destination = destination;
		aimRotation = Quaternion.LookRotation (destination - transform.position);
		rotating = true;
		moving = false;
	}

	//Génération de Ressources de nuit (toutes les 2 secondes,)
	public void SoulGenerate () {
		if (!dayNightCycle && timer >= delayGold && spawner.pause == false){
			controlledPlayer =(Player)GameObject.FindWithTag("Player").GetComponent(typeof(Player));
			controlledPlayer.AddResource (ResourceType.Money, goldNight);
			timer = 0;
		}
	}

}
