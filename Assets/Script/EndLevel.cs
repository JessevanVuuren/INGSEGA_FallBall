using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene(0);
        }
    }
}
