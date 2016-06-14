private _objectName = _this select 0;
private _configSection = _this select 1;

_objectName = "arifle_MX_ACO_pointer_F";
_configSection = "CfgWeapons";

private _config = configFile >> _configSection >> _objectName;

if not (_config == "") then 
{
	private _name = getText(_config >> "displayName");
	private _description = getText(_config >> "Library" >> "libTextDesc");
	private _image = getText(_config >> "picture");

	hint parseText format[
		'<t size="1.3" align="center" shadow="true" shadowColor="#000000">%1</t><br /><img image="%2" size="6" align="center" /><br />%3<br />%4',
		_name, _image, _description
	];
};