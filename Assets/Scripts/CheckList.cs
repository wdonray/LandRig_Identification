using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckList : MonoBehaviour
{
    public GameObject Prefab;
    public Text CountdownText;
    public List<GameObject> Zones = new List<GameObject>();
    private readonly List<GameObject> _childrenObjects = new List<GameObject>();
    private readonly List<GameObject> _cLObjects = new List<GameObject>();
    private float _countDown;
    private int _index;

    void Awake()
    {
        _index = 0;
        CreateToggles();
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < _childrenObjects.Count; i++)
        {
            var childCollider = _childrenObjects[i].GetComponentInChildren<Collider>();
            _cLObjects[i].GetComponent<Toggle>().isOn = !childCollider.enabled;
        }
    }

    /// <summary>
    ///     Called on each zone for updating the check list
    /// </summary>
    public void NextCheckList()
    {
        StartCoroutine(AllFoundDisable(FindObjectOfType<ForceTeleport>().delayTime));
    }

    /// <summary>
    ///     Updates each list and creates the objects
    /// </summary>
    private void CreateToggles()
    {
        CountdownText.gameObject.SetActive(false);

        foreach (var child in Zones[_index].GetComponentsInChildren<Transform>())
        {
            if (child == Zones[_index].transform) continue;
            if (child.GetComponent<InteractableObject>()) continue;
            _childrenObjects.Add(child.gameObject);
        }

        foreach (var o in _childrenObjects)
        {
            var prefab = Instantiate(Prefab, gameObject.transform);
            prefab.GetComponentInChildren<Text>().text = o.name;
            prefab.GetComponentInChildren<Toggle>().isOn = false;
            _cLObjects.Add(prefab);
        }

        CountdownText.transform.SetAsLastSibling();
    }

    /// <summary>
    ///     Reset each list and clear the clipboard
    /// </summary>
    private void ResetList()
    {
        _childrenObjects.Clear();
        foreach (var cLObject in _cLObjects)
        {
            Destroy(cLObject);
        }
        _cLObjects.Clear();
        _index++;
    }

    /// <summary>
    ///      Countdown until updating the clipboard
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator AllFoundDisable(float delay)
    {
        _countDown = delay;
        ResetList();
        CountdownText.gameObject.SetActive(true);
        while (_countDown > 0)
        {
            yield return null;
            _countDown -= Time.deltaTime;
            CountdownText.text = "Great Job, Next Area in: " + _countDown.ToString("0");
        }
        CreateToggles();
    }
}
