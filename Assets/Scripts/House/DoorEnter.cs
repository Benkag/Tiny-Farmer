using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnter : MonoBehaviour
{
    [Header("Tên scene bên trong nhà")]
    public string sceneToLoad = "HouseInterior";

    [Header("Tên điểm spawn khi ra khỏi nhà")]
    public string exitSpawnPointName = "DoorOutsideSpawn";

    private bool isLoading = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLoading) return;
        if (other.CompareTag("Player"))  // nhân vật phải có tag Player
        {
            isLoading = true;
            Debug.Log("[DoorEnter] Vào cửa, load scene " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
