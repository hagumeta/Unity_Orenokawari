using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPlatformActor : PlatformActor {

    public bool IsTurnOnBlockCorner;
    public LayerMask BlockLayerMask;
    public float WalkSpeed;


    private SpriteRenderer spriteRenderer; 

    protected override void Start()
    {
        base.Start();

        this.HorizontalSpeed = this.WalkSpeed * this.FacingDirectionHorizontal;


        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    

    protected override void Update()
    {
        base.Update();
        
        if (this.CurrentState.IsLanding && this.IsTurnOnBlockCorner)
        {

            Vector3 pos1 = this.transform.position + new Vector3(this.spriteRenderer.size.x/2 * this.transform.localScale.x, 0),
                pos2 = pos1 - new Vector3(0, this.spriteRenderer.size.y * this.transform.localScale.y);
            
            RaycastHit2D hit =  Physics2D.Linecast(pos1, pos2, this.BlockLayerMask);

            
            if (hit.transform == null)
            {
              this.turn();
            }
        }
    }

    private void turn()
    {
        this.HorizontalSpeed *= -1;
        this.FacingDirectionHorizontal *= -1;
    }
}