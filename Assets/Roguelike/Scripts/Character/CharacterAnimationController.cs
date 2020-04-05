
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Roguelike {
  public class CharacterAnimationController : MonoBehaviour
  {
    public const int DIRECTION_NUM = 8;

    public enum MotionType : Int32 {
      IDLE_UP = 0, IDLE_UP_LEFT, IDLE_LEFT, IDLE_DOWN_LEFT, IDLE_DOWN, IDLE_DOWN_RIGHT, IDLE_RIGHT, IDLE_UP_RIGHT, // IDLE
      WALK_UP = 10, WALK_UP_LEFT, WALK_LEFT, WALK_DOWN_LEFT, WALK_DOWN, WALK_DOWN_RIGHT, WALK_RIGHT, WALK_UP_RIGHT, // MOVE
    }

    public enum MotionStates : Int32 {
      IDLE, WALK
    }

    private MotionType motionType_ = MotionType.IDLE_DOWN;
    
    public Sprite[] idleSprites;
    public Sprite[] walkSprites;

    public int MotionSpriteNumPerDirection()
    {
      var sprites = GetStateSprites(motionType_);
      return sprites.Length / DIRECTION_NUM;
    }

    public MotionStates GetMotionState(MotionType motionType)
    {
      return (MotionStates)((int)motionType / 10);
    }

    private Sprite[] GetStateSprites(MotionType motionType)
    {
      MotionStates state = GetMotionState(motionType);
      switch (state) {
        case MotionStates.IDLE:
          return idleSprites;
        case MotionStates.WALK:
          return walkSprites;
      }

      Assert.IsTrue(false, "MotionState not found.");
      return idleSprites;
    }

    public Sprite GetSprite(int index)
    {
      var sprites = GetStateSprites(motionType_);
      var motion = (int)motionType_ % 10;
      int spriteNum = sprites.Length / DIRECTION_NUM;
      return sprites[motion * spriteNum + index];
    }

    public MotionType ChangeMotionState(MotionStates state)
    {
      var stateBaseIndex = (int)state * 10;
      motionType_ = (MotionType)(stateBaseIndex + (int)motionType_ % 10);
      return motionType_;
    }

    public MotionType GetMotionType()
    {
      return motionType_;
    }

    public void SetMotionType(MotionType motionType)
    {
      motionType_ = motionType;
    }
  }
}
