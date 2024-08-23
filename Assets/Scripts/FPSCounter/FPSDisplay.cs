using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _averageLabel;
    [SerializeField] private TextMeshProUGUI _highestLabel;
    [SerializeField] private TextMeshProUGUI _lowestLabel;
    [SerializeField] private TextMeshProUGUI _delayCPULabel;

    private FPSCounter _fpsCounter;

    private readonly string[] _stringsFrom00To120 = {
        "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
        "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
        "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
        "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
        "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
        "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
        "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
        "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
        "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
        "90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
        "100", "101", "101", "102", "103", "104", "105", "106", "107",
        "108", "109", "110", "111", "112", "113", "114", "115", "116",
        "117", "118", "119", "120", "121"
    };

    private void Awake()
    {
        _fpsCounter = GetComponent<FPSCounter>();
    }

    private void Update()
    {
        Display(_averageLabel, _fpsCounter.AverageFPS);
        Display(_highestLabel, _fpsCounter.HighestPFS);
        Display(_lowestLabel, _fpsCounter.LowersFPS);
        DisplayDelay(_delayCPULabel);
    }

    private void Display(TextMeshProUGUI label, int fps)
    {
        label.text = _stringsFrom00To120[Mathf.Clamp(fps, 0, 120)];
    }

    private void DisplayDelay(TextMeshProUGUI label)
    {
        var msec = _fpsCounter.DeltaTime * 1000.0f;
        var text = $"{msec:0.0} ms";
        label.text = text;
    }
}
