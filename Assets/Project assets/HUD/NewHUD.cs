using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TD;


public class NewHUD : MonoBehaviour {

	public Player player;
	protected int soulNumber;
	protected int hpNumber;
	protected string soulNumberString;
	protected string hpNumberString;
	public Text soulNumberAff;
	public Text hpNumberAff;
	
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
	}

	public void FocusChange (int focus)
	{
		player.focus = focus;
	}
}
