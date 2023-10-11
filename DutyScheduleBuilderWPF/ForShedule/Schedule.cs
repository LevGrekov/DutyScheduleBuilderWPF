using DutyScheduleBuilderWPF.Entities;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DutyScheduleBuilderWPF
{
    internal class Schedule
    {
        private IWorkbook workbook;
        private ISheet sheet;
        private Kitchen kitchen;
        private CellStyleManager CellStyleManager;

        private int endColumn;
        private int endRow;

        private int tableSize = 14;

        private WeekDay generalDay;
        private DateOnly datetime;

        private int lastDutyNum;

        private bool?[,] dutyTable;

        public Schedule(Kitchen kitchen, DateOnly dateTime, WeekDay generalDay, int lastDutyNum, int endRow = 30)
        {
            workbook = new XSSFWorkbook();
            CellStyleManager = new CellStyleManager(workbook);

            sheet = workbook.CreateSheet($"{kitchen.Id}a");
            this.kitchen = kitchen;
            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            endColumn = daysInMonth + 2;
            this.endRow = endRow;
            this.datetime = dateTime;
            this.generalDay = generalDay;

            if (lastDutyNum > kitchen.Students.Count) this.lastDutyNum = kitchen.Students.Count;

            else this.lastDutyNum = lastDutyNum - 1;

            dutyTable = new bool?[kitchen.Students.Count, daysInMonth];
            for (int i = 0; i < kitchen.Students.Count; i++)
            {
                for (int j = 0; j < daysInMonth; j++)
                {
                    dutyTable[i, j] = false;
                }
            }

            for (int rowIndex = 0; rowIndex <= endRow; rowIndex++)
            {
                for (int colIndex = 0; colIndex <= endColumn; colIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                    ICell cell = row.GetCell(colIndex) ?? row.CreateCell(colIndex);

                    if (rowIndex != 0 && rowIndex < tableSize)
                    {
                        if (colIndex == 2 && rowIndex < tableSize && rowIndex > 3)
                        {
                            CellStyleManager.SetCustomCellStyle(cell, 12, horizontalAlgin: false);
                        }
                        else CellStyleManager.SetCustomCellStyle(cell, 12);
                    }
                }
            }

            CreateTemplate();
            LoadPeople();
            SetWidthAndHight();
            SetPageLyout();
            SetSanitaryDays();
            RealizeDutyAlgoritm2();

        }

        private bool? this[int i, int j]
        {
            get
            {
                if (i < 0 || i > dutyTable.GetLength(0) || j < 0 || j > dutyTable.GetLength(1))
                    throw new IndexOutOfRangeException(nameof(j));
                if (IsCurrentDayIsGeneral(j + 1))
                    return null;

                return dutyTable[i, j];
            }
            set
            {
                if (i < 0 || i > dutyTable.GetLength(0) || j < 0 || j > dutyTable.GetLength(1))
                    throw new IndexOutOfRangeException(nameof(i));
                if (IsCurrentDayIsGeneral(j + 1))
                    throw new Exception(nameof(j) + "Санитарный день");
                if (value == null)
                    throw new Exception("Нельзя присвоить null не санитарному дню");

                dutyTable[i, j] = value;
                var CurrCell = sheet.GetRow(i + 4).GetCell(j + 3);
                CellStyleManager.SetBlack(CurrCell);

            }
        }
        private void CreateTemplate()
        {

            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, endColumn));
            ICell mergedCell = sheet.GetRow(0).GetCell(0);
            mergedCell.SetCellValue($"График дежурств на кухне № {kitchen.Id}a");
            CellStyleManager.SetCustomCellStyle(mergedCell, 26, borders: false);

            sheet.AddMergedRegion(new CellRangeAddress(1, 3, 0, 0));
            mergedCell = sheet.GetRow(1).GetCell(0);
            mergedCell.SetCellValue("№ п/п");
            CellStyleManager.SetCustomCellStyle(mergedCell, 12);

            sheet.AddMergedRegion(new CellRangeAddress(1, 3, 1, 1));
            mergedCell = sheet.GetRow(1).GetCell(1);
            mergedCell.SetCellValue("№ комнаты");
            CellStyleManager.SetCustomCellStyle(mergedCell, 10);

            sheet.AddMergedRegion(new CellRangeAddress(1, 3, 2, 2));
            mergedCell = sheet.GetRow(1).GetCell(2);
            mergedCell.SetCellValue("Ф.И.О");
            CellStyleManager.SetCustomCellStyle(mergedCell, 12);

            sheet.AddMergedRegion(new CellRangeAddress(1, 2, 3, endColumn));
            mergedCell = sheet.GetRow(1).GetCell(3);
            mergedCell.SetCellValue($"{((Month)(datetime.Month)).GetDescription()}");
            CellStyleManager.SetCustomCellStyle(mergedCell, 14);

            sheet.AddMergedRegion(new CellRangeAddress(14, 15, 0, endColumn));
            mergedCell = sheet.GetRow(14).GetCell(0);
            mergedCell.SetCellValue("Дежурный должен оставить чистыми электрическую плиту, стол, холодильник, мойку и кухонный гарнитур, произвести влажную уборку и вынести мусор!!!");
            CellStyleManager.SetCustomCellStyle(mergedCell, 16, borders: false);

            sheet.AddMergedRegion(new CellRangeAddress(16, 17, 0, endColumn));
            mergedCell = sheet.GetRow(16).GetCell(0);
            mergedCell.SetCellValue($"{generalDay.GetDescription()} - генеральная уборка!");
            CellStyleManager.SetCustomCellStyle(mergedCell, 14, underline: true, borders: false);

            sheet.AddMergedRegion(new CellRangeAddress(18, 19, 0, endColumn));
            mergedCell = sheet.GetRow(18).GetCell(0);
            CellStyleManager.SetCustomCellStyle(mergedCell, 14, borders: false);

            sheet.AddMergedRegion(new CellRangeAddress(20, 20, 0, endColumn));
            mergedCell = sheet.GetRow(20).GetCell(0);
            mergedCell.SetCellValue("Ответственный за кухню  ______________________________________");
            CellStyleManager.SetCustomCellStyle(mergedCell, 14, borders: false);

            sheet.AddMergedRegion(new CellRangeAddress(21, 21, 0, endColumn));
            mergedCell = sheet.GetRow(21).GetCell(0);
            mergedCell.SetCellValue("P.S.Проверка кухонь осуществляется ежедневно!");
            CellStyleManager.SetCustomCellStyle(mergedCell, 14, underline: true, borders: false);

            IRow row3 = sheet.GetRow(3);
            for (int j = 3; j <= endColumn; j++)
            {
                ICell cell = row3.GetCell(j);
                cell.SetCellValue(j - 2);
            }

        }

        private void LoadPeople()
        {
            foreach (var student in kitchen.Students)
            {
                var num = student.DutyNum + 3;
                var room = student.Room;
                var name = student.Name;

                sheet.GetRow(num).GetCell(0).SetCellValue(student.DutyNum);
                sheet.GetRow(num).GetCell(1).SetCellValue(room);
                sheet.GetRow(num).GetCell(2).SetCellValue(name);
            }
        }

        private void RealizeDutyAlgoritm()
        {
            int rows = dutyTable.GetLength(0);
            int cols = dutyTable.GetLength(1);
            var lastDuty = lastDutyNum;

            for (int j = 0; j < cols; j++)
            {
                bool containsNull = false; // Флаг для проверки наличия null в текущем столбце

                int i = lastDuty; // Начинаем с указанной строки
                for (; i < rows; i++)
                {
                    if (dutyTable[i, j] == null)
                    {
                        containsNull = true;
                        break; // Если найдено null, выходим из цикла
                    }

                    this[i, j] = true; // Заполняем текущую клетку значением true
                }

                // Если дошли до нижней строки или был найден null, переходим к следующей колонке
                if (i == rows || containsNull)
                {
                    lastDuty = lastDuty - 1; // Смещаемся на одну строку выше
                }
                else
                {
                    lastDuty = i; // Иначе обновляем начальную строку для следующей колонки
                }
            }
        }

        private void RealizeDutyAlgoritm2()
        {
            int rows = dutyTable.GetLength(0);
            int cols = dutyTable.GetLength(1);
            var lastDuty = lastDutyNum + 1;

            for (int j = 0; j < cols; j++)
            {
                if (lastDuty == rows)
                {
                    lastDuty = 0;
                }
                if (IsCurrentDayIsGeneral(j + 1))
                {
                    j++;
                }
                this[lastDuty, j] = true;
                lastDuty++;
            }
        }

        private void SetWidthAndHight()
        {
            sheet.GetRow(0).Height = 35 * 20;
            for (int i = 1; i <= 3; i++)
            {
                sheet.GetRow(i).Height = 16 * 20;
            }
            for (int i = 4; i <= endRow; i++)
            {
                sheet.GetRow(i).Height = (int)(20 * 22.5);
            }

            sheet.SetColumnWidth(0, (int)(3.4 * 256));
            sheet.SetColumnWidth(1, (int)(6.5 * 256));
            sheet.SetColumnWidth(2, (int)(35.7 * 256));

            // Задаем ширину для остальных столбцов
            for (int colIndex = 3; colIndex <= endColumn; colIndex++) // Вместо 50 укажите необходимое количество столбцов
            {
                sheet.SetColumnWidth(colIndex, (int)(3 * 256)); // Ширина остальных столбцов (0.61 см)
            }

        }
        private void SetPageLyout()
        {
            // Устанавливаем поля страницы
            sheet.SetMargin(MarginType.LeftMargin, 0.24);
            sheet.SetMargin(MarginType.RightMargin, 0.24);
            sheet.SetMargin(MarginType.TopMargin, 0.16);
            sheet.SetMargin(MarginType.BottomMargin, 0.16);

            // Отключаем колонтитулы
            sheet.Header.Center = string.Empty;
            sheet.Footer.Center = string.Empty;
            sheet.VerticallyCenter = true;
            sheet.HorizontallyCenter = true;

            // Устанавливаем ориентацию страницы (горизонтальная ориентация)
            sheet.PrintSetup.Landscape = true;

            sheet.PrintSetup.PaperSize = 9; //A4
        }

        private void SetSanitaryDays()
        {
            for (int j = 0; j < DateTime.DaysInMonth(datetime.Year, datetime.Month); j++)
            {
                if (IsCurrentDayIsGeneral(j + 1))
                {
                    sheet.AddMergedRegion(new CellRangeAddress(4, tableSize - 1, j + 3, j + 3));
                    ICell mergedCell = sheet.GetRow(4).GetCell(j + 3);
                    mergedCell.SetCellValue($"Санитарный Обход к.{kitchen.Id}");
                    CellStyleManager.setSanitary(mergedCell);


                    //for (int n = 0; n < dutyTable.GetLength(0); n++)
                    //{
                    //    dutyTable[n, j] = null;
                    //}
                }
            }
        }

        public void SaveFile()
        {
            // Запрос пути для сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    // Ваш код для сохранения файла по выбранному пути (filePath)
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        workbook.Write(fs);
                    }

                    MessageBox.Show("Файл успешно сохранен по пути: " + filePath, "Сохранение файла", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Сохранение отменено.", "Отмена", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsCurrentDayIsGeneral(int i)
        {
            if (i >= 1 && i <= DateTime.DaysInMonth(datetime.Year, datetime.Month))
            {
                var dt = new DateOnly(datetime.Year, datetime.Month, i);
                return (int)dt.DayOfWeek == (int)generalDay;
            }
            else return false;
        }
    }
}
