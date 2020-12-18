using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGameLevelBasic : Singleton<ManagerGameLevelBasic>
{
    public GameObject potion;


    public IEnumerator IniciarPartida()
    {
        Player.Instance.State();
        yield return new WaitUntil(() => !Player.Instance.IsActivate);
        ManagerQuestGame.Instance.panelSelect.SetActive(true);
        yield return new WaitUntil(() => ManagerQuestGame.Instance.startGame);
        StartCoroutine(ManagerQuestGame.Instance.GetQuestion());
        yield return new WaitUntil(() => !ManagerQuestGame.Instance.startGame);
        Player.Instance.State();
    }
}
