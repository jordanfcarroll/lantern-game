using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class GameTest { 
 
    public static GameTest current;
    public Character knight;
    public Character rogue;
    public Character wizard;

    private bool didTheThing = false;
 
    public void Game () {
        knight = new Character();
        rogue = new Character();
        wizard = new Character();
    }



         
}