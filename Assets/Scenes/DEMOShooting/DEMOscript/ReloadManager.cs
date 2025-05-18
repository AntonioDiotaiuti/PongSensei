using UnityEngine;

public class ReloadManager : MonoBehaviour
{
	[SerializeField] private GameObject reloadItemPrefab;
	[SerializeField] private Transform spawnPoint;

	private bool player1Shot = false;
	private bool player2Shot = false;
	private bool itemSpawned = false;

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
		if (playerNumber == "1")
		{
			player1Shot = true;
		}
		else if (playerNumber == "2")
		{
			player2Shot = true;
		}

		// Se non è ancora stato spawnato
		if (!itemSpawned && (player1Shot || player2Shot))
		{
			SpawnReloadItem();
			itemSpawned = true;
		}
		// Dopo il primo spawn, spawna solo quando entrambi hanno sparato almeno una volta
		else if (player1Shot && player2Shot)
		{
			SpawnReloadItem();
			itemSpawned = true;
			player1Shot = false;
			player2Shot = false;
		}
	}

	private void SpawnReloadItem()
	{
		Instantiate(reloadItemPrefab, spawnPoint.position, Quaternion.identity);
	}
}
