using System;
using UnityEngine;
using Common;

namespace Roguelike {

  public class MoveController
  {
    public enum MoveDir {
      UP, LEFT_UP, LEFT, LEFT_DOWN, DOWN, RIGHT_DOWN, RIGHT, RIGHT_UP
    }

    private float moveTime_ = 1.0f;
    private float MOVE_DIS = 1.5f;
    private Action startAction_ = null;
    private Action endCallback_ = null;
    private TimeCallback timeCallback_ = null;
    public MoveController(float moveTime, Action startAction = null, Action endCallback = null)
    {
      moveTime_ = moveTime;
      startAction_ = startAction;
      endCallback_ = endCallback;
    }

    // Update is called once per frame
    public void Update()
    {
      timeCallback_?.Update();
    }

    public static void MoveTriger(GameObject own, MoveDir moveDir, bool enabled)
    {
      Animator animator = own.GetComponent<Animator>();

      switch (moveDir) {
        case MoveDir.UP:
        case MoveDir.RIGHT_UP:
        case MoveDir.RIGHT:
          animator.SetBool("isRightMove", enabled);
          break;
        case MoveDir.RIGHT_DOWN:
        case MoveDir.DOWN:
          animator.SetBool("isDownMove", enabled);
          break;
        case MoveDir.LEFT_DOWN:
        case MoveDir.LEFT:
        case MoveDir.LEFT_UP:
          break;
      }
    }

    private Vector3 getMoveVector(MoveDir moveDir) {
      switch (moveDir) {
        case MoveDir.UP:
          return new Vector3(0, MOVE_DIS, 0);
        case MoveDir.RIGHT_UP:
          return new Vector3(MOVE_DIS, MOVE_DIS, 0);
        case MoveDir.RIGHT:
          return new Vector3(MOVE_DIS, 0, 0);
        case MoveDir.RIGHT_DOWN:
          return new Vector3(MOVE_DIS, -MOVE_DIS, 0);
        case MoveDir.DOWN:
          return new Vector3(0, -MOVE_DIS, 0);
        case MoveDir.LEFT_DOWN:
          return new Vector3(-MOVE_DIS, -MOVE_DIS, 0);
        case MoveDir.LEFT:
          return new Vector3(-MOVE_DIS, 0, 0);
        case MoveDir.LEFT_UP:
          return new Vector3(-MOVE_DIS, MOVE_DIS, 0);
      }
      return Vector3.zero;
    }

    public void moveAction(GameObject own, MoveDir moveDir)
    {
      // Animator animator = own.GetComponent<Animator>();

      Vector3 moveVec = getMoveVector(moveDir);
      if (!GameManager.instance().canMoveCharacter(own, own.transform.position + moveVec)) {
        return;
      }

      Vector3 startPosition = own.transform.position;
      Vector3 endPosition = startPosition + moveVec;
      
      timeCallback_ = new TimeCallback(moveTime_,
        (deltaTime, elapsedTime) => {
          own.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveTime_);
        },
        () => {
          Debug.Log(own.transform.position);
          endCallback_?.Invoke();
          // animator?.SetTrigger("idleTrigger");
        }
      );

      startAction_?.Invoke();
      // animator?.SetTrigger("walkTrigger");
      timeCallback_.Start();
    }
  }
}
