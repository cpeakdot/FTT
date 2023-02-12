using UnityEngine;
using cpeak.cPool;
using Internal = UnityEngine.Internal;
using DG.Tweening;
using UnityEngine.UI;
using FTT.Consumable;
using TMPro;

namespace FTT.Managers
{
    public class ParticleManager : MonoBehaviour
    {
        private cPool pool;
        [SerializeField] private RectTransform shopRectTransform;

        public enum ParticleType
        {
            Consumable,
            Money,
        }

        private void Start()
        {
            pool = cPool.instance;
        }

        public void InitParticle(ParticleType particleType,[Internal.DefaultValue("Vector3.zero")] Vector3 position,[Internal.DefaultValue("Quaternion.Identity")] Quaternion rotation, ConsumableSO consumableSO = null, int amount = 0)
        {
            switch (particleType)
            {
                case ParticleType.Consumable:

                    GameObject consumableParticle = null;
                    
                    if (position == Vector3.zero)
                    {
                        var returnDur = 1f;
                        var goUpDur = 1f;
                        consumableParticle = pool.GetPoolObject("consumableParticleGUI" , Vector3.zero , Quaternion.identity , true , returnDur);
                        consumableParticle.transform.SetParent(shopRectTransform);
                        var rect = consumableParticle.GetComponent<RectTransform>();
                        rect.anchoredPosition = new Vector3(0 , -200f , 0);
                        rect.DOMoveY(100f , goUpDur).SetRelative(true);
                    }
                    else
                    {
                        var returnDur = 2f;
                        var goUpDur = 2f;
                        var yOffset = 1f;
                        consumableParticle = pool.GetPoolObject("consumableParticle" , position + (Vector3.up * yOffset) , rotation , true , returnDur);
                        consumableParticle.transform.DOMoveY(2f , goUpDur).SetRelative(true);
                    }

                    var icon = consumableParticle.GetComponentInChildren<Image>();

                    if(position == Vector3.zero)
                    {
                        var cAmount = consumableParticle.GetComponentInChildren<TextMeshProUGUI>();
                        if(cAmount != null)
                        {
                            cAmount.SetText("+" + amount);
                        }
                    }
                    else
                    {
                        var cAmount = consumableParticle.GetComponentInChildren<TextMeshPro>();
                        if(cAmount != null)
                        {
                            cAmount.SetText("+" + amount);
                        }
                        var lScale = consumableParticle.transform.localScale;
                        lScale.x *= -1f;
                        consumableParticle.transform.localScale = lScale;
                        consumableParticle.transform.LookAt(Camera.main.transform, Vector3.up);
                    }
                    if(icon != null)
                    {
                        icon.sprite = consumableSO.icon;
                    }

                    break;
                case ParticleType.Money:
                    break;
                default:
                    break;
            }
        }
    }
}

