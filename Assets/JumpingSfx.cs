using UnityEngine;

public class JumpingSfx : StateMachineBehaviour
{
    public string AudioSourceName;
    private AudioSource _sfx;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_sfx == null)
            _sfx = GameObject.Find(AudioSourceName).GetComponent<AudioSource>();
        _sfx.Play();
    }
}
