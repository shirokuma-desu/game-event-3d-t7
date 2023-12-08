using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAsync : GenericSingleton<LoadAsync>
{
    [SerializeField]
    private Slider m_loadingBar;

    [SerializeField]
    private Animator m_animator;

    public void LoadScene(string _sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(_sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string _sceneName)
    {
        m_animator.SetBool("Start", true);
        yield return new WaitForEndOfFrame();
        m_animator.SetBool("Start", false);

        yield return new WaitForSeconds(1.5f);

        AsyncOperation _loadOperation = SceneManager.LoadSceneAsync(_sceneName);

        while (!_loadOperation.isDone)
        {
            float _progress = Mathf.Clamp01(_loadOperation.progress / .9f);
            m_loadingBar.value = _progress;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(.5f);

        m_animator.SetBool("End", true);
        yield return new WaitForEndOfFrame();
        m_animator.SetBool("End", false);

        yield return null;
    }
}
