#include "..\utils\crj_arrays.sqf"
#include "..\utils\crj_save.sqf"

private _box = _this select 0;
private _id = _this select 1;
private _owner = _this select 2;

_params = [_box, _id, _owner];
_response = str ([_params, "crj_fnc_saveBox", false] call BIS_fnc_MP);

diag_log _response;
hint format ["%1", _response];