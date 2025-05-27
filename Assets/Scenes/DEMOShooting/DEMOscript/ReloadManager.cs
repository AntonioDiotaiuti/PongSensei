using UnityEngine;

public class ReloadManager : MonoBehaviour
{
	[SerializeField] private GameObject reloadItemPrefab;
	[SerializeField] private Transform spawnPoint;

	private bool player1Shot = false;
	private bool player2Shot = false;
	private bool firstSpawnDone = false;

	private void OnEnable()
	{
		ReloadSystem.OnPlayerShot += OnPlayerShot;
	}

	private void OnDisable()
	{
		ReloadSystem.OnPlayerShot -= OnPlayerShot;
	}

	private void OnPlayerShot(string playerNumber)
	{
		if (playerNumber == "1") player1Shot = true;
		else if (playerNumber == "2") player2Shot = true;

		if (ReloadItemExistsInScene()) return;

		if (!firstSpawnDone && (player1Shot || player2Shot))
		{
			SpawnReloadItem();
			firstSpawnDone = true;
		}
		else if (firstSpawnDone && player1Shot && player2Shot)
		{
			SpawnReloadItem();
			player1Shot = false;
			player2Shot = false;
		}
	}

	private void SpawnReloadItem()
	{
		Instantiate(reloadItemPrefab, spawnPoint.position, Quaternion.identity);
	}

	private bool ReloadItemExistsInScene()
	{
		return GameObject.FindWithTag("MapAmmo") != null;
	}
}
