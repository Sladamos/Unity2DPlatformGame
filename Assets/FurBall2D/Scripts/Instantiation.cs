using UnityEngine;
using System.Collections;

public class Instantiation : MonoBehaviour
{

    public GameObject _gameObject = null;
    // Use this for initialization
    void Start()
    {

        _gameObject = Instantiate(Resources.Load("Prefabs/Cloud")) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
