using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int dayNum;
    public int demandNum;
    public float totalScore;
    public float likeability;


    private Image fadePanel;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        totalScore = 0.0f;
        demandNum = 0;
        fadePanel = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    public void CheckDemandNum()
    {
        if(demandNum == 3)
        {
            LoadScene("Day2");
        }
        if(demandNum == 6) 
        {
            LoadScene("Day3");
        }
        if (demandNum == 9)
        {
            LoadScene("Ending");
        }
    }

    public void LoadScene(string sceneName) 
    {
        StartCoroutine(FadeOutCoroutine());
        SceneManager.LoadScene(sceneName);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float alpha = 0;
        while(alpha < 1.0f)
        {
            alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadePanel.color = new Color(0,0,0, alpha);
        }
        yield return new WaitForSeconds(1);
    }

    //점수 더하기
    public void AddScore(int count)
    {
        if(count == 1)
        {
            likeability = 3.0f;
            totalScore += likeability;
        }
        else if(count == 2)
        {
            likeability = 2.0f;
            totalScore += likeability;
        }
        else if(count == 3)
        {
            likeability = 1.0f;
            totalScore += likeability;
        }
    }

}
