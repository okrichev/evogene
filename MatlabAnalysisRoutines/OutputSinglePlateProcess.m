function OutputSinglePlateProcess(S, OutputFpath)
mkdir(OutputFpath);
close all;

% show split into wells
figure('Position', [123    56   981   649]);
Imggrid(:, :, 1) = zeros(size(S.ImgDark));
Imggrid(:, :, 2) = S.ImgDark/max(S.ImgDark(:));
Imggrid(:, :, 3) = zeros(size(S.ImgDark));
Imggrid(S.MinLocationsRows, S.MinLocationsCols(1):S.MinLocationsCols(end) , 1) = 1;
Imggrid(S.MinLocationsRows(1):S.MinLocationsRows(end), S.MinLocationsCols, 1) = 1;
Imggrid(S.MinLocationsRows(1:(end-1))+1, S.MinLocationsCols(1):S.MinLocationsCols(end), 1) = 1; %to emphasize on scaled figures
Imggrid(S.MinLocationsRows(1):S.MinLocationsRows(end), S.MinLocationsCols(1:(end-1))+1, 1) = 1;

imagesc(Imggrid);
axis image;
set(gca, 'FontSize', 16, 'YTick', (S.MinLocationsRows(1:(end-1))+S.MinLocationsRows(2:end))/2, 'YTickLabel', {'A'; 'B'; 'C'; 'D'; 'E'; 'F'; 'G'; 'H'}, ...
    'XTick', (S.MinLocationsCols(1:(end-1))+S.MinLocationsCols(2:end))/2, 'XTickLabel', {'1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'});
figure(gcf)
set(gcf, 'PaperPositionMode', 'auto')
saveas(gcf, [OutputFpath genvarname(S.plateName) '_DarkImageWells.jpg']);


% show split into wells after thresholding
figure('Position', [123    56   981   649]);
Imggrid(:, :, 1) = zeros(size(S.ImgDark));
Imggrid(:, :, 2) = S.ImgDark.*S.BW/max(S.ImgDark(:));
Imggrid(:, :, 3) = zeros(size(S.ImgDark));
Imggrid(S.MinLocationsRows, S.MinLocationsCols(1):S.MinLocationsCols(end) , 1) = 1;
Imggrid(S.MinLocationsRows(1):S.MinLocationsRows(end), S.MinLocationsCols, 1) = 1;
Imggrid(S.MinLocationsRows+1, S.MinLocationsCols(1):S.MinLocationsCols(end), 1) = 1; %to emphasize on scaled figures
Imggrid(S.MinLocationsRows(1):S.MinLocationsRows(end), S.MinLocationsCols+1, 1) = 1;

imagesc(Imggrid);
axis image;
set(gca, 'FontSize', 16, 'YTick', (S.MinLocationsRows(1:(end-1))+S.MinLocationsRows(2:end))/2, 'YTickLabel', {'A'; 'B'; 'C'; 'D'; 'E'; 'F'; 'G'; 'H'}, ...
    'XTick', (S.MinLocationsCols(1:(end-1))+S.MinLocationsCols(2:end))/2, 'XTickLabel', {'1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'});
figure(gcf)
set(gcf, 'PaperPositionMode', 'auto')
saveas(gcf, [OutputFpath genvarname(S.plateName) '_DarkImageWellsThresholded.jpg']);



% use first FvFm measurement for presentation
figure('Position', [50    56   1000   649]);
J = 1;
imagesc(S.FvFm.FvFm_per_well(:, :, J)); 
colorbar;figure(gcf); set(gca, 'FontSize', 16)
v = caxis;
title('FvFm per well', 'FontSize', 18)
for i = 1:size(S.FvFm.FvFm_per_well, 1),
    for j = 1:size(S.FvFm.FvFm_per_well, 2),
       %text(j-0.3, i, num2str(FvFm_150(i,j), 2), 'FontSize', 16, 'Color', 'white');
       text(j-0.3, i, num2str(S.FvFm.FvFm_per_well(i,j, J), 2), 'FontSize', 16);
       if (S.FvFm.FvFm_per_well(i,j, J)> (v(1) + 2/3*(v(2) - v(1)))) | (S.FvFm.FvFm_per_well(i,j, J)< (v(1) + 1/3*(v(2) - v(1))))
           text(j-0.3, i, num2str(S.FvFm.FvFm_per_well(i,j, J), 2), 'FontSize', 16, 'Color', 'white');
       end;
    end;
end;
set(gca, 'FontSize', 16, 'YTickLabel', {'A'; 'B'; 'C'; 'D'; 'E'; 'F'; 'G'; 'H'}, ...
    'XTick', 1:12);
figure(gcf)
set(gcf, 'PaperPositionMode', 'auto')
saveas(gcf, [OutputFpath genvarname(S.plateName) '_FvFm per well.jpg']);

% FvFm image
figure('Position', [50    56   1000   649]);
J = 1;
imagesc(S.FvFm.FvFm_Image(:, :, J)); 
axis image;
set(gca, 'FontSize', 16, 'YTick', (S.MinLocationsRows(1:(end-1))+S.MinLocationsRows(2:end))/2, 'YTickLabel', {'A'; 'B'; 'C'; 'D'; 'E'; 'F'; 'G'; 'H'}, ...
    'XTick', (S.MinLocationsCols(1:(end-1))+S.MinLocationsCols(2:end))/2, 'XTickLabel', {'1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'});

colorbar;figure(gcf); set(gca, 'FontSize', 16);
set(gcf, 'PaperPositionMode', 'auto')
saveas(gcf, [OutputFpath genvarname(S.plateName) '_FvFm image.jpg']);

% save structure
eval([genvarname(S.plateName) ' = S;']);
save([OutputFpath 'processedData.mat'], 'S', genvarname(S.plateName));

% prepare and save coma separated file
ExcelFname = [genvarname(S.plateName) '_processedData.csv'];
ColumnNames = 'PLATE_ID, ROW, COLUMN, WELL_ID, TP_ID, Area, FvFm, EXCLUDED, REMARKS';
RowNames = {'A'; 'B'; 'C'; 'D'; 'E'; 'F'; 'G'; 'H'};
fid = fopen([OutputFpath ExcelFname],'w');
for i = 1:length(S.warnings),
    fprintf(fid, '%s\r\n', S.warnings{i});
end
fprintf(fid, '%s\r\n', ColumnNames);

for i = 1:size(S.area_mm, 1),
    for j = 1:size(S.area_mm, 2),
        fprintf(fid, '%s, %s, %d, %d, , %g, %g, , \r\n', S.plateName, RowNames{i}, j, ...
            j+12*(i-1), S.area_mm(i, j), S.FvFm.FvFm_per_well(i, j));
    end;
end;
fclose(fid)
