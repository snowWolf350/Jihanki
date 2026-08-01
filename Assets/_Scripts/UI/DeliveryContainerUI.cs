using UnityEngine;
using System.Collections.Generic;

public class DeliveryContainerUI : MonoBehaviour
{
    [SerializeField] List<BuildSite> _buildSiteList;

    [SerializeField] GameObject _deliveryTemplate;

    private void Start()
    {
        foreach (BuildSite buildSite in _buildSiteList)
        {
            GameObject newtemplate = Instantiate(_deliveryTemplate, transform);

            newtemplate.GetComponent<DeliveryTemplateUI>().SetBuildSiteTo(buildSite);

            newtemplate.SetActive(true);
        }
    }
}
