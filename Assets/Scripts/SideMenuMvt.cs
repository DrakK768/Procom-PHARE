using UnityEngine;

public class SideMenuMvt : MonoBehaviour
// Description : Script to make the side menu (menuPannel) move between its hidden position to its active position
{

    public GameObject menuHiddenPos;
    public GameObject menuActivePos;
    public GameObject menuPannel;

    public float speed; //pannel's movement speed

    private bool mvt_active;
    private bool mvt_hidden;
    private float tempPos;

    void Start()
    {
        mvt_active = false;
        mvt_hidden = false;
        menuPannel.transform.position = menuHiddenPos.transform.position;
        tempPos = -999999999999.99f;
    }

     void Update()
     //Using Lerp to move the pannel and have a smooth movement until it doesn't move anymore
    {
        if (mvt_active) {
            menuPannel.transform.position = Vector3.Lerp(menuPannel.transform.position, menuActivePos.transform.position,speed = Time.deltaTime);
            if (menuPannel.transform.localPosition.x == tempPos) {
                mvt_active = false;
                menuPannel.transform.position = menuActivePos.transform.position;
                tempPos = -999999999999.99f;
            }

            if (mvt_active){
                tempPos = menuPannel.transform.localPosition.x;
            }
        }

        if (mvt_hidden) {
            menuPannel.transform.position = Vector3.Lerp(menuPannel.transform.position, menuHiddenPos.transform.position,speed = Time.deltaTime);
            
            if (menuPannel.transform.localPosition.x == tempPos) {
                mvt_hidden = false;
                menuPannel.transform.position = menuHiddenPos.transform.position;
                tempPos = -999999999999.99f;
            }

            if (mvt_hidden){
                tempPos = menuPannel.transform.localPosition.x;
            }
        }
    }

    public void MovePanelActive() {
        mvt_active = true;
        mvt_hidden = false;
    }
        public void MovePanelHidden() {
        mvt_hidden = true;
        mvt_active = false;
    }

    public void EnableDisable(GameObject target) {
        target.SetActive(!target.activeInHierarchy);
    }
}
