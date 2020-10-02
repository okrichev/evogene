function setvalue = ReadTokenFromString(str, tokenName)
pos = strfind(str, tokenName);
setvalue = sscanf(str(pos:end), [tokenName '%f']);
