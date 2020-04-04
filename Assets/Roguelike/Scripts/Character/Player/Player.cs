using UnityEngine;
using Common;

namespace Roguelike {
  public class Player : Character
  {
    private PlayerStatus playerStatus_ = null;

    // Start is called before the first frame update
    protected override void Start()
    {
      base.Start();
      playerStatus_ = new PlayerStatus();
    }

    // Update is called once per frame
    protected override void Update()
    {
      base.Update();
    }

    public void EnableUIButton()
    {
      GameObject uiCanvas = GameObject.Find("Canvas");
      uiCanvas.GetComponent<CanvasController>().setEnabled(true);
    }
    
    public void DisableUIButton()
    {
      GameObject uiCanvas = GameObject.Find("Canvas");
      uiCanvas.GetComponent<CanvasController>().setEnabled(false);
    }

    public void moveButtonDown(int moveMotionType)
    { 
      TouchUtil.TouchInfo touchInfo = TouchUtil.getTouch();
      if (!TouchUtil.isTouch(touchInfo)) {
        SetMotionType(moveMotionType);
      }
    }

    public void attackButtonDown()
    {
      /*
      animator_ = GetComponent<Animator>();
      if (animator_.GetCurrentAnimatorStateInfo(0).IsName("attack"))
      {
        return;
      }
      
      animator_.SetTrigger("attackTrigger");
      GameManager.instance().enamyDamage(playerStatus_.power_);
      */
    }
  }
}
