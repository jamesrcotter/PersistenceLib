private _unit = _this select 3 select 0;
private _function = _this select 3 select 1; 
private _configSection = _this select 3 select 2;


_params = [_function, "_unit"];
_object = call compile (_params joinString " ");

private _config = configFile >> _configSection >> _object;
private _name = getText(_config >> "displayName");
private _description = getText(_config >> "Library" >> "libTextDesc");
private _image = getText(_config >> "picture");

hint parseText format[
	'<t size="1.3" align="center" shadow="true" shadowColor="#000000">%1</t><br /><img image="%2" size="6" align="center" /><br />%3<br />%4',
	_name, _image, _description
];

// this addAction ["Inspect Weapon", "displayScripts\hintObject.sqf", ["arifle_MX_ACO_pointer_F", "CfgWeapons"], 0, false, false, "", "{ _target == _this }"];