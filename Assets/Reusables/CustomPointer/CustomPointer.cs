using UnityEngine;
using UnityEngine.UI;

public class CustomPointer : MonoBehaviour
{
    #region References

    public bool _isHovering;
    public Camera _camera;

    [HideInInspector]
    public IHoverable _currentHoveredObject;
    [HideInInspector]
    public IHoverable[] hoveredObjects;

    #endregion

    #region Unity Functions

    void Start()
    {
        if (_camera == null) _camera = Camera.main;

        EnableRaycastTarget();
    }

    #endregion


    #region Public Functions

    public void SetPointerPosition(Vector2 input)
    {
        Vector3 pos = _camera.ScreenToWorldPoint(input);
        pos.z = -1;
        transform.position = pos;
    }



    public void CheckHover(Vector2 input)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.zero);
        if (hit.collider != null)
        {
            //_currentHoveredObject = hit.collider.gameObject.GetComponent<EWBA_IHoverable>();
            hoveredObjects = hit.collider.gameObject.GetComponentsInChildren<IHoverable>();

            if (hoveredObjects != null && hoveredObjects.Length > 0)
            {
                //This is OnPointerEnter condition
                if (!_isHovering)
                {
                    foreach (IHoverable hoverable in hoveredObjects)
                        hoverable.OnHoverStart();
                }

                _isHovering = true;

                //This is OnPointerOver condition
                foreach (IHoverable hoverable in hoveredObjects)
                    hoverable.OnHover();
            }
        }
        else
        {
            //This is OnPointerExit condition
            if (hoveredObjects != null && hoveredObjects.Length > 0)
            {
                _isHovering = false;
                foreach (IHoverable hoverable in hoveredObjects)
                    hoverable.OnHoverLost();
                //  _currentHoveredObject = null;
                hoveredObjects = null;
            }
        }
    }

    public void Click(Vector2 input)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("Click function is invoked");

            Button clickedButton = hit.collider.gameObject.GetComponent<Button>();

            if (clickedButton != null) clickedButton.onClick.Invoke();

            IClickable clickedObject = hit.collider.gameObject.GetComponent<IClickable>();

            if (clickedObject != null) clickedObject.OnClickBehaviour();
        }
    }

    #endregion

    #region Private Functions

    private void EnableRaycastTarget()
    {
        var graphicImage = this.GetComponent<Image>();

        if (!graphicImage.raycastTarget)
            graphicImage.raycastTarget = true;
    }

    #endregion
}
