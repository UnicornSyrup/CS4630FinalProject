using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerMask : MonoBehaviour
{
    [System.Serializable]
    public class MaskAnimationStep
    {
        public Vector3 pos;
        public float rot;
        public float duration;
        public AnimationCurve curve;

        public MaskAnimationStep(Vector3 npos, float nrot, float nduration = 0, AnimationCurve ncurve = null)
        {
            pos = npos;
            rot = nrot;
            duration = nduration;
            curve = ncurve;
        }
    }

    public List<MaskAnimationStep> steps = new List<MaskAnimationStep>();
    private float stepStartTime;

    private MaskAnimationStep prevStep;

    private int _currentStepIndex;
    private int currentStepIndex
    {
        get { return _currentStepIndex; }
        set { _currentStepIndex = value % steps.Count; }
    }

    private MaskAnimationStep currentStep
    {
        get
        {
            return steps[currentStepIndex];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stepStartTime = Time.time;
        prevStep = new MaskAnimationStep(transform.position, transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(steps.Count != 0)
        {
            if(currentStep.duration != 0)
            {
                float progress = Mathf.Clamp01((Time.time - stepStartTime) / currentStep.duration);
                transform.position = Vector3.Lerp(prevStep.pos, currentStep.pos, currentStep.curve.Evaluate(progress));
                transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(prevStep.rot, currentStep.rot, currentStep.curve.Evaluate(progress)));

                if (progress == 1)
                {
                    prevStep = currentStep;
                    currentStepIndex++;
                    stepStartTime = Time.time;
                }
            } else
            {
                transform.position = currentStep.pos;
                transform.eulerAngles = new Vector3(0, 0, currentStep.rot);
                prevStep = currentStep;
                currentStepIndex++;
                stepStartTime = Time.time;
            }

        }
    }
}
