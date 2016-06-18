// Load kit for the provided unit
crj_fnc_getConfig = {
	params ["_configSection", "_objectName"];
	_config = configFile >> _configSection >> _objectName;
	_config;
};