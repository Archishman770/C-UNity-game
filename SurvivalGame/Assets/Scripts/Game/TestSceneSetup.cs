using UnityEngine;

public class TestSceneSetup : MonoBehaviour
{
    [Header("UI")]
    public GameObject hudPrefab;
    [Header("Player")]
    public GameObject playerPrefab;
    public Vector2 playerSpawnPosition = new Vector2(0, 0);

    [Header("Boss")] 
    public GameObject bossPrefab;
    public Vector2 bossSpawnPosition = new Vector2(5, 0);

    [Header("Camera")]
    public float cameraSize = 5f;
    public Vector3 cameraOffset = new Vector3(0, 0, -10);

    void Start()
    {
        // Initialize HUD
        if (hudPrefab != null)
        {
            Instantiate(hudPrefab);
        }

        // Spawn player
        GameObject player = Instantiate(
            playerPrefab, 
            playerSpawnPosition, 
            Quaternion.identity
        );
        player.tag = "Player";

        // Spawn boss
        if (bossPrefab != null)
        {
            Instantiate(
                bossPrefab, 
                bossSpawnPosition, 
                Quaternion.identity
            );
        }

        // Camera setup
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = cameraSize;
        Camera.main.transform.position = new Vector3(
            playerSpawnPosition.x + cameraOffset.x,
            playerSpawnPosition.y + cameraOffset.y,
            cameraOffset.z
        );

        // Set sorting layers
        if (playerPrefab != null)
        {
            playerPrefab.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
        }
        if (bossPrefab != null) 
        {
            bossPrefab.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
        }

        // Start game music
        SoundManager.Instance.PlayMusic("ExplorationMusic");
    }
}