addpath(genpath('/Users/oleg/Documents/Evogene/MatlabAnalysisRoutines'));
set(groot, 'DefaultAxesFontSize', 16)
%% Experiment file path
ExperimentFpath = '/Users/oleg/Documents/Evogene/Example/'

%% this processes one plate: finds wells automatically. If automatic find fails it will use standard
% settings for wells
PlateFpath = 'plate#8/';
SP215R1 = ProcessFluorImagerData_v1([ExperimentFpath PlateFpath]);

%%
PlateFpath = 'Plate#SP215.R2/';
SP215R2 = ProcessFluorImagerData_v1([ExperimentFpath PlateFpath]);


%% this processes one plate: uses the wells definition from another measurement
PlateFpath = 'Plate#SP215.R2/';
SP215R2 = ProcessFluorImagerData_v1([ExperimentFpath PlateFpath], 'Grid options', 'user-defined', ...
    'User grid', {SP215R1.MinLocationsCols, SP215R1.MinLocationsRows});

%% this uses default grid
PlateFpath = 'Plate#SP215.R3/';
SP215R3 = ProcessFluorImagerData_v1([ExperimentFpath PlateFpath], 'Grid options', 'standard');

%% process the whole batch now
foldnames = dir(ExperimentFpath);
for i = 1:length(foldnames),
    if (foldnames(i).isdir)&(~strcmp(foldnames(i).name, '.'))&(~strcmp(foldnames(i).name, '..')),
        PlateFpath = [foldnames(i).name filesep];
         display(['Processing ' foldnames(i).name])
        S = ProcessFluorImagerData_v1([ExperimentFpath PlateFpath]);
    end;
end;

