using UnityEngine;
using System.Collections.Generic;
using System;

public class BuildSite : InteractSite , ICanInteract , IHasProgress
{
    [SerializeField] GameObject _uiTemplate;

    [SerializeField] Transform _uiCanvas;

    [SerializeField] List<PartsSO> _buildOrder;

    [SerializeField] List<PartSO_GameObjects> PartsSO_GameObjectsList;

    List<PartsSO> _partsList;

    int _buildIndex = 0;

    float _buildAmount;
    float _buildAmountMax = 5;

    bool _baseBuilt;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    [Serializable]
    struct PartSO_GameObjects
    {
        public PartsSO partsSO;
        public GameObject GameObject;   
    }
    private void Awake()
    {
        _partsList = new List<PartsSO>();
    }
    public void OnInteract(Player player)
    {
        if (player.TryGetHeldPartObject(out PartObject partObject))
        {
            //player is holding something
            if (partObject.GetPartsSO() == _buildOrder[_buildIndex])
            {
                //this is the part needed for building
                partObject.SetParentTo(this);


                if (_baseBuilt == false) return; // base is not done yet

                //base is done can add electric and drinks
                AddThisPartToPartList(partObject.GetPartsSO());
                ShowPartVisual();
                _buildIndex++;

            }
        }
    }
    public void OnAltInteract(Player player)
    {
        if (_partObjectPacedHere == null) return;
        if (_buildIndex != 0) return;

        //base obj is placed here

        _buildAmount++;


        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = _buildAmount / _buildAmountMax
        });

        if (_buildAmount >= _buildAmountMax)
        {
            ShowPartVisual();

            AddThisPartToPartList(_partObjectPacedHere.GetPartsSO());

            _buildAmount = 0;
            _buildIndex++;
            _baseBuilt = true;

            _partObjectPacedHere = null;
            Destroy(_partPlaceTransform.GetChild(0).gameObject);

        }
    }

    void AddThisPartToPartList(PartsSO partsSO)
    {
        _partsList.Add(partsSO);

        foreach (Transform child in _uiCanvas.transform)
        {
            if (child == _uiTemplate.transform)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach (PartsSO _partsSO in _partsList)
        {
            GameObject newIcon = Instantiate(_uiTemplate, _uiCanvas);
            newIcon.GetComponent<BuildSiteTemplateUI>().SetImageTo(_partsSO._partSprite);
            newIcon.SetActive(true);
        }
    }

    void ShowPartVisual()
    {
        foreach (PartSO_GameObjects p_go in PartsSO_GameObjectsList)
        {
            if (p_go.partsSO == _partObjectPacedHere.GetPartsSO())
            {
                p_go.GameObject.SetActive(true);
            }
        }
    }

    public void ClearBuild()
    {
        _partsList.Clear();
        _buildIndex = 0;
        _baseBuilt = false;

        foreach (Transform child in _uiCanvas.transform)
        {
            if (child.gameObject == _uiTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        foreach (PartSO_GameObjects p_go in PartsSO_GameObjectsList)
        {
            p_go.GameObject?.SetActive(false);
        }
    }

    public List<PartsSO> GetPartsSOList()
    {
        return _partsList;
    }
}
