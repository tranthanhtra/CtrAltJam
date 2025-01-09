using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyComponent : MonoBehaviour
{
    [SerializeField] private TextMesh text;
    private KeyCode key;

    public KeyCode Key
    {
        get => key;
        set
        {
            key = value;
            text.text = key.ToString();
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(key))
    //     {
    //         transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
    //     }
    //
    //     if (Input.GetKeyUp(key))
    //     {
    //         transform.localPosition = new Vector3(transform.localPosition.x, 1.5f, transform.localPosition.z);
    //     }
    // }
}