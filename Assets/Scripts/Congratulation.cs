using UnityEngine;
using UnityEngine.SceneManagement;

public class Congratulation : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField]
    private ButtonPro backBtn = null;
    [SerializeField]
    private ButtonPro quitBtn = null;
    #endregion


    #region Unity Methods
    private void Start()
    {
        backBtn.Init(Localization.BACK);
        quitBtn.Init(Localization.QUIT);
    }
    #endregion
    
    #region Buttons Methods
    public void BackHandler()
    {
        SceneManager.LoadScene(0);
    }
    
    public void QuitHandler()
    {
        Application.Quit();
    }
    #endregion
}
