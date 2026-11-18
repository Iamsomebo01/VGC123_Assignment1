
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private Pickup[] collectibles;
    [SerializeField] private Transform spawnLocationsParent;
    private Transform[] spawnLocations;


    // Start is called once before the first execution of Update
    void Start()
    {
    GetSpawnLocations();
    SpawnCollectibles();
    }


    // Gets all spawn locations parented to the assigned parent
    void GetSpawnLocations()
    {
        int spawnLocationCount = spawnLocationsParent.childCount;
        spawnLocations = new Transform[spawnLocationCount];

        for (int spawnIndex = 0; spawnIndex < spawnLocationCount; spawnIndex++)
        {
            spawnLocations[spawnIndex] = spawnLocationsParent.GetChild(spawnIndex);
        }
    }


    // Spawns a random collectible at every spawn location
    void SpawnCollectibles()
    {
        // Check if collectibles exist
        if (collectibles == null || collectibles.Length == 0)
        {
            Debug.LogWarning("No collectibles assigned!");
            return;
        }

        // Check if spawn locations exist
        if (spawnLocations == null || spawnLocations.Length == 0)
        {
            Debug.LogWarning("No spawn locations found!");
            return;
        }

        // Loop through every spawn location
        for (int spawnIndex = 0; spawnIndex < spawnLocations.Length; spawnIndex++)
        {
            Transform currentSpawnLocation = spawnLocations[spawnIndex];

            // Randomly select a collectible
            int randomCollectibleIndex = Random.Range(0, collectibles.Length);
            Pickup selectedPickup = collectibles[randomCollectibleIndex];

            // Check if selected collectible exists
            if (selectedPickup == null)
            {
                Debug.LogWarning("Selected collectible is missing!");
                continue;
            }

            // Spawn collectible at the current location
            Instantiate(selectedPickup, currentSpawnLocation.position, currentSpawnLocation.rotation);

            Debug.Log("Spawned: " + selectedPickup.name);
        }
    }
}
