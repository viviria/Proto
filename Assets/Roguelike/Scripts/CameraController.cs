using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike {
  public class CameraController : MonoBehaviour
  {
    public GameObject target_ = null;
    public Vector3 diffPostion_ = Vector3.zero;
    public GameObject targetLight_ = null;
    public Vector3 lightDiffPosition_ = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      transform.position = target_.transform.position + diffPostion_;

      if (targetLight_ != null) {
        targetLight_.transform.position = target_.transform.position + lightDiffPosition_;
      }
    }
  }
}
