function Setngs = ReadSettings_v1(fpath)
txt = fileread(fpath);
Setngs.version = ReadTokenFromString(txt, 'Version');
Setngs.LEDpower = ReadTokenFromString(txt, 'LED power:');
Setngs.exposureTime = ReadTokenFromString(txt, 'Exposure time:');
Setngs.frameRate = ReadTokenFromString(txt, 'Frame rate:');

% for version 2 of data
if isempty(Setngs.exposureTime),
    Setngs.exposureTime = ReadTokenFromString(txt, 'Exposure Time (ms):');
    Setngs.frameRate = ReadTokenFromString(txt, 'Frame Rate (fps):');
end

