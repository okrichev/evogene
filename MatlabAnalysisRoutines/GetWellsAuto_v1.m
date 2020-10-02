function [status, MinLocationsCols, MinLocationsRows, waveLen, wrnings] = GetWellsAuto_v1(Img, varargin)
% automatically finds the location of wells
% varargin in couples
% DishSize | [rows, columns] (8x12) default
% 
DishSize = ParseInputs('DishSize', [8, 12], varargin);
MaxPix = ParseInputs('MaxPix', 255, varargin);
minWellSizeFractionOfMedian = ParseInputs('Min Well Size', 0.7, varargin);
wrnings = {};

% find wells automaticall fft
Img = Img;
sumY = sum(Img);
CCDwidth = length(sumY);
fftSumY = fft(sumY);

% find max of fft abs value and the phase there
% Do not stray more than by 30% from the expected value of DishSize(2)
% corresponding to well distances of
% CCDwidth/DishSize(2)
safeFactor = 1.3;
start = 1+ round(DishSize(2)/safeFactor);
stop = 1+ round(DishSize(2)*safeFactor);
[temp, maxJ] = max(abs(fftSumY(start:stop)));
waveLen = CCDwidth/(maxJ+(start-1)-1);
FirstMax = -angle(fftSumY(maxJ+(start-1)))*waveLen/2/pi;
if FirstMax < 0,
    FirstMax = FirstMax + waveLen;
end;

MaxLocationsCols = round(FirstMax:waveLen:CCDwidth)+1;
% expect DishSize(2) (i.e. 12) maxima. If have more than 12, then cut out the
% lowest maxima on the edges
% while (length(MaxLocationsCols) > DishSize(2)),
%     if (sumY(MaxLocationsCols(1)) > sumY(MaxLocationsCols(end))),
%         MaxLocationsCols(end) = [];
%     else
%         MaxLocationsCols(1) = [];
%     end
% end
% add camera edges
% DO THIS AT A LATER STEP 20.07.17.
MaxLocationsCols = [1 MaxLocationsCols CCDwidth];


%MinLocationsCols = round(FirstMin:waveLen:CCDwidth)+1;
MinLocationsCols = [];
% look for minimum in between each maximum
for i = 1:(length(MaxLocationsCols)-1),
    [temp, minJ] = min(sumY(MaxLocationsCols(i):MaxLocationsCols(i+1)));
    MinLocationsCols(i) = MaxLocationsCols(i) - 1 + minJ;
end;

% expect DishSize(2) (i.e. 12) maxima. If have more than 12, then
% find and delete incomplete wells
wellSizes = diff(MinLocationsCols);
MedianWellSize = median(wellSizes);
if (wellSizes(1) < minWellSizeFractionOfMedian*MedianWellSize),
    MinLocationsCols(1) = [];
end
if (wellSizes(end) < minWellSizeFractionOfMedian*MedianWellSize),
    MinLocationsCols(end) = [];
end

% remove spare dark space if any
while (length(MinLocationsCols) > (DishSize(2)+1)),
    if (sum(sumY(MinLocationsCols(1):MinLocationsCols(2))) > sum(sumY(MinLocationsCols(end-1):MinLocationsCols(end)))),
        MinLocationsCols(end) = [];
    else
        MinLocationsCols(1) = [];
    end
end


subplot(1, 2, 1)
plot(sumY);
hold on;
plot(MinLocationsCols, sumY(MinLocationsCols), 'ro');
hold off;
figure(gcf)

