using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_ : MonoBehaviour
{

    public static Game_ instance;
    public LevelGenerator_ levelGenerator;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
