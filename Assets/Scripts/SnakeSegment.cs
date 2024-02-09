using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeController controller = collision.GetComponent<SnakeController>();

        if (controller != null && UILevel.Instance != null)
        {
            UILevel.Instance.GameOver();
            controller.enabled = false;
        }
    }
}