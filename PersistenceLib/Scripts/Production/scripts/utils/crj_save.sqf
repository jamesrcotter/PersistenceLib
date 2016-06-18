#include "crj_arrays.sqf"

// Save a kit for the provided unit

crj_fnc_saveKit = {
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
				"addHandgunItem=" + str (_handgunItems call crj_fnc_arrayToString) + "|" +
				"addSecondaryWeaponItem=" + str (_secondaryWeaponItems call crj_fnc_arrayToString) + "|" +
				"addPrimaryWeaponItem=" + str (_primaryWeaponItems call crj_fnc_arrayToString) + "|" +
				"addItem,assignItem=" + str (_assignedItems call crj_fnc_arrayToString) + "|" +
				"addItemToUniform=" + str (_uniformItems call crj_fnc_arrayToString) + "|" +
				"addItemToVest=" + str (_vestItems call crj_fnc_arrayToString) + "|" +
				"addItemToBackpack=" + str (_backpackItems call crj_fnc_arrayToString);
	
	_response = "PersistenceLib" callExtension "saveKit " + _id + "~" + _kitName + "~" + _details;

	_response;
};

// Save the provided container
crj_fnc_saveBox = {
	params["_box", "_id", "_owner"];

	_items = itemCargo _box; 
	_magazines = magazineCargo _box; 
	
	_details =  "addItemCargo=" + str (_items call crj_fnc_arrayToString) + "|" +
				"addMagazineCargo=" + str (_magazines call crj_fnc_arrayToString);
	
	_response = "PersistenceLib" callExtension "saveBox " + _id + "~" + _owner + "~" + _details;

	_response;
};
