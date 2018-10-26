using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceTeleport : MonoBehaviour
{
    public UnityEngine.UI.Image blindfold;
    public float fadeTime, delayTime;
    private Vector3 originalPos;
    private Quaternion originalRot;
    public GameObject objectRef;


    // ---------- ---------- ---------- ---------- ---------- 
    private void Start()
    {
        if (objectRef == null)
        { objectRef = gameObject; }

        originalPos = objectRef.transform.localPosition;
        originalRot = objectRef.transform.localRotation;
    }


    public void SetoriginalPos()
    {

    }

    /// <summary>
    ///     Used to call via the messenger
    /// </summary>
    /// <param name="pos"></param>
    public void StartTeleport(Transform pos)
    {
        StartCoroutine(TeleportPlayerToTransform(pos));
    }

    /// <summary>
    ///     Teleport the player to the set location with a delay and a blindfold
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    // ---------- ---------- ---------- ---------- ---------- 
    public IEnumerator TeleportPlayerToTransform(Transform pos)
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(FadeInOut());
        yield return new WaitForSeconds(.1f);
        pos.gameObject.SetActive(true);
        objectRef.transform.position = pos.position;
        objectRef.transform.rotation = pos.rotation;
    }

    /// <summary>
    ///     Used to call via the messenger
    /// </summary>
    public void StartLoadScene(string scene)
    {
        StartCoroutine(LoadScene(scene));
    }

    /// <summary>
    ///     Load the scene at the end of the game
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadScene(string scene)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(scene);
    }


    // ---------- ---------- ---------- ---------- ---------- 
    public void ResetToOriginalPosIn(float time)
    {
        StartCoroutine(TimeToTeleportBack(time));
    }


    // ---------- ---------- ---------- ---------- ---------- 
    public void SetOriginalValuesToCurrent()
    {
        originalPos = objectRef.transform.localPosition;
        originalRot = objectRef.transform.localRotation;
    }

    // ---------- ---------- ---------- ---------- ---------- 
    public void SetOriginalValuesTo(Transform newValues)
    {
        originalPos = newValues.localPosition;
        originalRot = newValues.localRotation;
    }



    // ---------- ---------- ---------- ---------- ---------- 
    public IEnumerator FadeInOut()
    {

        if (blindfold != null)
        {
            Color c = blindfold.color;
            print(blindfold.color);
            c.a = 1f;
            blindfold.color = c;

            while (blindfold.color.a > 0.01f)
            {
                c.a -= (Time.deltaTime / fadeTime);
                blindfold.color = c;
                yield return null;
            }

            c.a = 0f;
            blindfold.color = c;
        }
    }


    // ---------- ---------- ---------- ---------- ---------- 
    public IEnumerator TimeToTeleportBack(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(FadeInOut());
        objectRef.transform.localPosition = originalPos;
        objectRef.transform.localRotation = originalRot;
    }
}