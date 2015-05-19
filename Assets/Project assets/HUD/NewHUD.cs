using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TD;


public class NewHUD : MonoBehaviour {

	public Player player;
	public HUD oldHUD;
	public Spawner spawner;
	protected int soulNumber;
	protected int hpNumber;
	protected string soulNumberString;
	protected string hpNumberString;
	public Text soulNumberAff;
	public Text hpNumberAff;

	public Image tooltip;
	public Sprite ttEfrit, ttElephant, ttProph, ttSirene, ttSoldier;
	protected string actualTt;
	public Image statusButton;
	public Text statusButtonText;

	
	// Use this for initialization
	void Start () {
		spawner.launching = false;
	}
	
	// Update is called once per frame
	void Update () {
		soulNumber = player.GetResourceValue (ResourceType.Money);
		soulNumberString = soulNumber.ToString ();
		soulNumberAff.text = soulNumberString;
		hpNumber = player.GetResourceValue (ResourceType.Power);
		hpNumberString = hpNumber.ToString();
		hpNumberAff.text = (hpNumberString);
		actualTt = oldHUD.hover;
		tooltipUp();
		if (spawner.launching)
		{
			statusButton.enabled = false;
			statusButtonText.enabled = false;
		}
		else {
			statusButton.enabled = true;
			statusButtonText.enabled = true;
		}
	}

	public void FocusChange (int focus)
	{
		player.focus = focus;
	}

	protected void tooltipUp ()
	{
		switch (actualTt)
		{
			case "Soldier" :
			tooltip.sprite = ttSoldier;
			tooltip.enabled = true;
			break;
			case "Elephant" :
			tooltip.sprite = ttElephant;
			tooltip.enabled = true;
			
			break;
			case "ProphYereus" :
			tooltip.sprite = ttProph;
			tooltip.enabled = true;
			break;
			case "Sirene" :
			tooltip.sprite = ttSirene;
			tooltip.enabled = true;
			break;
			case "Efrit" :
			tooltip.sprite = ttEfrit;
			tooltip.enabled = true;
			break;
			default :
			tooltip.enabled = false;
			break;

		}
	}

	public void launchingStatus () {
		spawner.launching = !spawner.launching;
		
	}
}
