using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private KeyComponent keyPrefab;
    [SerializeField] private float spaceKey = 2;

    private Dictionary<KeyCode, KeyComponent> keyList;
    private void Start()
    {
        GenerateKeyboard();
    }

    private void GenerateKeyboard()
    {
        try
        {
            using StreamReader sr = new StreamReader("Assets/KeyboardPosition.txt");
            string line;
            Vector3 startLine = Vector3.zero;
            while ((line = sr.ReadLine()) != null)
            {
                string[] keys = line.Split(' ');
                for (var index = 0; index < keys.Length; index++)
                {
                    var key = keys[index];
                    Enum.TryParse(typeof(KeyCode), key, out object keyCode);
                    if (keyCode != null)
                    {
                        var keyObj = Instantiate(keyPrefab, transform);
                        keyObj.transform.localPosition =
                            new Vector3(startLine.x + spaceKey * index, 1.5f, startLine.z);
                        keyObj.Key = (KeyCode) keyCode;
                        keyList.Add(keyObj.Key, keyObj);
                    }
                }

                startLine.z -= spaceKey;
                startLine.x += spaceKey / 2;
            }
            GenerateBoundary();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void GenerateBoundary()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(5, 5, 5);
    }
}