using UnityEngine;
using System.Collections;

public class BaldLogic : MonoBehaviour {

    public GameObject[] hairStyle;
    public GameObject[] baldModelArray;
    public Transform hairTransform;
    public GameObject dropGameObject, chewbacca;
    public Animator animator, dropAnimator;
    public AnimationClip desactivateAnimation, dropAnimation, baldAnimation;
    public AudioManager audioManager;
    public float speed;
    public bool bald, dropGlue;
    public Color[] hairColor;

    private int baldModel, hairStyleNumber;
    private SpriteRenderer spriteRender;
    public float currentTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //FollowHead();
        if(animator != null)animator.speed = speed;
        dropAnimator.speed = speed;
    }

    public void ActivateBald()
    {
        chewbacca.SetActive(false);
        baldModelArray[baldModel].SetActive(false);
        int prevBaldModel = baldModel;
        while(baldModel == prevBaldModel) baldModel = Mathf.FloorToInt(Random.Range(0.0f, baldModelArray.Length));
        baldModelArray[baldModel].SetActive(true);
        animator = baldModelArray[baldModel].GetComponent<Animator>();
        animator.Play("ActivateAnim01");
    }

    public void ActivateHairStyle()
    {
        hairStyle[hairStyleNumber].SetActive(false);
        int prevHairStyleNum = hairStyleNumber;
        
        while (hairStyleNumber == prevHairStyleNum) hairStyleNumber = Mathf.FloorToInt(Random.Range(0.0f, hairStyle.Length));
        hairStyle[hairStyleNumber].SetActive(true);
        spriteRender = hairStyle[hairStyleNumber].GetComponent<SpriteRenderer>();
        spriteRender.color = hairColor[(int)Random.Range(0.0f, hairColor.Length)];
        FollowHead();
    }

    public void DesactivateBald()
    {
        if (dropGlue)
        {
            dropGameObject.SetActive(true);
            dropAnimator.Play("dropAnim");
        }
        else
        {
            animator.Play("DesactivateAnim01");
        }
    }

    public void DeasctivateHairStyle()
    {
        hairStyle[hairStyleNumber].SetActive(false);
    }

    public void ShowDrop()
    {
        if (bald)
        {
            ActivateHairStyle();
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            audioManager.Play(audioManager.wellDone[Random.Range(0, audioManager.wellDone.Length)], audiSor, 1.0f);
        }
        else
        {
            chewbacca.SetActive(true);
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            audioManager.Play(audioManager.wookieSound, audiSor, 1.0f);
            DeasctivateHairStyle();
        }

        animator.Play("DesactivateAnim01");
        dropGameObject.SetActive(false);
    }

    public void DescativateMan()
    {
        DeasctivateHairStyle();
        baldModelArray[baldModel].SetActive(false);
    }

    public void FollowHead()
    {
        for (int i = 0; i < baldModelArray.Length; i++)
        {
            if (baldModelArray[i].activeInHierarchy)
            {
                hairTransform.parent = baldModelArray[i].transform;
                hairTransform.localPosition = Vector3.zero;
            }
        }
    }


}
