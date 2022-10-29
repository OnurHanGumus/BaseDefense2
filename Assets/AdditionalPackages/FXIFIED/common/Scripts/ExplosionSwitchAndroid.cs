using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSwitchAndroid : MonoBehaviour
{
    
    public CameraShake cameraShake;
    public int selectedExplosion = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectExplosion ();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))

            StartCoroutine(cameraShake.Shake(.1f, .2f));
            
        

        int previousSelectedExplosion = selectedExplosion;

        if (Input.GetMouseButtonDown(0))

        {
            if (selectedExplosion >= transform.childCount - 1)
                selectedExplosion = 0;
            else
             selectedExplosion++;

           

        }

 



        if (previousSelectedExplosion != selectedExplosion)
        {
            SelectExplosion();
        }
    }
    void SelectExplosion ()
    {
        int i = 0;
        foreach (Transform explosion in transform)
        {
            if (i == selectedExplosion)
                explosion.gameObject.SetActive(true);

            else
               explosion.gameObject.SetActive(false);
            i++;
        }
    }
}
