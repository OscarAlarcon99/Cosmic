using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caldero : MonoBehaviour
{

    public void FinishGame()
    {
        Player.Instance.State();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.PLAYER_TAG))
        {
            StartCoroutine(ManagerGameLevelBasic.Instance.IniciarPartida()); 
        }
    }
}
