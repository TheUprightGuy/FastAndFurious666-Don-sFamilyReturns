using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public Image pressAnyKey;
    public Animator options;

    public List<GameObject> pointers;
    int index = 0;
    bool started;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlashPressAnyKey());
        pointers[0].SetActive(false);
        pointers[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !started)
        {
            StopAllCoroutines();
            started = true;
            pressAnyKey.enabled = false;
            options.SetTrigger("ShowOptions");
            Invoke("ShowPointer", 1.0f);
        }

        if (pointers[0].activeSelf || pointers[1].activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                pointers[index].SetActive(false);
                index++;
                if (index >= pointers.Count)
                {
                    index = 0;
                }
                pointers[index].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                Use(index);
            }
        }
    }

    void ShowPointer()
    {
        pointers[0].SetActive(true);
    }

    public void Use(int _index)
    {
        switch (_index)
        {
            // Start
            case 0:
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                break;
            }
            // Quit
            case 1:
            {
                Application.Quit();
                break;
            }
        }     
    }

    IEnumerator FlashPressAnyKey()
    {
        while (true)
        {
            pressAnyKey.enabled = !pressAnyKey.enabled;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
