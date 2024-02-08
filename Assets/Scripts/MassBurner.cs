using UnityEngine;

public class MassBurner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeController controller = collision.gameObject.GetComponent<SnakeController>();

        if (controller != null)
        {
            //GameManager.Instance.SpawnMassBurnerAtRandomPosition();
            controller.Shrink();
            Destroy(gameObject);
        }
    }
}
