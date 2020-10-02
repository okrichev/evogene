function FvFm_data = AnalyzeFvFm_v1(ImgDark, ImgSat, ThresholdStructure)

Neighbors =  ThresholdStructure.Neighbors;
H = fspecial('disk', Neighbors);
ImgRatioScaling = ThresholdStructure.ImgRatioScaling;

Fm_to_Fo_Image = ImgRatioScaling*imfilter(ImgSat, H)./imfilter(ImgDark, H);
FvFm_Image = 1 -1./Fm_to_Fo_Image;
FvFm_Image(~ThresholdStructure.BW) = NaN;


ImgDark = ImgDark.*ThresholdStructure.BW;
ImgSat = ImgSat.*ThresholdStructure.BW;
MinLocationsRows = ThresholdStructure.MinLocationsRows;
MinLocationsCols = ThresholdStructure.MinLocationsCols;
MaxPix = ThresholdStructure.MaxPix;

for i = 1:(length(MinLocationsRows)-1),
    for j = 1:(length(MinLocationsCols)-1),
        ImgDark_ROI = ImgDark(MinLocationsRows(i):(MinLocationsRows(i+1)-1), ...
            MinLocationsCols(j):(MinLocationsCols(j+1)-1));
        ImgSat_ROI = ImgSat(MinLocationsRows(i):(MinLocationsRows(i+1)-1), ...
            MinLocationsCols(j):(MinLocationsCols(j+1)-1));
        Fm_to_Fo(i, j) = ImgRatioScaling*mean(ImgSat_ROI(:))./mean(ImgDark_ROI(:));
        FvFm(i, j) = 1 - 1./Fm_to_Fo(i, j); 
        frac_satPix(i, j) = sum(ImgSat_ROI(:)==MaxPix)/ThresholdStructure.area_pix(i, j);
    end;
end;

FvFm_data.Image = FvFm_Image;
FvFm_data.FvFm = FvFm;
FvFm_data.frac_satPix = frac_satPix; 

imagesc(FvFm_Image);
colorbar;
set(gca, 'FontSize', 16)
figure(gcf)
