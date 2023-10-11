using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyScheduleBuilderWPF
{
    internal class CellStyleManager
    {
        private IWorkbook workbook;

        private ICellStyle borders;
        private ICellStyle algins;
        private ICellStyle unsetAlgin;

        private ICellStyle black;
        private ICellStyle white;

        private ICellStyle sanitaryDay;

        public CellStyleManager(IWorkbook workbook)
        {
            this.workbook = workbook;
            borders = setBorders();
            algins = setlAlgins();
            unsetAlgin = unsetHorizontalAlgin();
            black = setBlack();
            white = setWhite();
            sanitaryDay = setSanitary();
        }


        private ICellStyle setSanitary()
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.CloneStyleFrom(algins);
            style.Rotation = 90;

            IFont font = workbook.CreateFont();
            font.IsBold = true;
            font.IsItalic = true;
            font.FontName = "Times New Roman";
            font.FontHeightInPoints = 12;
            style.SetFont(font);

            return style;
        }

        private ICellStyle unsetHorizontalAlgin()
        {
            var style = workbook.CreateCellStyle();
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            return style;
        }
        private ICellStyle setlAlgins()
        {
            var style = workbook.CreateCellStyle();
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            style.WrapText = true;
            return style;
        }
        private ICellStyle setBorders()
        {
            var style = workbook.CreateCellStyle();
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            // Установите цвет границы в черный
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.TopBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;

            return style;
        }

        private ICellStyle setBlack()
        {
            ICellStyle blackStyle = workbook.CreateCellStyle();
            blackStyle.FillForegroundColor = IndexedColors.Black.Index;
            blackStyle.FillPattern = FillPattern.SolidForeground;
            return blackStyle;
        }
        private ICellStyle setWhite()
        {
            ICellStyle whiteStyle = workbook.CreateCellStyle();
            whiteStyle.FillForegroundColor = IndexedColors.White.Index;
            whiteStyle.FillPattern = FillPattern.SolidForeground;
            whiteStyle.CloneStyleFrom(borders);
            return whiteStyle;
        }

        public void SetWhite(ICell cell) => cell.CellStyle = white;
        public void SetBlack(ICell cell) => cell.CellStyle = black;
        public void setBorders(ICell cell) => cell.CellStyle = borders;
        public void setSanitary(ICell cell) => cell.CellStyle = sanitaryDay;
        public void SetCustomCellStyle(
            ICell cell,

            short fontSize,
            string fontName = "Times New Roman",
            bool isBold = true,
            bool underline = false,

            bool borders = true,
            bool horizontalAlgin = true
        )
        {
            var style = workbook.CreateCellStyle();

            style.CloneStyleFrom(algins);

            if (!horizontalAlgin)
                style.CloneStyleFrom(this.unsetAlgin);
            if (borders)
                style.CloneStyleFrom(this.borders);

            IFont font = workbook.CreateFont();
            font.FontName = fontName; 
            font.FontHeightInPoints = fontSize; 
            font.IsBold = isBold;
            font.Underline = underline ? FontUnderlineType.Single : FontUnderlineType.None;
            style.SetFont(font);

            cell.CellStyle = style;


        }
    }
}
