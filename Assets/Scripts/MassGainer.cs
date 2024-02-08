using UnityEngine;

public class MassGainer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeController controller = collision.gameObject.GetComponent<SnakeController>();

        if (controller != null)
        {
            GameManager.Instance.SpawnMassGainerAtRandomPosition();
            controller.Grow();
            Destroy(gameObject);
        }
    }
}