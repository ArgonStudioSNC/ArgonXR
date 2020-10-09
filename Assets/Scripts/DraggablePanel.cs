using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DraggablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    #region PUBLIC_MEMBER_VARIABLES

    public Transform UIMask;
    public Transform mainContent;
    public float accelerationRate;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private float mCurrentPosition;
    private float mMaxPosition;
    private float mTmpPosition;
    private RectTransform m_rectTransform;
    private Image mUIMaskImage;
    private CanvasScaler mScaler;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    protected void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        mMaxPosition = Mathf.Abs(m_rectTransform.rect.width);
        mCurrentPosition = 0.0f;
        mUIMaskImage = UIMask.GetComponent<Image>();
        mScaler = GetComponentInParent<CanvasScaler>();
    }

    protected void Update()
    {
        UIMask.gameObject.SetActive(mCurrentPosition > 0.0f);
        Color color = mUIMaskImage.color;
        color.a = (mCurrentPosition / mMaxPosition) / 2.0f;
        mUIMaskImage.color = color;
        m_rectTransform.anchoredPosition = new Vector2(mCurrentPosition * -1.0f, 0.0f);
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void OnBeginDrag(PointerEventData eventData)
    {
        mTmpPosition = mCurrentPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mTmpPosition += ((mScaler.referenceResolution.x / Screen.width) * eventData.delta.x *-1.0f);
        mCurrentPosition = Mathf.Clamp(mTmpPosition, 0.0f, mMaxPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (mCurrentPosition > mMaxPosition / 2.0f) StartCoroutine(OpenAnimation()); else StartCoroutine(CloseAnimation());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!eventData.dragging)
        {
            if (mCurrentPosition == 0) StartCoroutine(OpenAnimation());
            else if (mCurrentPosition == mMaxPosition) StartCoroutine(CloseAnimation());
        }
    }

    public void OpenPanel()
    {
        if (mCurrentPosition == 0.0f) StartCoroutine(OpenAnimation());
    }

    public void ClosePanel()
    {
        if (mCurrentPosition == mMaxPosition) StartCoroutine(CloseAnimation());
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    private IEnumerator CloseAnimation()
    {
        mTmpPosition = mCurrentPosition;
        float speed = -250;
        for (mTmpPosition = mCurrentPosition; mTmpPosition > 0; mTmpPosition += speed * Time.deltaTime)
        {
            speed -= Time.deltaTime * accelerationRate * 5000;
            mCurrentPosition = Mathf.Clamp(mTmpPosition, 0.0f, mMaxPosition);
            yield return null;
        }
        mCurrentPosition = 0.0f;
    }

    private IEnumerator OpenAnimation()
    {
        mTmpPosition = mCurrentPosition;
        float speed = +250;
        for (mTmpPosition = mCurrentPosition; mTmpPosition < mMaxPosition; mTmpPosition += speed * Time.deltaTime)
        {
            speed += Time.deltaTime * accelerationRate * 5000;
            mCurrentPosition = Mathf.Clamp(mTmpPosition, 0.0f, mMaxPosition);
            yield return null;
        }
        mCurrentPosition = mMaxPosition;
    }

    #endregion // PRIVATE_METHODS
}