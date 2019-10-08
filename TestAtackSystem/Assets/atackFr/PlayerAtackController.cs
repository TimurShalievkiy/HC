using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtackController : MonoBehaviour
{
    bool isDraging = false;
    bool isMelle = true;
    public static bool isAtacking = true;

    [SerializeField] Animator anim;

    [SerializeField] GameObject sword;
    [SerializeField] GameObject crossbow;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform directionForArrow;

    GameObject g;
    private void Start()
    {
        arrow = Resources.Load<GameObject>("bolt");
    }
    public void CheckForDraging(bool flag)
    {
        isDraging = flag;
    }
    public void Atack()
    {
        if (!isDraging)
        {
            anim.SetBool("Atack", true);
            isAtacking = true;
            Debug.Log("Atack");
            if (!isMelle)
            {
    
                g = Instantiate(arrow);
                g.transform.position = directionForArrow.position;
                g.transform.rotation = directionForArrow.rotation;
                Destroy(g,1);

            }
            StartCoroutine(AtackSpeedDelay());
        }
        
    }
    private void Update()
    {
        
        anim.SetBool("Atack", false);
        
    }
    public void ChangeWeapon()
    {
        if (!isMelle)
        {
            crossbow.SetActive(false);
            sword.SetActive(true);
            isMelle = true;
            anim.SetBool("isMelee", true);
        }
        else
        {
            sword.SetActive(false);
            crossbow.SetActive(true);
            isMelle = false;
            anim.SetBool("isMelee", false);
        }
    }

    IEnumerator AtackSpeedDelay()
    {
 
            yield return new WaitForSeconds(0.5f);
            isAtacking = false;


        
    }
}
