function varargout = Manual_IR(varargin)
% MANUAL_IR MATLAB code for Manual_IR.fig
%      MANUAL_IR, by itself, creates a new MANUAL_IR or raises the existing
%      singleton*.
%
%      handles = MANUAL_IR returns the handle to a new MANUAL_IR or the handle to
%      the existing singleton*.
%
%      MANUAL_IR('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in MANUAL_IR.M with the given input arguments.
%
%      MANUAL_IR('Property','Value',...) creates a new MANUAL_IR or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before Manual_IR_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to Manual_IR_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help Manual_IR

% Last Modified by GUIDE v2.5 19-Jul-2018 11:54:54

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @Manual_IR_OpeningFcn, ...
                   'gui_OutputFcn',  @Manual_IR_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


%% --- Executes just before Manual_IR is made visible.
function Manual_IR_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to Manual_IR (see VARARGIN)

handles.tform.T = eye(3);
handles.tform.S = 1;
handles.tform.Tx = 0;
handles.tform.Ty = 0;
handles.tform.Theta = 0;


resetAxes(handles.axes1);
resetAxes(handles.axes2);
resetAxes(handles.axes3);

val = get(handles.MultispectralM,'Value');
if val == 1
    handles.Multisp = true;
else
    handles.Multisp = false;
end

% Choose default command line output for Manual_IR
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% UIWAIT makes Manual_IR wait for user response (see UIRESUME)
% uiwait(handles.figure1);


%% --- Outputs from this function are returned to the command line.
function varargout = Manual_IR_OutputFcn(hObject, eventdata, handles) 
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


%% --- Executes on button press in LoadIR.
function LoadIR_Callback(hObject, eventdata, handles)
% hObject    handle to LoadIR (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)


if handles.Multisp
    [f1,p1,index] = uigetfile({'*.tif','Tagged Image File (*.tif,)';...
        '*.*','All files (*.*)'},'Load Multispectral image');
    
    if index == 0
        return;
    end
    resetAxes(handles.axes1);
    IRimage = imread([p1 f1]);
    
    handles.Rawdata = IRimage;
    handles.IRimage = im2double(imadjust(IRimage));
    
    axes(handles.axes1);
    % set(handles.figure1,'CurrentAxes',handles.axes1);
    imshow(IRimage,[]);
    % title(f1);
    % Update handles structure
    guidata(hObject, handles);
    return;
end

[f1,p1,index] = uigetfile({'*.txt','Raw data (txt file)';...
    '*.png;*.jpg;*.bmp;*.tif','Supported images';...
    '*.png','Portable Network Graphics (*.png)';...
    '*.jpg','J-PEG (*.jpg)';...
    '*.bmp','Bitmap (*.bmp)';...
    '*.tif','Tagged Image File (*.tif,)';...
    '*.*','All files (*.*)'},'Load IR image');

if index == 0
    return;
end
resetAxes(handles.axes1);
[~,~,ext] = fileparts(f1);

if ext == '.txt'
    Rawdata = dlmread([p1 f1]);
    Imin = min(Rawdata(:));
    Imax = max(Rawdata(:));
    IRimage = (Rawdata - Imin)/(Imax - Imin);
    handles.Rawdata = Rawdata;
    handles.IRimage = imadjust(IRimage);
else
    IRimage = imread([p1 f1]);
    %     IRimage = IRimage(90:end-90,:,:);
    [~,~,c] = size(IRimage);
    if c == 1
        IRimage = gray2rgb(IRimage);
    end
end


handles.IRimage = IRimage;
axes(handles.axes1);
% set(handles.figure1,'CurrentAxes',handles.axes1);
imshow(IRimage,[]);
% title(f1);
% Update handles structure
guidata(hObject, handles);

%% --- Executes on button press in LoadRGB.
function LoadRGB_Callback(hObject, eventdata, handles)


if handles.Multisp
    [f1,p1,index] = uigetfile({'*.tif','Tagged Image File (*.tif,)';...
        '*.*','All files (*.*)'},'Load Multispectral image');
    
    if index == 0
        return;
    end
    resetAxes(handles.axes2);
    RGBimageC = imread([p1 f1]);
    handles.RGBimageC = im2double(imadjust(RGBimageC));
    
    axes(handles.axes2);
    % set(handles.figure1,'CurrentAxes',handles.axes1);
    imshow(RGBimageC,[]);
    % title(f1);
    
    % Update handles structure
    guidata(hObject, handles);
    return;
end


[f2,p2,index] = uigetfile({'*.png;*.jpg;*.bmp;*.tif','Supported images';...
    '*.png','Portable Network Graphics (*.png)';...
    '*.jpg','J-PEG (*.jpg)';...
    '*.bmp','Bitmap (*.bmp)';...
    '*.tif','Tagged Image File (*.tif,)';...
    '*.*','All files (*.*)'},'Load RGB image');

if index == 0
    return;
end
resetAxes(handles.axes2);
RGBimageC = imread([p2 f2]);

% RGBimageC = imrotate(RGBimageC,180);

axes(handles.axes2);
RGBimageC = im2double(RGBimageC);
handles.RGBimageC = RGBimageC;
imshow(RGBimageC);
% title(f2)
% Update handles structure
guidata(hObject, handles);


 
%% --- Executes on button press in R90_RGB.
function R90_RGB_Callback(hObject, eventdata, handles)
% hObject    handle to R90_RGB (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
handles.RGBimageC = imrotate(handles.RGBimageC,90);
axes(handles.axes2);
% set(handles.figure1,'CurrentAxes',handles.axes1);
imshow(handles.RGBimageC);
guidata(hObject, handles);


%% --- Executes on button press in R90_IR.
function R90_IR_Callback(hObject, eventdata, handles)
% hObject    handle to R90_IR (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
handles.IRimage = imrotate(handles.IRimage,90);
handles.Rawdata = imrotate(handles.Rawdata,90);
axes(handles.axes1);
% set(handles.figure1,'CurrentAxes',handles.axes1);
imshow(handles.IRimage,[]);
guidata(hObject, handles);

% --- Executes during object creation, after setting all properties.
function rot_CreateFcn(hObject, eventdata, handles)
% hObject    handle to rot (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end
% --- Executes during object creation, after setting all properties.
function xtran_CreateFcn(hObject, eventdata, handles)
% hObject    handle to ytran (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end
% --- Executes during object creation, after setting all properties.
function ytran_CreateFcn(hObject, eventdata, handles)
% hObject    handle to xtran (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


%% Translation
function xtran_Callback(hObject, eventdata, handles)
% hObject    handle to ytran (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of ytran as text
%        str2double(get(hObject,'String')) returns contents of ytran as a double
Tx = str2double(get(hObject,'String'));  
handles.tform.Tx = Tx;
handles.tform.T = pram2tform(handles);

% Update handles structure
guidata(hObject, handles);

function ytran_Callback(hObject, eventdata, handles)
% hObject    handle to xtran (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of xtran as text
%        str2double(get(hObject,'String')) returns contents of xtran as a double
Ty = str2double(get(hObject,'String'));  
handles.tform.Ty = Ty;
handles.tform.T = pram2tform(handles);

% Update handles structure
guidata(hObject, handles);



%% Scale
function Scale_Callback(hObject, eventdata, handles)
% hObject    handle to ytran (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of ytran as text
%        str2double(get(hObject,'String')) returns contents of ytran as a double
S = str2double(get(hObject,'String'));  
handles.tform.S = S;
handles.tform.T = pram2tform(handles);

% Update handles structure
guidata(hObject, handles);

% --- Executes during object creation, after setting all properties.
function Scale_CreateFcn(hObject, eventdata, handles)
% hObject    handle to ytran (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end

%% Rotate
function rot_Callback(hObject, eventdata, handles)
% hObject    handle to QuitButton (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

Theta = str2double(get(hObject,'String'));  %2*pi/10;
ch = get(handles.deg,'Value');
if ch == 1
    Theta = Theta*pi/180;
end

handles.tform.Theta = Theta;
handles.tform.T = pram2tform(handles);

% Update handles structure
guidata(hObject, handles);


% --- Executes on button press in deg.
function deg_Callback(hObject, eventdata, handles)
% hObject    handle to deg (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hint: get(hObject,'Value') returns toggle state of deg

%set(handles.rad,'Value',0);
ch = get(hObject,'Value');
Theta = handles.tform.Theta;
switch ch
    case 0
        set(handles.rad,'Value',1)
        set(handles.rot,'string',num2str(Theta));
        
    case 1
        set(handles.rad,'Value',0)
        Theta = Theta*180/pi;
        set(handles.rot,'string',num2str(Theta));
end

guidata(hObject, handles);
% --- Executes on button press in rad.
function rad_Callback(hObject, eventdata, handles)
% hObject    handle to rad (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hint: get(hObject,'Value') returns toggle state of rad

ch = get(hObject,'Value');
Theta = handles.tform.Theta;
switch ch
    case 0
        set(handles.deg,'Value',1)
        Theta = Theta*180/pi;
        set(handles.rot,'string',num2str(Theta));
    case 1
        set(handles.deg,'Value',0)
        set(handles.rot,'string',num2str(Theta));
end
guidata(hObject, handles);

%% select control points
% --- Executes on button press in ControlPoints.
function ControlPoints_Callback(hObject, eventdata, handles)
% hObject    handle to ControlPoints (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
set(handles.MessageText,'String',''); 
IRimageC = handles.IRimage;
RGBimageC = handles.RGBimageC;

if isfield(handles,'RGBpoints')
    [IRpoints, RGBpoints]  = cpselect(IRimageC, RGBimageC,...
        handles.IRpoints,handles.RGBpoints,'wait' ,true);
else
    [IRpoints, RGBpoints]  = cpselect(IRimageC, RGBimageC,...
        'wait' ,true);
end

if isempty(RGBpoints) || isempty(IRpoints)
    return;
end

handles.IRpoints = IRpoints;
handles.RGBpoints = RGBpoints;

[tform] = estimateGeometricTransform(...
    IRpoints,RGBpoints,'similarity');

% [tform2] = estimateGeometricTransform(...
%     IRpoints,RGBpoints,'projective');

handles.tform.T = tform.T;

[S,Theta,Tx,Ty] = tform2pram(handles);

handles.tform.S = S;
handles.tform.Theta = Theta;
handles.tform.Tx = Tx;
handles.tform.Ty = Ty;

axes(handles.axes1);
imshow(IRimageC);
hold on;
plot(IRpoints(:,1),IRpoints(:,2)...
    ,'x','LineWidth',2,'Color','yellow');

axes(handles.axes2);
imshow(RGBimageC);
hold on;
plot(RGBpoints(:,1),RGBpoints(:,2)...
    ,'x','LineWidth',2,'Color','yellow');


% Update handles structure
guidata(hObject, handles);

%% transform
% --- Executes on button press in Transform.
function Transform_Callback(hObject, eventdata, handles)
% hObject    handle to Transform (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
set(handles.MessageText,'String',''); 

hb(1)=num_check(handles.rot); % Rotation
hb(2)=num_check(handles.Scale); % Scale
hb(3)=num_check(handles.xtran); % Translation in x-direction
hb(4)=num_check(handles.ytran); % Translation in y-direction

% check all the inputs 
hc = sum(hb);

if hc == 0
    
    S = str2double(get(handles.Scale,'String'));
    Tx = str2double(get(handles.xtran,'String'));
    Ty = str2double(get(handles.ytran,'String'));
    Theta = str2double(get(handles.rot,'String'));  %2*pi/10;
    ch = get(handles.deg,'Value');
    if ch == 1
        Theta = Theta*pi/180;
    end
    
    handles.tform.S = S;
    handles.tform.Theta = Theta;
    handles.tform.Tx = Tx;
    handles.tform.Ty = Ty;
    
    
    
    if ~isfield(handles,'RGBimageC')||~isfield(handles,'IRimage')
        set(handles.MessageText,'String','Load 2 images to continue');
        return;
    end
    
    set(handles.MessageText,'String','Transformaing .... Please Wait');
    handles.tform.T = pram2tform(handles);
    
    tform2 = affine2d(handles.tform.T);
    
    IRimageC = gray2rgb(handles.IRimage);
%     IRimage = rgb2gray(IRimageC);
    RGBimageC = handles.RGBimageC;
    [~,~,c] = size(RGBimageC);
    if (c == 3)
        RGBimage = rgb2gray(RGBimageC);
    else
        RGBimage = RGBimageC;
    end
    
    % relate intrinsic and world coordinates
    RGBimageref = imref2d(size(RGBimage)); 
    % create image for display 
    IRregisteredC = imwarp(IRimageC,tform2,'OutputView',RGBimageref,'inter','cubic');
    
    % create the raw data registered !!!
    if isfield(handles,'Rawdata')
        handles.RawdataRegistered = imwarp(handles.Rawdata,tform2...
            ,'OutputView',RGBimageref,'inter','cubic');
    end

    % creat frame for display
    T = tform2.T;
    [m, n, ~] = size(IRimageC);
    s = max(n,m);
    oldCoo = [(m/2)/s (n/2)/s];
    newCoo = 2*s*T(1:2,1:2)*oldCoo'+ [T(3,2) T(3,1)]';
    [m, n, ~] = size(RGBimageC);
    if newCoo(1) > m
        newCoo(1) = m;
    end
    if newCoo(2) > n
        newCoo(2) = n;
    end
    
    Rs = 1; Cs = 1;
    
    if T(3,2) > 0
        Rs = round(T(3,2));
    end
    if T(3,1) > 0
        Cs = round(T(3,1));
    end
    newCoo = round(newCoo);
    handles.frame = zeros(size(RGBimageC));
    if handles.Multisp
        handles.frame(Rs:newCoo(1) ,Cs:newCoo(2)) = 1;
    else
        handles.frame(Rs:newCoo(1) ,Cs:newCoo(2) ,:) = 1;
    end
    
    
    % calculate the blended image 
    x = get(handles.Intensity,'value');
    handles.BlendImage = RGBimageC.*(1-x).*handles.frame +...
        IRregisteredC.*x.*handles.frame +...
        RGBimageC.*~handles.frame;
    handles.IRregisteredC = IRregisteredC;
    
    
    resetAxes(handles.axes3);
    %     imshowpair(RGBimageC,IRregisteredC,'blend');
    imshow(handles.BlendImage);
    title('Blend Image');
    set(handles.MessageText,'String','Done!');
    guidata(hObject, handles);
else
    set(handles.MessageText,'String','Invalid value: enter a real number only');
    
end


%% help function 
function T = pram2tform(handles)

S = handles.tform.S;
Theta = handles.tform.Theta;
Tx = handles.tform.Tx;
Ty = handles.tform.Ty;

TT=S*[cos(Theta) -sin(Theta); sin(Theta) cos(Theta)];
T = eye(3);
T(1:2,1:2) = TT;
T(3,1) = Tx;
T(3,2) = Ty;


function [S,Theta,Tx,Ty] = tform2pram(handles)

TT = handles.tform.T;

Tx = TT(3,1);
Ty = TT(3,2);
Theta = atan(TT(2,1)/TT(1,1)); % theta in rad
S = TT(1,1)/cos(Theta);

set(handles.Scale,'string',num2str(S));
set(handles.xtran,'string',num2str(Tx));
set(handles.ytran,'string',num2str(Ty));
ch = get(handles.deg,'Value');
if ch == 1
    set(handles.rot,'string',num2str(Theta*180/pi));  %2*pi/10;
end

    
function hb = num_check(ha)
% This function checks the validation 
% of the entered transformation parameters
% ha is the handle to be checked
% hb is a flag, 1 for correct range, 0 for incorrect range.
h = guidata(gcbo);
ba = get(ha,'String');
bb = str2double(ba);
hb = 0;
if isempty(bb) || sum(ba == 'i') || sum(ba == 'j') || isnan(bb) 
    axes(h.axes2);
    cla;
    set(h.MessageText,'String','Invalid value: enter a real number only');
    if ha == h.Scale
        set(ha,'String','1');
    else
        set(ha,'String','0');
    end
    
 	hb = 1;
end


% --- Executes on slider movement.
function Intensity_Callback(hObject, eventdata, handles)
% hObject    handle to Intensity (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'Value') returns position of slider
%        get(hObject,'Min') and get(hObject,'Max') to determine range of slider
set(handles.MessageText,'String',''); 

if ~isfield(handles,'IRregisteredC')
    return;
end

x = get(handles.Intensity,'value');
RGBimageC = handles.RGBimageC;
IRregisteredC = handles.IRregisteredC;
frame = handles.frame;

handles.BlendImage = RGBimageC.*(1-x).*frame +...
    IRregisteredC.*x.*frame + RGBimageC.*~frame;

resetAxes(handles.axes3);

imshow(handles.BlendImage);

guidata(hObject, handles);


% --- Executes during object creation, after setting all properties.
function Intensity_CreateFcn(hObject, eventdata, handles)
% hObject    handle to Intensity (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: slider controls usually have a light gray background.
if isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor',[.9 .9 .9]);
end

set(hObject,'value',0.5);
guidata(hObject, handles);


% --- Executes on button press in Restore.
function Restore_Callback(hObject, eventdata, handles)
% hObject    handle to Restore (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
if ~isfield(handles,'IRpoints')
    set(handles.MessageText,'String','Select control points to restore transformation');
    return;
end

[tform] = estimateGeometricTransform(...
    handles.IRpoints,handles.RGBpoints,'similarity');
handles.tform.T = tform.T;

[S,Theta,Tx,Ty] = tform2pram(handles);

handles.tform.S = S;
handles.tform.Theta = Theta;
handles.tform.Tx = Tx;
handles.tform.Ty = Ty;



guidata(hObject, handles);


% --- Executes on button press in SaveTform.
function SaveTform_Callback(hObject, eventdata, handles)
% hObject    handle to SaveTform (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
[file,path,indx] = uiputfile({'*.txt'});
if indx == 0
    return;
end

T = handles.tform.T;
fileID = fopen([path file],'w');

if fileID == -1
    set(handles.MessageText,'String',['Can''t open file ''' file '']); 
end
fprintf(fileID,'%f %f %f %f %f %f',...
    T(1,1),T(1,2),T(3,1),T(2,1),T(2,2),T(3,2));
fclose(fileID);
guidata(hObject, handles);

% --- Executes on button press in LoadTform.
function LoadTform_Callback(hObject, eventdata, handles)
% hObject    handle to LoadTform (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
[file,path,indx] = uigetfile({'*.txt'});
if indx == 0
    return;
end


fileID = fopen([path file],'r');
if fileID == -1
    set(handles.MessageText,'String',['Can''t open file ''' file '']); 
end

formatSpec = '%f';
A = fscanf(fileID,formatSpec);
fclose(fileID);

TT = zeros(3,2);
TT(1,1:2) = A(1:2);
TT(2,1:2) = A(4:5);
TT(3,1) = A(3);
TT(3,2) = A(6);
handles.tform.T = TT;

[S,Theta,Tx,Ty] = tform2pram(handles);

handles.tform.S = S;
handles.tform.Theta = Theta;
handles.tform.Tx = Tx;
handles.tform.Ty = Ty;

guidata(hObject, handles);

function rgb = gray2rgb(gray)
rgb = repmat(gray,[1 1 3]);




% --- Executes on button press in SaveMfile.
function SaveMfile_Callback(hObject, eventdata, handles)
% hObject    handle to SaveMfile (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
set(handles.MessageText,'String',''); 

[file, path, index] = uiputfile({'*.mat','mat'},'Save workcpace data');
if index == 0
    return;
end

if ~isfield(handles,'RawdataRegistered')
    set(handles.MessageText,'String',['apply transformation ' ...
        'before attempting to save mat file']);
    return;
end

if isfield(handles,'IRpoints')
    IRpoints = handles.IRpoints;
    RGBpoints = handles.RGBpoints;
else
    IRpoints = [];
    RGBpoints = [];
end
tform = handles.tform;
Rawdata = handles.Rawdata;
RGBimageC = handles.RGBimageC;
RawdataRegistered = handles.RawdataRegistered;

if file~=0
    save([path file],'tform','Rawdata','RGBimageC',...
        'RawdataRegistered','IRpoints','RGBpoints');
else
    set(handles.MessageText,'String','can''t save the file');
end
guidata(hObject, handles);

% --- Executes on button press in SaveTxt.
function SaveTxt_Callback(hObject, eventdata, handles)
% hObject    handle to SaveTxt (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
set(handles.MessageText,'String',''); 

if ~isfield(handles,'RawdataRegistered')
    set(handles.MessageText,'String',['apply transformation ' ...
        'before attempting to save raw data file']);
    return;
end

[file, path, index] = uiputfile({'*.txt','txt'},'Save Raw data file');
if index == 0
    return;
end

if file~=0
    dlmwrite([path file], handles.RawdataRegistered);
else
    set(handles.MessageText,'String','can''t save the file');
end
guidata(hObject, handles);


% --- Executes on button press in SaveImage.
function SaveImage_Callback(hObject, eventdata, handles)
% hObject    handle to SaveImage (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
set(handles.MessageText,'String',''); 

if ~isfield(handles,'BlendImage')
    set(h.MessageText,'String',['apply transformation ' ...
        'before attempting to save registered image']);
    return;
end

[file, path, index] = uiputfile({'*.tif','tif';'*.jpg','jpg'},'Save Image');
if index == 0
    return;
end

if file~=0
    imwrite(handles.BlendImage,[path file]);
else
    set(handles.MessageText,'String','can''t save the file');
end
guidata(hObject, handles);

% --- Executes on button press in QuitButton.
function QuitButton_Callback(hObject, eventdata, handles)
% hObject    handle to QuitButton (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
close();


% --- Executes on button press in MultispectralM.
function MultispectralM_Callback(hObject, eventdata, handles)
% hObject    handle to MultispectralM (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hint: get(hObject,'Value') returns toggle state of MultispectralM
val = get(hObject,'Value');
if val == 1
    handles.Multisp = true;
else
    handles.Multisp = false;
end
guidata(hObject, handles);

function resetAxes(ax)
axes(ax);
cla reset
set(ax,'Box','on');
set(ax,'XTick',[]);
set(ax,'YTick',[]);
