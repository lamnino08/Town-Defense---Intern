namespace Trung.Scene
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlaneScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseDown()
        {
            MainUI_T.instance.edit_Status(false);
            LogicScript.instance._currentBuild = null;
        }
    }

}