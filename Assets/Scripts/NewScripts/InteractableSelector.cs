using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableSelector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler  
{

    [SerializeField] public GameObject ActionPanel;
    [SerializeField] public InteractableStats intStats;
    [SerializeField] private UiInteracion uiStat;


    private void Awake()
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        uiStat.currentPhobiaName = intStats.phobiaName;
        uiStat.currentActionName = intStats.actionName;

        uiStat.currentUnsuccessfulDesc = intStats.unsuccessfulDesc;
        uiStat.currentSuccessfulDesc = intStats.successfulDesc;
        uiStat.currentCryticallDesc = intStats.cryticallDesc;
        uiStat.currentUnsuccessfulNightDesc = intStats.unsuccessfulNightDesc;
        uiStat.currentUnvisibleDesc = intStats.unvisibleDesc;

        uiStat.currentHealthDamage = intStats.healthDamage;
        uiStat.currentEnergyCost = intStats.energyCost;
        uiStat.currentIsUsed = intStats.isUsed;

        uiStat.actionPlanelInitialization();
        ActionPanel.SetActive(true);
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }








}
