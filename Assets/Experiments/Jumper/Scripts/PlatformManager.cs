using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

	public Transform platformParent;
	public Transform platformSpawnLocation;
	public float leftEdge;
	public float rightEdge;
	public bool initialRun = true;

	public AnimationCurve spaceBetweenPlatforms;
	private Vector3 lastSpawnPosition;
	private Vector3 nextSpawnPosition;

	private float zPos = 0;
	private float yPos;
	private float xPos;

	private GameObject platform;

	private int platformSize;

	void Start () {
		GameManagerJump.instance.platformManager = this;
		lastSpawnPosition = platformSpawnLocation.position;
		StartCoroutine(SpawnPlatform());
	}

	void Update () {
		
	}
		
	IEnumerator SpawnPlatform () 
	{
		yield return new WaitForSeconds (GameManagerJump.instance.gameStartWaitTime);

		zPos = 0;

		while (!GameManagerJump.instance.isGameOver) {
			yield return new WaitForSeconds (0.1f);
			xPos = Random.Range (leftEdge, rightEdge);
			if (initialRun) {
				yPos = platformSpawnLocation.position.y;
				initialRun = false;
			} else {
				yPos = lastSpawnPosition.y + Random.Range (spaceBetweenPlatforms.keys [0].value, spaceBetweenPlatforms.keys [1].value);
			}

			nextSpawnPosition = new Vector3 (xPos, yPos, zPos);

			platformSize = Random.Range (1, 3);
			platform = Pooler.instance.RequestFromPool (platformSize);
			if (platform != null) {
				platform.transform.position = nextSpawnPosition;
				platform.SetActive (true);
				lastSpawnPosition = platform.transform.position;
			}

		}
	}
}
