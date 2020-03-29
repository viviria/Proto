using UnityEngine;
using Common;

namespace Roguelike {
  public class Player : MonoBehaviour
  {
    private const float MOVE_TIME = 0.25f;
    private MoveController moveController_ = null;
    private PlayerStatus playerStatus_ = null;
    private Animator animator_ = null;

    // Start is called before the first frame update
    void Start()
    {
      animator_ = GetComponent<Animator>();
      playerStatus_ = new PlayerStatus();
    }

    // Update is called once per frame
    void Update()
    {
      moveController_?.Update();
    }

    public void SetUIEnabled(int enabled)
    {
      bool bEnabled = enabled != 0;
      GameObject uiCanvas = GameObject.Find("Canvas");
      uiCanvas.GetComponent<CanvasController>().setEnabled(bEnabled);
    }
    
    public void moveButtonDown(int moveDir)
    { 
      TouchUtil.TouchInfo touchInfo = TouchUtil.getTouch();
      if (!TouchUtil.isTouch(touchInfo)) {
        MoveController.MoveTriger(gameObject, (MoveController.MoveDir)moveDir, true);
        /*moveController_ = new MoveController(MOVE_TIME,
        () => {
          uiCanvas.GetComponent<CanvasController>().setEnabled(false);
        },
        () => {
          uiCanvas.GetComponent<CanvasController>().setEnabled(true);
        });*/

        //moveController_ = new MoveController(MOVE_TIME);
        //moveController_.moveAction(gameObject, (MoveController.MoveDir)moveDir);
      }
    }

    public void TransitionIdle(int moveDir)
    {
      MoveController.MoveTriger(gameObject, (MoveController.MoveDir)moveDir, false);
    }

    public void attackButtonDown()
    {
      animator_ = GetComponent<Animator>();
      if (animator_.GetCurrentAnimatorStateInfo(0).IsName("attack"))
      {
        return;
      }
      
      animator_.SetTrigger("attackTrigger");
      GameManager.instance().enamyDamage(playerStatus_.power_);
    }
  }
}
