using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TD;


public class NewHUD : MonoBehaviour {

	public Player player;
	public HUD oldHUD;
	protected int soulNumber;
	protected int hpNumber;
	protected string soulNumberString;
	protected string hpNumberString;
	public Text soulNumberAff;
	public Text hpNumberAff;

	public Image tooltip;
	public Sprite ttEfrit, ttElephant, ttProph, ttSirene, ttSoldier;
	protected string actualTt;
	
	// Use this for initialization
	void Start () {
	
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
}
