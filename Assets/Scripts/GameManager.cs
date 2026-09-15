using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    public bool won { get; private set; }
    public bool fellOutOfBounds { get; private set; }

    public void Win()
    {
        won = true;
        winScreen.SetActive(true);
    }

    public void FellOutOfBounds()
    {
        fellOutOfBounds = true;
    }
}
