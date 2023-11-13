using UnityEngine.SceneManagement;

public class GameFacade
{
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}