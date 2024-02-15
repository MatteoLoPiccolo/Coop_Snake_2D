using UnityEngine;

public class MassGainer : MonoBehaviour
{
    [SerializeField] private int _score = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeController controller = collision.gameObject.GetComponent<SnakeController>();

        if (controller != null)
        {
            GameManager.Instance.SpawnMassGainerAtRandomPosition();
            controller.UpdateScore(_score);

            if(!controller.IsSnake2)
                UILevel.Instance.UpdateSnakeOneScore();
            else
                UILevel.Instance.UpdateSnakeTwoScore();

            controller.Grow();
            Destroy(gameObject);
        }
    }
}