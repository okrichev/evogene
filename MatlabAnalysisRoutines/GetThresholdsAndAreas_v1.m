function S = GetThresholdsAndAreas_v1(Img, MinLocationsRows, MinLocationsCols, varargin)
%
Neighbors = ParseInputs('Neighbors', 3, varargin); %number of neighbors for image opening operation

H = fspecial('disk', Neighbors);
dsk = strel('disk', Neighbors);

S.MinLocationsRows = MinLocationsRows;
S.MinLocationsCols = MinLocationsCols;

BW = zeros(size(Img));
for i = 1:(length(MinLocationsRows)-1),
    for j = 1:(length(MinLocationsCols)-1),
        Img_ROI = Img(MinLocationsRows(i):(MinLocationsRows(i+1)-1), ...
            MinLocationsCols(j):(MinLocationsCols(j+1)-1));
        level(i, j) = graythresh(Img_ROI/255);
        BW_ROI = im2bw(Img_ROI/255, level(i, j));
        BW_ROI = imopen(BW_ROI, dsk);
        BW(MinLocationsRows(i):(MinLocationsRows(i+1)-1), ...
            MinLocationsCols(j):(MinLocationsCols(j+1)-1)) = BW_ROI;
        area_pix(i, j) = sum(BW_ROI(:));
    end;
end;
S.threshold = level;
S.BW = BW;
S.area_pix = area_pix;
