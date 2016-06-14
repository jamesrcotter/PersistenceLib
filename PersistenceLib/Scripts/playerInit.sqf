private _unit = _this select 0;
_unit addAction ["Save", "saveKit.sqf", [_unit, "testKit"], 0, false, false, "", "(_target == _unit)"];
_unit addAction ["Load", "loadKit.sqf", [_unit, "testKit"], 0, false, false, "", "(_target == _unit)"];