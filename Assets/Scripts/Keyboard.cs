using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private KeyComponent keyPrefab;
    [SerializeField] private float spaceKey = 2;

    private List<KeyComponent> keyList = new();
    private float length;
    private float width;
    private float height;

    private void Start()
    {
        GenerateKeyboard();
    }

    private void Update()
    {
        foreach (var key in keyList)
        {
            if (Input.GetKey(key.Key))
            {
                key.transform.localPosition = new Vector3(key.transform.localPosition.x, 0.5f,
                    key.transform.localPosition.z);
            }
            else
            {
                key.transform.localPosition = new Vector3(key.transform.localPosition.x, 1.5f,
                    key.transform.localPosition.z);
            }
        }
    }

    private void GenerateKeyboard()
    {
        try
        {
            using StreamReader sr = new StreamReader("Assets/KeyboardPosition.txt");
            string line;
            Vector3 startLine = Vector3.zero;
            height = 1;
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
                        keyObj.Key = (KeyCode)keyCode;
                        keyList.Add(keyObj);
                    }
                }

                if (keys.Length * (1 * spaceKey) > length)
                {
                    length = keys.Length * (1 * spaceKey);
                }

                width += spaceKey;
                startLine.z -= spaceKey;
                startLine.x += spaceKey / 2;
            }

            length += 2;
            GenerateBoundary();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void GenerateBoundary()
    {
        //top
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localPosition = new Vector3(transform.localPosition.x + length / 2 - spaceKey, .5f,
            transform.localPosition.z + spaceKey);
        cube.transform.localScale = new Vector3(length, 1, 1);
        cube.name = "Top";
        cube.tag = Utils.wallTag;
        // bottom
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localPosition = new Vector3(transform.localPosition.x + length / 2 - spaceKey, .5f,
            transform.localPosition.z - width);
        cube.transform.localScale = new Vector3(length, 1, 1);
        cube.name = "Bottom";
        cube.tag = Utils.wallTag;
        // left
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localPosition = new Vector3(transform.localPosition.x - spaceKey, .5f,
            transform.localPosition.z - width / 2 + 1);
        cube.transform.localScale = new Vector3(1, 1, width + spaceKey);
        cube.name = "Left";
        cube.tag = Utils.wallTag;
        // right
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localPosition = new Vector3(transform.localPosition.x + length - spaceKey, .5f,
            transform.localPosition.z - width / 2 + 1);
        cube.transform.localScale = new Vector3(1, 1, width + spaceKey);
        cube.name = "Right";
        cube.tag = Utils.wallTag;
    }
}