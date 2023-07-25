using System.Linq;
using UnityEngine;

public static class Helper
{
    #region Extensions Methods
    public static bool AllCharactersIsLatinLetters(this string @string)
    {
        return @string.All(x => (x >= 'a' && x <= 'z') || (x >= 'A' && x <= 'Z'));
    }
    
    public static bool AllCharactersIsLatinLettersOrDigits(this string @string)
    {
        return @string.All(x => (x >= 'a' && x <= 'z') || (x >= 'A' && x <= 'Z') || (x >= '0' && x <= '9'));
    }

    public static void Show(this CanvasGroup canvasGroup, bool show = true)
    {
        canvasGroup.alpha = show ? 1.0f : 0.0f;
        canvasGroup.blocksRaycasts = show;
    }
    #endregion
}
