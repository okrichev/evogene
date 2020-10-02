function S = ProcessFluorImagerData_v1(fpath, varargin)
% processes data produced by our homebuilt fluorescence imager
% fpath - folder path to data
% vararging options come in pairs: descriptive string and the option
% 
% 'Grid options' : 'auto'| 'standard' | 'user-defined' : determines how the
%                   cell grid will be placed. In 'auto' (default) regime the cell
%                   grid will be determined automatically by FFT analysis
%                   of the image. In 'stardard' regime the predetermined
%                   pattern will be used (defined below). In 'user-defined'
%                   the use will supply a cell array of {MinLocationsCols,
%                   MinLocationsRows} where each entry is a vector. The
%                   user defined definition should be preceded by an 'User
%                   grid' parameter, i.e. (..., 'User grid', {MinLocationsCols,
%                   MinLocationsRows}, ...).
% 'DarkFileTemplate' : the file name template in dark condtions (default 'Plant_dark_*')
% 'SatFileTemplate'  : the file name template in saturating condtions (default 'Plant_sat_*')
% 'Neighbors'        : the size of neighborhood for neighborhood operations
%                       (3 by default)
% 'DoOutput'         : true|false - whether to output figures and data (true by default)
% 'OutputFpath'      : the path to output folder. By default [fpath 'Processed' filesep]
% 'DishSize'         : [rows, columns] the size of multiwell plate ([8, 12] is default)


% Constants: to be checked by calibrations

% from '/Users/oleg/Documents/Evogene/Process_310816.mat'
P_100_to_15 = 16.0793; % ratio of measured LED brightnesses for LEDpower =100 to LEDpower = 15;
P_150_to_15 = 18.8684; % ratio of measured LED brightnesses for LEDpower =150 to LEDpower = 15;

% New values from '/Users/oleg/Documents/Evogene/Calibrations_230817/Process_230817.mat'
P_255_to_10 = 64.5833;
P_255_to_20 = 21.7421;

