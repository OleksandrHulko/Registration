using System;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPro : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField]
    private InputField inputField = null;
    [SerializeField]
    private Text titleText = null;
    [SerializeField]
    private Text warningText = null;
    [SerializeField]
    private AudioSource audioSource = null;
    #endregion

    #region Public Fields
    [HideInInspector]
    public string currentText = string.Empty;
    [HideInInspector]
    public bool valueIsCorrect = false;
    #endregion
    
    #region Private Fields
    private string _warning = string.Empty;
    private Func<string, bool> _checkValueOnCorrect = null;
    private Action _onValueChanged = null;
    #endregion

    
    #region Public Methods
    public void Init(string title, string warning, Func<string,bool> checkValueOnCorrect, Action onValueChanged )
    {
        _warning = warning;
        _checkValueOnCorrect = checkValueOnCorrect;
        _onValueChanged = onValueChanged;
        
        inputField.onValueChanged.AddListener(OnValueChangedHandler);
        titleText.text = title;
    }

    public void Recalculate()
    {
        valueIsCorrect = _checkValueOnCorrect.Invoke(currentText);
        ShowWarningText(!valueIsCorrect);
    }
    #endregion
    
    #region Private Methods
    private void OnValueChangedHandler(string value)
    {
        currentText = value;
        valueIsCorrect = _checkValueOnCorrect.Invoke(currentText);
        ShowWarningText(!valueIsCorrect);
        _onValueChanged.Invoke();
        audioSource.Play();
    }

    private void ShowWarningText(bool show = true)
    {
        warningText.text = show ? _warning : string.Empty; // maybe use Canvas Group
    }
    #endregion
}
