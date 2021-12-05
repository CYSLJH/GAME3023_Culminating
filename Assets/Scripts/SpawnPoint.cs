using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    static PlayerBehaviour player = null;

    private void Awake()
    {
        if(player == null)
        {
            GameObject newPlayerSpawned = Instantiate(playerPrefab, transform.position, transform.rotation);
            player = newPlayerSpawned.GetComponent<PlayerBehaviour>();
        }
    }
}