%MinLocationsCols = [1 MinLocationsCols]; %the last column appears to be empty so not adding the last line
%MinLocationsCols = [(MinLocationsCols(1)-waveLen) MinLocationsCols 

% repeat the procedure for the other axis
sumX = sum(Img, 2);

CCDheight = length(sumX);
fftSumX = fft(sumX);

% find max of fft abs value and the phase there
start = 1+ round(DishSize(1)/safeFactor);
stop = 1+ round(DishSize(1)*safeFactor);

[temp, maxJ] = max(abs(fftSumX(start:round(CCDheight/2))));
waveLen = CCDheight/(maxJ+(start-1)-1);
FirstMax = -angle(fftSumX(maxJ+(start-1)))*waveLen/2/pi;
if FirstMax < 0,
    FirstMax = FirstMax + waveLen;
end;

MaxLocationsRows = round(FirstMax:waveLen:CCDheight)+1;
% expect DishSize(1) (i.e. 8) maxima. If have more than 8, then cut out the
% lowest maxima on the edges
% while (length(MaxLocationsRows) > DishSize(1)),
%     if (sumX(MaxLocationsRows(1)) > sumX(MaxLocationsRows(end))),
%         MaxLocationsRows(end) = [];
%     else
%         MaxLocationsRows(1) = [];
%     end
% end
% add camera edges
MaxLocationsRows = [1 MaxLocationsRows CCDheight];

MinLocationsRows = [];
% look for minimum in between each maximum
for i = 1:(length(MaxLocationsRows)-1),
    [temp, minJ] = min(sumX(MaxLocationsRows(i):MaxLocationsRows(i+1)));
    MinLocationsRows(i) = MaxLocationsRows(i) - 1 + minJ;
end;

% expect DishSize(1) (i.e. 8) maxima. If have more than 12, then
% find and delete incomplete wells
wellSizes = diff(MinLocationsRows);
MedianWellSize = median(wellSizes);
if (wellSizes(1) < minWellSizeFractionOfMedian*MedianWellSize),
    MinLocationsRows(1) = [];
end
if (wellSizes(end) < minWellSizeFractionOfMedian*MedianWellSize),
    MinLocationsRows(end) = [];
end

% remove spare dark space at the top
while (length(MinLocationsRows) > (DishSize(1)+1)),
    if (sum(sumX(MinLocationsRows(1):MinLocationsRows(2))) > sum(sumX(MinLocationsRows(end-1):MinLocationsRows(end)))),
        MinLocationsRows(end) = [];
    else
        MinLocationsRows(1) = [];
    end
end

subplot(1, 2, 2)
plot(sumX);
hold on;
plot(MinLocationsRows, sumX(MinLocationsRows), 'ro');
hold off;
figure(gcf)

% show in the image
figure('Position', [123    56   981   649]);

% Imggrid(:, :, 1) = zeros(size(Img));
% Imggrid(:, :, 2) = Img;
% Imggrid(:, :, 3) = zeros(size(Img));
% Imggrid(MinLocationsRows, :, 1) = 255;
% Imggrid(:, MinLocationsCols, 1) = 255;
% Imggrid(MinLocationsRows+1, :, 1) = 255; %to emphasize on scaled figures
% Imggrid(:, MinLocationsCols+1, 1) = 255;
% 
% imagesc(Imggrid/255);
% %axis image;
% zoom on;
% title('Zoom into the relevant wells and press any key', 'FontSize', 20);
% pause;
% v = axis;
% J = find((MinLocationsRows > v(3)) & (MinLocationsRows < v(4)));
% minJ = max((min(J)-1), 1);
% maxJ = min((max(J)+1), length(MinLocationsRows));
% MinLocationsRows = MinLocationsRows(minJ:maxJ);
% 
% J = find((MinLocationsCols > v(1)) & (MinLocationsCols < v(2)));
% minJ = max((min(J)-1), 1);
% maxJ = min((max(J)+1), length(MinLocationsCols));
% MinLocationsCols = MinLocationsCols(minJ:maxJ);

% show in the image

clear Imggrid;
Imggrid(:, :, 1) = zeros(size(Img));
Imggrid(:, :, 2) = Img;
Imggrid(:, :, 3) = zeros(size(Img));
Imggrid(MinLocationsRows, MinLocationsCols(1):MinLocationsCols(end) , 1) = MaxPix;
Imggrid(MinLocationsRows(1):MinLocationsRows(end), MinLocationsCols, 1) = MaxPix;
Imggrid(MinLocationsRows+1, MinLocationsCols(1):MinLocationsCols(end), 1) = MaxPix; %to emphasize on scaled figures
Imggrid(MinLocationsRows(1):MinLocationsRows(end), MinLocationsCols+1, 1) = MaxPix;

imagesc(Imggrid/MaxPix);
axis image;
figure(gcf)
if ((length(MinLocationsRows) ~= (DishSize(1)+1)) | (length(MinLocationsCols) ~= (DishSize(2)+1))),
   status = false;
   warning('Automated finding of wells failed! Using standard settings.');
   wrnings = [wrnings 'Automated finding of wells failed! Using standard settings.'];
else
    status = true;
    display('Found wells successfully')
end;

   





