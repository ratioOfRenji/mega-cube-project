using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyTouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnPointerDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction OnPointerUpEvent;

    private Slider uiSlider;
    private void Awake()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener (onSliderValueChanged);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerDownEvent != null)
            OnPointerDownEvent.Invoke();
        if (OnPointerDragEvent != null)
            OnPointerDragEvent.Invoke(uiSlider.value);
    }
    public void onSliderValueChanged(float value)
    {
        if (OnPointerDragEvent != null)
            OnPointerDragEvent.Invoke(value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerUpEvent != null)
            OnPointerUpEvent.Invoke();

        // reset slider value
        uiSlider.value = 0f;
    }
    private void OnDestroy()
    {
        //remove listeners to avoid memory leaks
        uiSlider.onValueChanged.RemoveListener(onSliderValueChanged);
    }


}
