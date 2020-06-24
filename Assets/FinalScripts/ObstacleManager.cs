using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public PlayerController playerHealth;


    public GameObject[] ObjectCarsFrequent;
    public GameObject[] objectCarsRare;

    public GameObject Ambulance;

    public float safePositionToSpawnNewObstacleY;

    public float minDistaceToSlipBetweenLanes;
    
    public List<GameObject> ObstaclesInScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        DestroyObstacle();
        SpawnObjects();        
    }





    private bool PossibilityOfSpawning(float spawnXPosition)
    {
        bool canSpawn;

        foreach(GameObject obstacle in ObstaclesInScene)
        {
            if(obstacle.transform.position.x == spawnXPosition)
            {
                if (obstacle.transform.position.y > safePositionToSpawnNewObstacleY)
                {
                    canSpawn = false;
                    return canSpawn;
                }
            }
        }
        canSpawn = true;
        return canSpawn;
    }


    private bool PossibilityOfLaneChangesBetweenCars()
    {
        bool canSpawn;

        foreach(GameObject obstacle in ObstaclesInScene)
        {
            if (transform.position.y - obstacle.transform.position.y < minDistaceToSlipBetweenLanes)
            {
                canSpawn = false;
                return canSpawn;
            }
        }
        canSpawn = true;
        return canSpawn;
    }


    private void DestroyObstacle()
    {
        if (ObstaclesInScene.Count > 0)
        {
            if (ObstaclesInScene[0].transform.position.y < -20f)
            {

                Destroy(ObstaclesInScene[0]);
                ObstaclesInScene.RemoveAt(0);
            }
        }
    }


    private void SpawnObjects()
    {
        if (PossibilityOfSpawning(PlayerController.possiblePosX[PlayerController.currentPosX]) && PossibilityOfLaneChangesBetweenCars() && playerHealth.Health < playerHealth.criticalHealth && Random.Range(1, 99) > 75)
        {
            GameObject obstacle = Instantiate(Ambulance);
            obstacle.transform.position = new Vector2(PlayerController.possiblePosX[PlayerController.currentPosX], transform.position.y);
            ObstaclesInScene.Add(obstacle);
        }


        if (PossibilityOfSpawning(PlayerController.possiblePosX[PlayerController.currentPosX]) && PossibilityOfLaneChangesBetweenCars() && Random.Range(1, 99) > 75)
        {
            GameObject obstacle = Instantiate(objectCarsRare[Random.Range(0, objectCarsRare.Length)], transform);
            obstacle.transform.position = new Vector2(PlayerController.possiblePosX[PlayerController.currentPosX], transform.position.y);
            ObstaclesInScene.Add(obstacle);
        }

        if (PossibilityOfSpawning(PlayerController.possiblePosX[PlayerController.currentPosX]) && PossibilityOfLaneChangesBetweenCars())
        {
            GameObject obstacle = Instantiate(ObjectCarsFrequent[Random.Range(0, ObjectCarsFrequent.Length)], transform);
            obstacle.transform.position = new Vector2(PlayerController.possiblePosX[PlayerController.currentPosX], transform.position.y);
            ObstaclesInScene.Add(obstacle);
        }        
    }
}
