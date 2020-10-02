function S = GetThresholdsAndAreas_v2(Sin, varargin)
%

Neighbors = ParseInputs('Neighbors', Sin.Neighbors, varargin); %number of neighbors for image opening operation
MinThreshold = ParseInputs('MinThresh', [], varargin); % [] for automatic determination

H = fspecial('disk', Neighbors);
dsk = strel('disk', Neighbors);

S = Sin;
S.Neighbors = Neighbors;
MinLocationsRows = S.MinLocationsRows;
MinLocationsCols = S.MinLocationsCols;
MaxPix = S.MaxPix;

Img = S.ImgDark;

if isempty(MinThreshold),
    % automatic detection of minimal threshold: take half of global
    % threshold
    MinThreshold = 0.5*graythresh(Img/max(Img(:)));
end

% determine pixel size in mm assuming distance between wells to be 9 mm (for 96 well plate)

S.scale_mm_per_pix = 9/median([diff(MinLocationsRows) diff(MinLocationsCols)]);

BW = zeros(size(Img));
for i = 1:(length(MinLocationsRows)-1),
    for j = 1:(length(MinLocationsCols)-1),
        Img_ROI = Img(MinLocationsRows(i):(MinLocationsRows(i+1)-1), ...
            MinLocationsCols(j):(MinLocationsCols(j+1)-1));
        saturatedPix(i, j) = sum(Img_ROI(:) == MaxPix);
        level(i, j) = max(graythresh(Img_ROI/MaxPix), MinThreshold);
        BW_ROI = im2bw(Img_ROI/MaxPix, level(i, j));
        BW_ROI = imopen(BW_ROI, dsk);
        BW(MinLocationsRows(i):(MinLocationsRows(i+1)-1), ...
            MinLocationsCols(j):(MinLocationsCols(j+1)-1)) = BW_ROI;
        area_pix(i, j) = sum(BW_ROI(:));
    end;
end;
S.threshold = level;
S.BW = BW;
S.area_pix = area_pix;
S.area_mm = area_pix*S.scale_mm_per_pix^2;
S.saturatedPix = saturatedPix;
S.frac_satPix = saturatedPix./area_pix;
