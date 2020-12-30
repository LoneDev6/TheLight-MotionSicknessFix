using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace TheLightTweaks
{
    [HarmonyPatch(typeof(PlayerScript))]
    [HarmonyPatch("Update")]
    class Patch1
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_R4)
                {
                    float op = (float) codes[i].operand;
                    if (op == 65)
                        codes[i].operand = 75.0f;
                    else if (op == 60)
                        codes[i].operand = 71.0f;
                }
            }

            return codes.AsEnumerable();
        }
    }
}
