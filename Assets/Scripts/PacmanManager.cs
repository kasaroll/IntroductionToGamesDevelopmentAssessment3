using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().isPlaying == true && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().loop == false)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<AudioSource>().Stop();
        }
        else
        {
            gameObject.GetComponent<Animator>().enabled = true;

            if (!gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }

            if (gameObject.transform.position == new Vector3(-12.5f, 13.0f, 0.0f))
            {
                gameObject.transform.Rotate(0.0f, 0.0f, 45.0f);
                StartCoroutine(LerpPosition(new Vector3(-7.5f, 13.0f, 0.0f), 1.0f));
            }
            else if (gameObject.transform.position == new Vector3(-7.5f, 13.0f, 0.0f))
            {
                gameObject.transform.Rotate(0.0f, 0.0f, 45.0f);
                StartCoroutine(LerpPosition(new Vector3(-7.5f, 9.0f, 0.0f), 0.66f));
            }
            else if (gameObject.transform.position == new Vector3(-7.5f, 9.0f, 0.0f))
            {
                gameObject.transform.Rotate(0.0f, 0.0f, 45.0f);
                StartCoroutine(LerpPosition(new Vector3(-12.5f, 9.0f, 0.0f), 1.0f));
            }
            else if (gameObject.transform.position == new Vector3(-12.5f, 9.0f, 0.0f))
            {
                gameObject.transform.Rotate(0.0f, 0.0f, 45.0f);
                StartCoroutine(LerpPosition(new Vector3(-12.5f, 13.0f, 0.0f), 0.66f));
            }
        }
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = gameObject.transform.position;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}