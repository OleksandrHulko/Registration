using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField]
    private InputFieldPro nickname = null;
    [SerializeField]
    private InputFieldPro mail = null;
    [SerializeField]
    private InputFieldPro password = null;
    [SerializeField]
    private InputFieldPro passwordCheck = null;
    
    [SerializeField]
    private CanvasGroup nicknameAndMailCG = null;
    [SerializeField]
    private CanvasGroup passwordsCG = null;
    
    [SerializeField]
    private ButtonPro nextStageBtn = null;
    [SerializeField]
    private ButtonPro previousStageBtn = null;
    [SerializeField]
    private ButtonPro completeRegistrationBtn = null;
    
    [SerializeField]
    private Text delayText = null;
    #endregion


    #region Unity Methods
    private void Start()
    {
        InitButtons();
        
        InitNicknameField();
        InitMailField();
        InitPasswordField();
        InitPasswordCheckField();
    }
    #endregion

    #region Private Methods
    private void InitButtons()
    {
        nextStageBtn           .Init(Localization.NEXT);
        previousStageBtn       .Init(Localization.BACK);
        completeRegistrationBtn.Init(Localization.COMPLETE_REGISTRATION);
    }
    
    private void InitNicknameField()
    {
        nickname.Init(Localization.ENTER_NICKNAME, Localization.INVALID_NICKNAME, CheckValueOnCorrect, SetInteractableButtonNext);
        
        bool CheckValueOnCorrect(string value)
        {
            return value.Length >= 2 && value.AllCharactersIsLatinLetters();
        }
    }

    private void InitMailField()
    {
        mail.Init(Localization.ENTER_EMAIL, Localization.INVALID_EMAIL, CheckValueOnCorrect, SetInteractableButtonNext);
        
        bool CheckValueOnCorrect(string value)
        {
            return value.Contains("@") && value.EndsWith(".com");
        }
    }
    
    private void InitPasswordField()
    {
        password.Init(Localization.ENTER_PASSWORD, Localization.INVALID_PASSWORD, CheckValueOnCorrect, SetInteractableBtnAndRecalculatePasswordCheck);
        
        bool CheckValueOnCorrect(string value)
        {
            return value.Length >= 8 && value.AllCharactersIsLatinLettersOrDigits() && value.Any(char.IsUpper) && value.Any(char.IsDigit);
        }
    }
    
    private void InitPasswordCheckField()
    {
        passwordCheck.Init(Localization.REPEAT_PASSWORD, Localization.INVALID_SECON_PASSWORD, CheckValueOnCorrect, SetInteractableBtnAndRecalculatePasswordCheck);
        
        bool CheckValueOnCorrect(string value)
        {
            return value == password.currentText;
        }
    }

    private void SetInteractableButtonNext()
    {
        nextStageBtn.SetInteractable(nickname.valueIsCorrect && mail.valueIsCorrect);
    }
    
    private void SetInteractableButtonCompleteRegistration()
    {
        completeRegistrationBtn.SetInteractable(password.valueIsCorrect && passwordCheck.valueIsCorrect);
    }

    private void SetInteractableBtnAndRecalculatePasswordCheck() // fixed a bug when changing the first password after entering both passwords
    {
        passwordCheck.Recalculate();
        SetInteractableButtonCompleteRegistration();
    }
    #endregion

    #region Buttons Methods
    public void NextStageHandler()
    {
        bool correct = nickname.valueIsCorrect && mail.valueIsCorrect;

        if (!correct)
            return;

        nicknameAndMailCG.Show(false);
        passwordsCG.Show();
    }
    
    public void PreviousStageHandler()
    {
        nicknameAndMailCG.Show();
        passwordsCG.Show(false);
    }

    public void CompleteRegistrationHandler()
    {
        bool correct = password.valueIsCorrect && passwordCheck.valueIsCorrect;

        if (!correct)
            return;

        passwordsCG.Show(false);

        StartCoroutine(UpdateDelayTextAndLoadScene());
        
        IEnumerator UpdateDelayTextAndLoadScene()
        {
            float seconds = Time.deltaTime;
            
            while (seconds < 1.3f)
            {
                seconds += Time.deltaTime;
                delayText.text = string.Format(Localization.WAIT, Math.Round(seconds, 2));
                
                yield return null;
            }

            SceneManager.LoadScene(1);
        }
    }
    #endregion
}
