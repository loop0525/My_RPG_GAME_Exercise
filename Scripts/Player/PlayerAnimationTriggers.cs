using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    // ¶¯»­Ö¡´¥·¢º¯Êý
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
}
