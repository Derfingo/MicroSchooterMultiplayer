using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public float DeltaTime { get; private set; }
    public int AverageFPS { get; private set; }
    public int HighestPFS { get; private set; }
    public int LowersFPS { get; private set; }

    private int _frameRange = 120;
    private int[] _fpsBuffer;
    private int _fpsBufferIndex;

    private void Update()
    {
        if (_fpsBuffer == null || _frameRange != _fpsBuffer.Length)
        {
            InitializeBuffer();
        }

        UpdateBuffer();
        CalculateFps();
    }

    private void InitializeBuffer()
    {
        if (_frameRange <= 0)
        {
            _frameRange = 1;
        }

        _fpsBuffer = new int[_frameRange];
        _fpsBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
        _fpsBuffer[_fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if (_fpsBufferIndex >= _frameRange)
        {
            _fpsBufferIndex = 0;
        }
    }

    private void CalculateFps()
    {
        int sum = 0;
        int lowest = int.MaxValue;
        int highest = 0;
        for (int i = 0; i < _frameRange; i++)
        {
            int fps = _fpsBuffer[i];
            sum += fps;
            if (fps > highest)
            {
                highest = fps;
            }

            if (fps < lowest)
            {
                lowest = fps;
            }
        }

        DeltaTime += (Time.unscaledDeltaTime - DeltaTime) * 0.1f;

        HighestPFS = highest;
        LowersFPS = lowest;
        AverageFPS = sum / _frameRange;
    }
}
