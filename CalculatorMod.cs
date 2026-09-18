using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(CalculatorMod.CalculatorMod), "CalculatorMod", "1.0.0", "dark")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace CalculatorMod
{
    public class CalculatorMod : MelonMod
    {
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("[CalculatorMod] Cargado.");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                var phone = GameObject.FindObjectOfType<PhoneManager>();
                if (phone != null)
                {
                    var openMethod = phone.GetType().GetMethod("Open",
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    if (openMethod != null)
                        openMethod.Invoke(phone, null);
                }
            }
        }
    }
}   
