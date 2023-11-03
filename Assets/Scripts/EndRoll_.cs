using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoll_ : MonoBehaviour
{

    public Player_ player;

    public void Awake()
    {
        player = GetComponentInParent<Player_>();
    }
   
    public void EndRoll()
    {
        player.Height = Height.Ground;
    }
}
