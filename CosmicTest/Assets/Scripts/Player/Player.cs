using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public SimpleWalkerController controllerMove;
    public CharacterInput input;
    public CinemachineControllerCamera cinmachineCamera;
    public bool IsActivate;
    public GameObject panelButtons;
    public InventoryObject inventory;
    public string playername;

    void Start()
    {
        input = GetComponent<CharacterInput>();
        controllerMove = GetComponent<SimpleWalkerController>();
        cinmachineCamera = GetComponent<CinemachineControllerCamera>();
        StartCoroutine(LoadDataPlayer());
    }

    public void SavedDataPlayer()
    {
        inventory.Save();
    }

    public IEnumerator LoadDataPlayer()
    {
        yield return new WaitForSeconds(1f);

        if (!ManagerBaseData.Instance.Load(inventory.savePath, inventory))
        {
            SavedDataPlayer();
        }
    }

    public bool State()
    {
        return IsActivate = !IsActivate;
    }

    void FixedUpdate()
    {

        controllerMove.FixedUpdateController();

        if (IsActivate)
        {
            input.FixedUpdateInput();
            cinmachineCamera.FixedUpdateCameraController();
            panelButtons.SetActive(true);
        }
        else
        {
            controllerMove.mover.SetVelocity(Vector3.zero);
            input.Desactivate();
            panelButtons.SetActive(false); 
        }
    }

    
}
