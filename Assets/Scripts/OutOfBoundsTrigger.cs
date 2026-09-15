using UnityEngine;

public class OutOfBoundsTrigger : MonoBehaviour
{
    public bool FellOutOfBounds { get; private set; }

    private void OutOfBoundsEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FellOutOfBounds = true;
        }
    }
}

