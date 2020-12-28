using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}