using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TD;

[System.Serializable]

public class Vagues
{
    public string name;
    public List<Unit> ennemis;    
}

public class Spawner : MonoBehaviour
{

    public List<Vagues> wavesList;
	public float spawnDelay;
	public float waveDelay; 
    protected Vagues currentWave;
    private bool onGoing;
    private int activeWave;
    private Unit activeUnit;
	private int activeUnitNumber;
    private float timer;
	private int waveCount;
	public bool launching = false;

    private Vector3 spawnSpot;

	public bool pause = true;
    
    private void Start()
    {
        spawnSpot.x = this.transform.position.x;
        spawnSpot.y = (this.transform.position.y + 1f);
        spawnSpot.z = this.transform.position.z;
		activeWave = 0;
		onGoing = false;
		activeUnitNumber = 0;
		NextWave ();
		
		
    }


    private void Update() 
    {
        timer += Time.deltaTime;
        if (onGoing)
        {
            if (activeUnit != null)
            {
                WaveSpawn();
            }
            else
            {
                onGoing = false;
				
            }       
        }
		if (activeUnitNumber >= waveCount)
		{
			onGoing = false;
			if(timer >= waveDelay )
			{	
				pause = true;		
				if (launching == true)
				{
				timer = 0;
				NextWave ();
				}
				else
				{
				launching = false;
				}
				launching = false;
				
			}
			
		}  

		if (Input.GetKeyDown (KeyCode.P)) {
			if (Time.timeScale == 1){
				Time.timeScale = 0;
			}
			else
			{
				Time.timeScale = 1;
			}
			
		}
	}

    public void NextWave()
    {
		pause = false;
		activeUnitNumber = 0;
		currentWave = wavesList [activeWave];
		activeUnit = currentWave.ennemis [activeUnitNumber];
		onGoing = true;
		activeWave++;
		waveCount = currentWave.ennemis.Count;
		if (activeWave >= (wavesList.Count))
		{
			onGoing = false;
		}
    }

    private void WaveSpawn()
    {
        if (timer >= spawnDelay && launching == true)
		{
			timer = 0.0f;
			activeUnit =  currentWave.ennemis [activeUnitNumber];
			activeUnitNumber ++;
			Instantiate(activeUnit, spawnSpot, transform.rotation);
			waveCount = currentWave.ennemis.Count;
		}
		

    }

}
