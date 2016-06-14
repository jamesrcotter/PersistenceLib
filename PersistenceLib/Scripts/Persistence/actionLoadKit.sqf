private _unit = _this select 3 select 0;
private _kitName = _this select 3 select 1;

_handle = [_unit, _kitName] execVM "persistenceScripts\loadKit.sqf";