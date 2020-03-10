using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotIce : GrabbableObject
{

    [Header("Sounds")]
    [SerializeField] AudioClip sizzleOutSound;
    [SerializeField] AudioClip sizzleInSound;
    [SerializeField] AudioClip sizzleLoopSound;

    private AudioSource myAudioSource;
    private float sizzleVolume = 0.8f;
    private bool playingSizzle = false;
    [SerializeField] float timeToDrop = 1f;
    [SerializeField] float recoveryTime = 3f;
    float dropTimer = 0f;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
   new void Update()
    {
        base.Update();
    }

    IEnumerator MakePlayerDrop(float seconds)
    {
        if (CanHold())
        {
            yield return new WaitForSeconds(seconds);
            SetCanHold(false);
            OnDrop();
            StartCoroutine(MakeGrabbableAfterSeconds(recoveryTime));
            PlaySizzleOut();
        }
        else
            yield return null;
    }

    IEnumerator MakeGrabbableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SetCanHold(true);
    }

    private void OnDrop()
    {
        timesDropped++;

        if (triggerOnDrop == null)
            return;
        //If timesDropped is contained within the triggerOnNthDrop array,
        // trigger the event that corresponds
        for(int i = 0; i < triggerOnNthDrop.Length; i++)
        {
            if(triggerOnNthDrop[i] == timesDropped)
            {
                triggerOnDrop[i].TriggerEvent();
                break;
            }

            if(i == triggerOnNthDrop.Length - 1)
            {
                return;
            }

        }

        //foreach(EventTrigger et in triggerOnDrop)
        //{
        //    et.TriggerEvent();
        //}
    }

    Coroutine dropCoroutine; 
    public override void OnGrab()
    {
        if(dropCoroutine != null)
            StopCoroutine(dropCoroutine);

        PlaySizzleIn();
        dropCoroutine = StartCoroutine(MakePlayerDrop(timeToDrop));
    }

    public void PlaySizzleIn()
    {
        myAudioSource.Stop();
        myAudioSource.clip = sizzleInSound;
        myAudioSource.Play();
    }

    public void PlaySizzleOut()
    {
        myAudioSource.Stop();
        myAudioSource.clip = sizzleOutSound;
        myAudioSource.Play();
    }

    public void PlaySizzle()
    {
        if (!playingSizzle)
        {
            myAudioSource.Stop();
            myAudioSource.clip = sizzleLoopSound;
            myAudioSource.Play();
            myAudioSource.loop = true;
            playingSizzle = true;
        }
    }

    public void StopSizzle()
    {
        myAudioSource.Stop();
        myAudioSource.loop = false;
        playingSizzle = false;
    }

    public override void OnRelease()
    {
        myRigidbody.AddForce(Vector3.up * 2, ForceMode.VelocityChange);
        PlaySizzleOut();
    }

    public override void OnThrow()
    {
        PlaySizzleOut();
    }

    private void OnCollisionStay(Collision collision)
    {
        IMeltable meltableObject = collision.collider.GetComponent<IMeltable>();

        if(meltableObject != null)
        {
            PlaySizzle();
            meltableObject.Melt();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        IMeltable meltableObject = collision.collider.GetComponent<IMeltable>();
        if (meltableObject != null)
        {
            StopSizzle();
            PlaySizzleOut();
        }
    }
}
