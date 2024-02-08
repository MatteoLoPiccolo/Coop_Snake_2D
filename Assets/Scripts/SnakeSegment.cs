using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeSegment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeController controller = collision.GetComponent<SnakeController>();

        if (controller != null )
        {
            SceneManager.LoadScene(0);
        }
    }
}