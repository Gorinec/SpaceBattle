using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BorderArena : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.deathEffect != null)
            {
                Instantiate(playerHealth.deathEffect, other.transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject);

            
            StartCoroutine(LoadLoseSceneAfterDelay());
        }
    }

    IEnumerator LoadLoseSceneAfterDelay()
    {
        yield return new WaitForSeconds(2f); 
        SceneManager.LoadScene(3); 
    }
}
