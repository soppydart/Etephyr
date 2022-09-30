using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint0 : MonoBehaviour
{
    [SerializeField] int cpNum;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameData gameData = new GameData();
            gameData.playerPosition = new Vector3(FindObjectOfType<PlayerMovement>().transform.position.x,
            FindObjectOfType<PlayerMovement>().transform.position.y,
            FindObjectOfType<PlayerMovement>().transform.position.z);
            gameData.health = FindObjectOfType<PlayerCombat>().GetComponent<PlayerCombat>().GetHealth();
            gameData.sceneNumber = FindObjectOfType<GameController>().GetSceneNumber();
            gameData.checkpointNumber = cpNum;
            SaveManager.Save(gameData);
        }
    }
}
