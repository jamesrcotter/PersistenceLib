#include "..\utils\crj_load.sqf"

private _unit = _this select 0;
private _kitName = _this select 1;

_params = [_unit, _kitName];
_response = str ([_params, "crj_fnc_loadKit", false] call BIS_fnc_MP);

diag_log _response;
hint format ["%1", _response];