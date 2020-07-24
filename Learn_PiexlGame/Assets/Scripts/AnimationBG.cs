using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBG : MonoBehaviour
{
    private Material material;
    private Vector2 moveMent;
    [SerializeField] private Vector2 Speed;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        moveMent += Speed * Time.deltaTime;
        material.mainTextureOffset = moveMent;
    }
}