% Standard cell grid (from the analysis of Experiment_6562/Plate#SP215.R1)
MinLocationsCols = [1 126 223 322 420 519 616 713 812 911 1010 1109 1239];
MinLocationsRows = [131   257   355   454   551   649   748   846   974];

% filetype: might introduce into options eventually
filetype = 'PNG';
% plate name template: might introduce into options eventually
plateNameTmpl = 'Plate#';

% read options or use defaults
GridOptions = ParseInputs('Grid options', 'auto', varargin);
DarkFileTemplate = ParseInputs('DarkFileTemplate', 'Plant_dark_*', varargin);
SatFileTemplate = ParseInputs('SatFileTemplate', 'Plant_sat_*', varargin);
Neighbors = ParseInputs('Neighbors', 3, varargin); %number of neighbors for image opening operation
DoOutput = ParseInputs('DoOutput', true, varargin);
OutputFpath = ParseInputs('OutputFpath', [fpath 'Processed' filesep], varargin);
DishSize = ParseInputs('DishSize', [8, 12], varargin);
MinThreshold = ParseInputs('MinThresh', [], varargin); % [] for automatic determination
MinGraylevelForWarning = ParseInputs('MinGrayLevelForWaring', 0.3, varargin); % as fraction from MaxPix

% extract plate name
nn = strfind(fpath, plateNameTmpl);
S.plateName = fpath((nn+length(plateNameTmpl)):(end-1));
S.warnings = {};

% read measurement settings files
DarkSettings = ReadSettings_v1([fpath 'AcqusitionSettings_dark_.txt']);
SatSettings = ReadSettings_v1([fpath 'AcqusitionSettings_sat_.txt']);

if (DarkSettings.version == 1),
    MaxPix = 255; %maximal value of pixel
else
    MaxPix = 1023;
end

if (DarkSettings.LEDpower == 15),
    if (SatSettings.LEDpower == 100),
        PowerRatio = P_100_to_15;
    elseif (SatSettings.LEDpower == 150),
        PowerRatio = P_150_to_15;
    else
        S.warnings = [S.warnings 'No power ratio calibration for these settings exist!'];
        error('No power ratio calibration for these settings exist!');
    end;
elseif (DarkSettings.LEDpower == 10),
        if (SatSettings.LEDpower == 255),
            PowerRatio = P_255_to_10;
        else
            S.warnings = [S.warnings 'No power ratio calibration for these settings exist!'];
            error('No power ratio calibration for these settings exist!');
        end;
elseif (DarkSettings.LEDpower == 20),
        if (SatSettings.LEDpower == 255),
            PowerRatio = P_255_to_20;
        else
            S.warnings = [S.warnings 'No power ratio calibration for these settings exist!'];
            error('No power ratio calibration for these settings exist!');
        end;

else
    S.warnings = [S.warnings 'No power ratio calibration for these settings exist!'];
    error('No power ratio calibration for this dark settings exist!');
end;


% the scaling factor for Saturating to Dark images ratio
ImgRatioScaling = DarkSettings.exposureTime/SatSettings.exposureTime/PowerRatio;


S.DarkSettings = DarkSettings;
S.SatSettings = SatSettings;
S.PowerRatio = PowerRatio;
S.ImgRatioScaling = ImgRatioScaling;
S.MaxPix = MaxPix;
S.Neighbors = Neighbors;

% load dark images (second image)
darkfnames = dir([fpath DarkFileTemplate]);
% sort by datenum
%datenum = [darkfnames.datenum];
%[temp, J] = sort(datenum);

% sort by number in the file name
Nfile = []; 
for i = 1:length(darkfnames), 
    temp = regexp(darkfnames(i).name(1:(end-4)), DarkFileTemplate(1:(end-1)), 'split'); 
    Nfile(i) = str2num(temp{2}); 
end
[temp, J] = sort(Nfile);
darkfnames = darkfnames(J);
darkfnames = {darkfnames.name}';

ImgDark = imread([fpath darkfnames{2}], filetype);

if (DarkSettings.version == 1), 
    ImgDark = double(ImgDark(:,:,1));
else % convert 16 bit (camera saving format) to 10 bit (actual camera format)
    ImgDark = double(round(double(ImgDark)/64));
end

% find wells
if strcmp(GridOptions, 'auto'),
    [status, MinLocationsCols1, MinLocationsRows1, waveLen, wrnings] = GetWellsAuto_v1(ImgDark, 'DishSize', DishSize, 'MaxPix', MaxPix);
    if status,
        MinLocationsCols = MinLocationsCols1;
        MinLocationsRows = MinLocationsRows1;
    end; %else if status = false, standard setting will be used.
    S.warnings = [S.warnings wrnings];
elseif strcmp(GridOptions, 'standard'),
    % no action: standard definitions are already introduced
elseif strcmp(GridOptions, 'user-defined'),
    temp = ParseInputs('User grid', [], varargin);
    if ~iscell(temp),
        S.warnings = [S.warnings 'The User grid parameter should be a cell array!'];
        error('The User grid parameter should be a cell array!');
    elseif (length(temp)~=2),
        S.warnings = [S.warnings 'The User grid parameter should be a cell array of two vectors!'];
        error('The User grid parameter should be a cell array of two vectors!');        
    elseif ~(isvector(temp{1})&isvector(temp{2})),
        S.warnings = [S.warnings 'The User grid parameter should be a cell array of two vectors!'];
        error('The User grid parameter should be a cell array of two vectors!');
    end;
    MinLocationsCols = temp{1};
    MinLocationsRows = temp{2};
end;
S.MinLocationsCols =MinLocationsCols;
S.MinLocationsRows = MinLocationsRows;


% show in the image
close all;
Imggrid(:, :, 1) = zeros(size(ImgDark));
Imggrid(:, :, 2) = ImgDark/max(ImgDark(:));
Imggrid(:, :, 3) = zeros(size(ImgDark));
Imggrid(MinLocationsRows, MinLocationsCols(1):MinLocationsCols(end) , 1) = 1;
Imggrid(MinLocationsRows(1):MinLocationsRows(end), MinLocationsCols, 1) = 1;
Imggrid(MinLocationsRows+1, MinLocationsCols(1):MinLocationsCols(end), 1) = 1; %to emphasize on scaled figures
Imggrid(MinLocationsRows(1):MinLocationsRows(end), MinLocationsCols+1, 1) = 1;
imagesc(Imggrid);
set(gca, 'YTickLabel', {'A'; 'B'; 'C'; 'D'; 'E'; 'F'; 'G'; 'H'}, ...
    'XTick', 1:12);
axis image;
figure(gcf)
%set(gcf, 'PaperPositionMode', 'auto')
%saveas(gcf, [fpath 'DarkImage_wells.jpg']);


% get thresholds by well
S.ImgDark = ImgDark;
S = GetThresholdsAndAreas_v2(S, 'MinThresh', MinThreshold);
    
% get images at saturating illumination
satfnames = dir([fpath SatFileTemplate]);
% sort by datenum
%datenum = [satfnames.datenum];
%[temp, J] = sort(datenum);
% sort by number in the file name
Nfile = []; 
for i = 1:length(satfnames), 
    temp = regexp(satfnames(i).name(1:(end-4)), SatFileTemplate(1:(end-1)), 'split'); 
    Nfile(i) = str2num(temp{2}); 
end
[temp, J] = sort(Nfile);

satfnames = satfnames(J);
satfnames = {satfnames.name}';

for i = 2:length(satfnames),
    ImgSat = imread([fpath satfnames{i}], filetype);
    if (SatSettings.version == 1), 
        ImgSat = double(ImgSat(:,:,1));
    else % convert 16 bit (camera saving format) to 10 bit (actual camera format)
        ImgSat = double(round(double(ImgSat)/64));
    end
    
    if (max(ImgSat(:)) < (MaxPix*MinGraylevelForWarning)),
        S.warnings = [S.warnings ['The intensity of ' satfnames{i} ' might be too low!']];
    end
    FvFm_data = AnalyzeFvFm_v1(ImgDark, ImgSat, S);
    S.FvFm.FvFm_per_well(:, :, i-1) = FvFm_data.FvFm;
    S.FvFm.FvFm_Image(:, :, i-1) = FvFm_data.Image;
    S.FvFm.frac_satPix(:, :, i-1) = FvFm_data.frac_satPix;
end;

%
%outputs 

if DoOutput,
    OutputSinglePlateProcess(S, OutputFpath);
end;

%
%FvFm kinetic per well
figure;
t = (1:(length(satfnames)-1));
Fm1 = permute(S.FvFm.FvFm_per_well,[3 1 2]);
Fm2 = reshape(Fm1, size(S.FvFm.FvFm_per_well,3), [], 1);
S.FvFm.kinetics = Fm2;
S.FvFm.kineticsRescaled = Fm2./repmat(Fm2(1, :), size(S.FvFm.FvFm_per_well, 3), 1);
plot(t, Fm2./repmat(Fm2(1, :), size(S.FvFm.FvFm_per_well, 3), 1));
%plot(t, Fm2);
title('Kinetics per well')
ylabel('Saturation fluorescence relative to the first frame', 'FontSize', 18);
xlabel('frame no', 'FontSize', 18)
figure(gcf)



