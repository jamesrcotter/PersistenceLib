private _unit = _this select 0;
private _kitName = _this select 1;

crj_fnc_kitLoad = {
	params ["_unit", "_kitName"];

	_params = [_unit];
	_params call crj_fnc_kitRemoveAll;

	_id = getPlayerUID _unit;
	_result = "";

	_response = "PersistenceLib" callExtension "loadKit " + _id + "~" + _kitName;

	if not (_response == "") then
	{
		_kit = _response splitString "|";
		{
			_parts = _x splitString "=";
			if (count _parts == 2) then
			{
				_stringFunctions = _parts select 0; 
				_functions = _stringFunctions splitString ",";
				_stringObjects = _parts select 1;
				_objects = _stringObjects splitString ","; 
				{
					_function = _x;
					{
						_object = _x;
						_params = ["_unit", _function, """" + _object + """"];
						call compile (_params joinString " ");

					} forEach _objects;
				} forEach _functions;
			};
		} forEach _kit;

		_result = "Done.";
	};

	_result;	
};

crj_fnc_kitRemoveAll = {
	params["_unit"];

	removeAllAssignedItems _unit;
	removeAllItems _unit;
	removeAllWeapons _unit;
	removeVest _unit;
	removeUniform _unit;
	removeBackpack _unit;
	removeGoggles _unit;
	removeHeadgear _unit;
};

_params = [_unit, _kitName];
_response = str ([_params, "crj_fnc_kitLoad", false] call BIS_fnc_MP);

diag_log _response;
hint format ["%1", _response];