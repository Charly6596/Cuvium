﻿using UnityEngine;

[CreateAssetMenu(
    fileName = "BoolVariable.asset",
    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "bool",
    order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 5)]
public sealed class BoolVariable : BaseVariable<bool>
{
}