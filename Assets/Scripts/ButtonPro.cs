using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPro : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler , IPointerClickHandler
{
    #region Serialize Fields
    [SerializeField]
    private Text titleText = null;
    [SerializeField]
    private Button button = null;
    [SerializeField]
    
    private AudioSource audioSource = null;
    [SerializeField]
    
    private AudioClip audioPointing = null;
    [SerializeField]
    private AudioClip audioClick = null;
    [SerializeField]
    private AudioClip audioClickNotInteractable = null;
    #endregion

    #region Private Fields
    private static Vector3 _newScale = new Vector3(1.15f, 1.15f, 1.15f);
    #endregion


    #region Public Methods
    public void Init(string title)
    {
        titleText.text = title;
    }

    public void SetInteractable( bool isInteractable = true)
    {
        button.interactable = isInteractable;
    }
    #endregion

    #region Private Methods
    private void Play(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    #endregion

    #region Implementation of interfaces
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _newScale;
        Play(audioPointing);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        Play(button.interactable ? audioClick : audioClickNotInteractable);
    }
    #endregion
}
