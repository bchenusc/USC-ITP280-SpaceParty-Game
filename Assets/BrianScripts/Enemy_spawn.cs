using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_spawn : MonoBehaviour {
	public int range = 0;
	public int num=0;
	public Transform[] enemyPrefabs;
	public Transform[] coins;
	public Transform[] powerUps;
	public float max_speed = 3;
	private float clampSpeed=20;
	
	
	public float timeAtStart=0;
	
	private int waveNum=0;
	private bool canSpawnNextWave=true;
	private Transform spawnThisEnemy;
	
	private GUISCRIPTS guiscript;
	
	Transform player;
	
	public float cycleTimer;
	private float resetCycleTimer = 3;
	Player_move playerMoveScript;
	
	private List<int> waveArray = new List<int>();
	
	public GUIStyle guistyle;

	public void AddWave(int i){
		waveArray.Add(i);	
	}
	
	// Use this for initialization
	void Start () {
		cycleTimer = 0;
		max_speed=Mathf.Exp (1);
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerMoveScript = player.GetComponent<Player_move>();
		
	}
	// Update is called once per frame
	void Update () {
		if ((int)(Time.timeSinceLevelLoad-timeAtStart)%10==0){
			max_speed= Mathf.Clamp(2+Mathf.Log((Time.timeSinceLevelLoad-timeAtStart)),4,clampSpeed);
			playerMoveScript.speed = max_speed;
		}
		if (!playerMoveScript.playerDead){
			#region Cycle enemy region
			if (cycleTimer==0){
				if (waveNum<waveArray.Count)
				spawnWave(waveArray[waveNum]);
			}
			if (cycleTimer<0){
				cycleTimer = 0;	
			}
			if (cycleTimer>0 && playerMoveScript.playerDead==false){
				cycleTimer-=Time.deltaTime;	
			}
			#endregion
		}
		
	}
	
	void spawnWave(int wave){
		if (!canSpawnNextWave){
			return;	
		}
		canSpawnNextWave =false;
		switch (wave){
		case 0:
			//Spawn entrance
			num=3;
			StartCoroutine(spawnCycle(Random.Range (0,3)));
			break;
		case 1:
			//spawn coins mid
			num=53;
			StartCoroutine(spawnCycle(Random.Range (0,1)));
			break;
		case 2:	
			//Random Spawns
			num=22;	//Number of random spawns
			StartCoroutine(randomSpawn(0.5f));
			break;
		case 3:
			//spawn coins -->
			num=51;
			StartCoroutine(spawnCycle(0.5f));
			break;
		case 4:
			//Spawn middle barrier.
			num=2;
			StartCoroutine(spawnCycle(2.0f));
			break;
		case 5:
			//coin clump <--
			num=52;
			StartCoroutine(spawnCycle(0.5f));
			break;
		case 6:
			//LeftBarrier 
			num=0;
			StartCoroutine(spawnCycle(0.5f));
			break;
		case 7:
			//and RightBarrier
			num=1;
			StartCoroutine(spawnCycle(0.5f));
			break;
		case 8:
			//right / fast barrier
			num=8;
			StartCoroutine(spawnCycle(2));
			break;
		case 9:
			//coin clump random
			num=53;
			StartCoroutine(spawnCycle (0.5f));
			break;
		case 10:
			//Spawn 40 random.
			num=70;
			StartCoroutine(randomSpawn(0.5f));
			break;
		case 11:
			//spawn 2d movement.
			num=100;
			StartCoroutine(spawnCycle(7));
			break;
		case 12:
			//Spawn big "/" only opening on right.
				num=9;
				StartCoroutine (spawnCycle(1));
			break;
		case 13:
			//Spawn a 10
				num=15;
				StartCoroutine(randomSpawnSide(0.5f));
			break;
		case 14:
			//spawn a shield.
				num=101;
				StartCoroutine(spawnCycle(0.5f));
			break;
		case 15:
			//spawn magnet
			num = 102;
			StartCoroutine(spawnCycle (0.5f));
			break;
		case 16:
			num =12;
			StartCoroutine(spawnCycle(0));
			break;
			
		case 17:
			num =13;
			StartCoroutine(spawnCycle(0));
			break;
		case 18:
			num =14;
			StartCoroutine(spawnCycle(0));
			break;
		case 19:
			num =15;
			StartCoroutine(spawnCycle(0));
			break;
		case 20:
			num =16;
			StartCoroutine(spawnCycle(0));
			break;
		case 21:
			num = 100;
			StartCoroutine(randomSpawnSide(0.5f));
			break;
		case 22:
			num=0;
			StartCoroutine(randomSpawnSide (0.5f));
			break;
		case 23:
			num=100;
			StartCoroutine(randomSpawn(0));
			StartCoroutine(randomSpawnSide(0));
			break;
		
		default: break;
		}
		if (!playerMoveScript.playerDead){
			if (waveNum<waveArray.Count)
				waveNum++;
			cycleTimer=resetCycleTimer;
		}
		
	}
	
	IEnumerator randomSpawn(float waitTime){
		//This function's number of random spawns is based on num the class variable.
		for(int i=0;i<num;i++){
			if (i%2 == 0)
				spawnEnemy ((int)(Random.Range(-640, 640)),enemyPrefabs[0]);
			if (i%2 == 1)
				spawnTracker((int)(Random.Range (-640,640)),enemyPrefabs[1]);
			
			yield return new WaitForSeconds(waitTime);
		}
		yield return new WaitForSeconds (4);
		canSpawnNextWave=true;
	}
	
	IEnumerator randomSpawnSide(float waitTime){
		//This function's number of random spawns is based on num the class variable.
			for(int i=0;i<num;i++){
				if (i%2==0){
					int leftRight = (int)Random.Range (0.0f,1.9f);
					if (leftRight==0)
						spawnEnemyLeft ((int)(Random.Range(-450, 450)+50),enemyPrefabs[0]);
					if (leftRight==1)
						spawnEnemyRight ((int)(Random.Range(-450, 450)+50),enemyPrefabs[0]);
				}
				if (i%2==1){
					int leftRight = Random.Range (0,1);
					if (leftRight==0)
						spawnTrackerLeft ((int)(Random.Range(-450, 450)+50),enemyPrefabs[1]);
					if (leftRight==1)
						spawnTrackerRight ((int)(Random.Range(-450, 450)+50),enemyPrefabs[1]);
				}
				yield return new WaitForSeconds(waitTime);
			}

		yield return new WaitForSeconds (4);
		canSpawnNextWave=true;
	}
	
	IEnumerator spawnCycle(float waitTime){

		switch (num){
		case 0:
			//Left barrier.
			for (int i=-10; i<0; i++){
				
				spawnEnemy((int)(-675*i/-10.0),enemyPrefabs[0]);
			}
			break;
		case 1:
			//Right barrier

			for (int i=0; i<10; i++){
				spawnEnemy((int)(-675*(i/-10.0)),enemyPrefabs[0]);
			}
			
			break;
		case 2:
			//Middle barrier
			for (int i=-5; i<5; i++){
				spawnEnemy((int)(-675*(i/-10.0)),enemyPrefabs[0]);
			}
			break;
		case 3:
			//Split the middle
			for (int i=-10; i<-2; i++){
				spawnEnemy((int)(-675*(i/-10.0)), enemyPrefabs[0]);
				
			}
			for (int i=2; i<10; i++){
				spawnEnemy((int)(-675*(i/-10.0)), enemyPrefabs[0]);
			} 
			break;
		case 4:
			//Right '/' barrier fast
			for(int i=0; i<16; i++){
				spawnEnemy((int)(-350*(i/-10.0)), enemyPrefabs[0]);
				yield return new WaitForSeconds(0.1f);
			}

			break;
		case 5:
			//Right '/' slow barrier
			for (int i=0; i<5; i++){
				spawnEnemy((int)(-675*(i/-10.0)),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.5f);
			}
			break;
		case 6:
			//slow '\'
			for (int i=0; i>-5; i--){
				spawnEnemy((int)(-675*(i/-10.0)),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.5f);
			}
			break;
		case 7:
			//slow '/' accross the entire screen
			for (int i=-10; i<10; i+=2){
				spawnEnemy((int)(-675*(i/-10.0)),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.9f);
			}
			break;
		case 8:
			//Arrow \ then /
			for (int i=-1; i>-10; i--){
				spawnEnemy((int)(-675*(i/-10.0)),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.2f);
			}
			for (int i=1; i<10; i++){
				spawnEnemy((int)(-675*(i/-10.0)),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.2f);
			}
			break;
		case 9:
			//Big / only opening on the right.
			for (int i=-10; i<7; i++){
				spawnEnemy((int)(-675*(i/-10.0)),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.1f);
			}
			break;
		case 10:
			//Big / only opening on the right.
			for (int i=20; i>3; i--){
				spawnEnemy((int)(-675*(i/-10.0)-675),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.1f);
			}
			break;
		case 11:
			//Random spawns
			spawnEnemy ((int)(Random.Range(-640, 640)),enemyPrefabs[1]);
			break;
		case 12:
			//Spawn enemies from left
			//Split the middle
			for (int i=-10; i<-2; i++){
				spawnEnemyLeft((int)(-400*(i/-10.0)+50), enemyPrefabs[0]);
			}
			for (int i=2; i<10; i++){
				spawnEnemyLeft((int)(-400*(i/-10.0)+50), enemyPrefabs[0]);
			} 
			break;
		case 13:
			//LEFT SIDE 
			//Arrow \ then /
			for (int i=-1; i>-10; i--){
				spawnEnemyLeft((int)(-500*(i/-10.0)+50),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.2f);
			}
			for (int i=1; i<10; i++){
				spawnEnemyLeft((int)(-500*(i/-10.0)+50),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.2f);
			}
			break;
		case 14:
			//Left Side
			//Middle barrier
			for (int i=-5; i<5; i++){
				spawnEnemyLeft((int)(-500*(i/-10.0)+50),enemyPrefabs[0]);
			}
			break;
		case 15:
			//Left Side
			//Big / only opening on the right.
			for (int i=-10; i<6; i++){
				spawnEnemyLeft((int)(-500*(i/-10.0)+50),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.1f);
			}
			break;
		case 16:
			//Left Side
			//Big / only opening on the right.
			for (int i=20; i>4; i--){
				spawnEnemyLeft((int)(-450*(i/-10.0)-500),enemyPrefabs[0]);
				yield return new WaitForSeconds(0.1f);
			}
			break;
			
		///-------------------------///
		//Spawn coins.
		case 50:
			//Coin clump 4x4
			for(int j=0; j<4;j++){
				for (int i=-2; i<=2; i++){
					spawnCoin(50*i, coins[0]);
				}
				yield return new WaitForSeconds(0.2f);
			}
			break;
		case 51:
			//Coin clump -->4x4
			for(int j=0; j<4;j++){
				for (int i=-2; i<=2; i++){
					spawnCoin(500+50*i, coins[0]);
				}
				yield return new WaitForSeconds(0.2f);
			}
			break;
			
		case 52:
			//Coin clump <--4x4
			for(int j=0; j<4;j++){
				for (int i=-2; i<=2; i++){
					spawnCoin(-500+50*i,coins[0]);
				}
				yield return new WaitForSeconds(0.2f);
			}
			break;
		
		case 53:
			//Random coin clump
			int offset = Random.Range(-500,500);
			for (int j=0; j<4;j++){
				for (int i=-2; i<=2;i++){
					spawnCoin(offset+50*i, coins[0]);
				}
			}
			break;
			//Spawn powerups
		case 100:
			//spawn 2d movement.
				spawnEnemy(Random.Range (-500,500), powerUps[0]);
			break;
		case 101:
			//Spawn shield.
				spawnShield(Random.Range (-500,500),powerUps[1]);
			break;
		case 102:
			//Spawn magnet.
				spawnMagnet (Random.Range(-500,500), powerUps[2]);
			break;
		}
		yield return new WaitForSeconds(waitTime);
		canSpawnNextWave=true;
	}
	
	void spawnEnemy(int position, Transform enemy){
			Transform clone = Instantiate(enemy, new Vector3(position,transform.position.y,transform.position.z), Quaternion.identity) as Transform;
			clone.GetComponent<Enemy_move>().direction = -Vector3.forward;
			clone.GetComponent<Enemy_move>().speed = max_speed;
	}
	
	void spawnTracker(int position, Transform enemy){
			Transform clone = Instantiate(enemy, new Vector3(position,transform.position.y,transform.position.z), Quaternion.identity) as Transform;
			clone.GetComponent<Enemy_move>().direction = Vector3.Normalize(player.position-clone.position);
			clone.GetComponent<Enemy_move>().speed = max_speed;
	}
	
	void spawnTrackerLeft(int position, Transform enemy){
			Transform clone = Instantiate(enemy, new Vector3(-700,transform.position.y,position), Quaternion.identity) as Transform;
			clone.GetComponent<Enemy_move>().direction = Vector3.Normalize(player.position-clone.position);
			clone.GetComponent<Enemy_move>().speed = max_speed;
	}
	
	void spawnTrackerRight(int position, Transform enemy){
			Transform clone = Instantiate(enemy, new Vector3(700,transform.position.y,position), Quaternion.identity) as Transform;
			clone.GetComponent<Enemy_move>().direction = Vector3.Normalize(player.position-clone.position);
			clone.GetComponent<Enemy_move>().speed = max_speed;
	}
	
	void spawnEnemyLeft(int position, Transform enemy){
		Transform clone = Instantiate(enemy, new Vector3(-700,transform.position.y,position), Quaternion.identity) as Transform;
		clone.GetComponent<Enemy_move>().direction = Vector3.right;
		clone.GetComponent<Enemy_move>().speed = max_speed;
	}
	void spawnEnemyRight(int position, Transform enemy){
		Transform clone = Instantiate(enemy, new Vector3(700,transform.position.y,position), Quaternion.identity) as Transform;
		clone.GetComponent<Enemy_move>().speed = max_speed;
	}
	
	void spawnCoin(int position, Transform coin){
			Transform clone = Instantiate(coin, new Vector3(position,transform.position.y,transform.position.z), Quaternion.identity) as Transform;
			clone.GetComponent<Coin_Script>().speed = max_speed;
	}
	
	void spawnShield(int position, Transform shield){
		Transform clone = Instantiate (shield, new Vector3(position,transform.position.y,transform.position.z), Quaternion.identity) as Transform;
		clone.GetComponent<FallingShieldIcon>().speed = max_speed;
	}
		
	void spawnMagnet(int position, Transform magnet){
		Transform clone = Instantiate (magnet, new Vector3(position,transform.position.y,transform.position.z), Quaternion.identity) as Transform;
		clone.GetComponent<FallingMagnetScript>().speed = max_speed;
	}
}
