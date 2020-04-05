using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike {
  public class Character : MonoBehaviour
  {
    public CharacterAnimationController animationController_ { private set; get; } = null;
    public Animator animator_ { private set; get; } = null;

    private int spirteIndex_ = 0;
    private int spriteAdd_ = 1;
    protected virtual void Start()
    {
      animationController_ = GetComponent<CharacterAnimationController>();
      animator_ = GetComponent<Animator>();
      spirteIndex_ = 0;
    }

    // Update is called once per frame
    protected virtual void Update() {}

    public void NextMotionSpite()
    {
      spirteIndex_ = spirteIndex_ + spriteAdd_;
      if (spirteIndex_ == 0 || spirteIndex_ == animationController_.MotionSpriteNumPerDirection() - 1) {
        spriteAdd_ *= -1;
      }
      var sprite = animationController_.GetSprite(spirteIndex_);
      var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      spriteRenderer.sprite = sprite;
    }

    public void SetMotionType(int motionType)
    {
      animationController_.SetMotionType((CharacterAnimationController.MotionType)motionType);
      animator_.SetInteger("Motion", motionType);
    }

    public virtual void MotionStart() {}
    public virtual void MotionEnd(int motionState) {}
  }
}