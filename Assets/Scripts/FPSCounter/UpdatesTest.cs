using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[ExecuteInEditMode]
public class UpdatesTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _updateText;
    [SerializeField] private TextMeshProUGUI _fixedText;

    private float updateCount = 0;
    private float fixedUpdateCount = 0;
    private float updateUpdateCountPerSecond;
    private float updateFixedUpdateCountPerSecond;

    private readonly string _update = "U: ";
    private readonly string _fixed = "F: ";

    void Awake()
    {
        // Uncommenting this will cause framerate to drop to 10 frames per second.
        // This will mean that FixedUpdate is called more often than Update.
        //Application.targetFrameRate = 10;
        StartCoroutine(Loop());
    }

    // Increase the number of calls to Update.
    void Update()
    {
        updateCount += 1;
    }

    // Increase the number of calls to FixedUpdate.
    void FixedUpdate()
    {
        fixedUpdateCount += 1;
    }

    // Show the number of calls to both messages.
    //void OnGUI()
    //{
    //    GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
    //    fontSize.fontSize = 50;
    //    GUI.Label(new Rect(10, 750, 300, 200), "Update: " + updateUpdateCountPerSecond.ToString(), fontSize);
    //    GUI.Label(new Rect(10, 850, 400, 200), "FixedUpdate: " + updateFixedUpdateCountPerSecond.ToString(), fontSize);
    //}

    // Update both CountsPerSecond values every second.
    IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            updateUpdateCountPerSecond = updateCount;
            updateFixedUpdateCountPerSecond = fixedUpdateCount;

            _updateText.text = _update + updateUpdateCountPerSecond;
            _fixedText.text = _fixed + updateFixedUpdateCountPerSecond;

            updateCount = 0;
            fixedUpdateCount = 0;
        }
    }
}
