using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowing : Singleton<CameraFollowing>
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Material blurMaterial;
    [SerializeField] private Camera mainCamera;
    private Material originalMaterial;
    private Transform currentObtacle;
    private bool isBlockedObtacle;

    private float speed;
    private Transform TF;
    void Start()
    {
        speed = 5;   
        TF = transform;
    }

    void Update()
    {
        this.CheckObtacle();
        if (GameManager.Instance.currentState != GameState.ShopSkinMenu)
        {
            TF.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 10 + playerCtrl.rangeAttack * 1.5f, -10 - playerCtrl.rangeAttack * 1.5f), Time.deltaTime * speed);
            TF.rotation = Quaternion.Euler(45, 0, 0);
            return;
        }
        TF.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, -1.551f, -10), Time.deltaTime * speed);
        TF.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void CheckObtacle()
    {

        Vector3 playerPos = Camera.main.WorldToScreenPoint(player.position);
        Ray ray = Camera.main.ScreenPointToRay(playerPos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,obstacleLayer))
        {
            if(!isBlockedObtacle)
            {
                isBlockedObtacle = true;
                currentObtacle = hit.transform;
                originalMaterial = currentObtacle.GetComponent<MeshRenderer>().material;
                currentObtacle.GetComponent<MeshRenderer>().material = blurMaterial;
            }
        }else
        {
            if (originalMaterial != null)
            {
                currentObtacle.GetComponent<MeshRenderer>().material = originalMaterial;
                currentObtacle = null;
                originalMaterial = null;
            }
            isBlockedObtacle = false;
        }
        

    }
}
