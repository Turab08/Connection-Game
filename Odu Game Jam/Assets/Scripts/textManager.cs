using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class textManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textMeshPro;
    void Start()
    {
        Sequence introSequence = DOTween.Sequence();

        introSequence.Append(textMeshPro[0].DOFade(1f, 1.5f))
                     .AppendInterval(1f)
                     .Append(textMeshPro[1].DOFade(1f, 1.5f))
                     .AppendInterval(1f)
                     .Append(textMeshPro[2].DOFade(1f, 1.5f))
                     .AppendInterval(3f);        

    }

}