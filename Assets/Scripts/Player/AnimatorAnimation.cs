using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class AnimatorAnimation : MonoBehaviour
{

    [SerializeField] PlayerController controller;

    [SerializeField] Animator _Controller;

    //(parametros) => expresión

    private void Update()
    {
        _Controller.SetBool("WalkForward", false);
        _Controller.SetBool("WalkBack", false);
        _Controller.SetBool("WalkLeft", false);
        _Controller.SetBool("WalkRight", false);


        // 2️⃣ Activar solo la animación que corresponde según el input
        if (controller.VerticalMove > 0)
            _Controller.SetBool("WalkForward", true);
        else if (controller.VerticalMove < 0)
            _Controller.SetBool("WalkBack", true);
        else if (controller.HorizontalMove > 0)
            _Controller.SetBool("WalkLeft", true);
        else if (controller.HorizontalMove < 0)
            _Controller.SetBool("WalkRight", true);
    }
}
    
  
