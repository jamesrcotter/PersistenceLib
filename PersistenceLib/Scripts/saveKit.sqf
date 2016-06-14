private _unit = _this select 3 select 0;
private _kitName = _this select 3 select 1;

fn_kitSave = {
	params["_unit", "_kitName"];

	_id = getPlayerUID _unit;

	_headgear = headgear _unit; 
	_goggles = goggles _unit; 
	_uniform = uniform _unit; 
	_vest = vest _unit; 
	_backpack = backpack _unit; 
	_primaryWeapon = primaryWeapon _unit; 
	_secondaryWeapon = secondaryWeapon _unit; 
	_handgunWeapon = handgunWeapon _unit; 
	_assignedItems = assignedItems _unit; 
	 
	_uniformItems = uniformItems _unit; 
	_vestItems = vestItems _unit; 
	_backpackItems = backpackItems _unit; 
	_primaryWeaponItems = primaryWeaponItems _unit; 
	_secondaryWeaponItems = secondaryWeaponItems _unit; 
	_handgunItems = handgunItems _unit; 
	
	_details =  "addHeadgear=" + _headgear + "|" + 
				"addGoggles=" + _goggles + "|" +
				"forceAddUniform=" + _uniform + "|" +
				"addVest=" + _vest + "|" +
				"addBackpack=" + _backpack + "|" +
				"addWeapon=" + _primaryWeapon + "|" +
				"addWeapon=" + _secondaryWeapon + "|" +
				"addWeapon=" + _handgunWeapon + "|" +
				"addHandgunItem=" + str (_handgunItems call fn_arrayToString) + "|" +
				"addSecondaryWeaponItem=" + str (_secondaryWeaponItems call fn_arrayToString) + "|" +
				"addPrimaryWeaponItem=" + str (_primaryWeaponItems call fn_arrayToString) + "|" +
				"addItem,assignItem=" + str (_assignedItems call fn_arrayToString) + "|" +
				"addItemToUniform=" + str (_uniformItems call fn_arrayToString) + "|" +
				"addItemToVest=" + str (_vestItems call fn_arrayToString) + "|" +
				"addItemToBackpack=" + str (_backpackItems call fn_arrayToString);
	
	_response = "PersistenceLib" callExtension "saveKit " + _id + "~" + _kitName + "~" + _details;

	_response;
};

fn_arrayToString = {
    _return = _this joinString ",";
	_return;
};

_params = [_unit, _kitName];
_response = str (_params call fn_kitSave);

diag_log _response;
hint format ["%1", _response];