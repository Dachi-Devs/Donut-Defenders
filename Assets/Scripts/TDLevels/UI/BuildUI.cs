using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildUI : MonoBehaviour
{
    public GameObject canvas;

    public void CantBuild()
    {
        Debug.Log("Cant build");
        canvas.SetActive(true);
        StartCoroutine(CantBuildTimer());
    }

    private IEnumerator CantBuildTimer()
    {
        yield return new WaitForSeconds(2f);
        canvas.SetActive(false);
    }
}
