using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Web;
using Library.Common;
using Library.Model;
using System.Collections;
using System.Globalization;
using Library.DataAcessLayer.Repository;
using Library.DataAcessLayer.Entities;
using AutoMapper;
using System.Web.WebSockets;
using System.Runtime.InteropServices.WindowsRuntime;
using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Library.Implementation
{
    public class ManageEmployeService : IManageEmployeService
    {
        private readonly IManageEmployeRepository _manageEmployeRepository;
        private readonly IGenderRepository _genderRepository;
        public ManageEmployeService(IManageEmployeRepository manageEmployeRepository, IGenderRepository genderRepository)
        {
            this._manageEmployeRepository = manageEmployeRepository;
            this._genderRepository = genderRepository;
        }
        public ResponseModel<List<EmployeeExportListViewModel>> UploadFile(HttpPostedFile hpf)
        {
            ResponseModel<List<EmployeeExportListViewModel>> response = new ResponseModel<List<EmployeeExportListViewModel>>();
            string path = string.Empty;
            try
            {
                string fileName = hpf.FileName;
                string fileExtension = Path.GetExtension(hpf.FileName);
                var allowedExtensions = new[] { ".xlsx", ".xls", ".csv" };
                int fileSize = hpf.ContentLength;
                if (allowedExtensions.Contains(fileExtension))
                {
                    path = SaveFile(hpf);
                    ExcelDataReaderHelpers excelData = new ExcelDataReaderHelpers(path);
                    IEnumerable<DataRow> data = excelData.getData();
                    if (data != null && data.Any())
                    {
                        ResponseModel<List<ManageEmployeeListViewModel>> responseExcel = ValidateFileData(data);
                        if (response.Validation)
                        {
                            int[] ids = InsertBulkEmployeeData(responseExcel.Entity);
                            response.Entity = GetEmployeeListByIds(string.Join(",", ids));

                            response.ExcelValidationError = responseExcel.ExcelValidationError;
                            response.SkipedRow = responseExcel.SkipedRow;
                            response.Validation = responseExcel.Validation;
                            response.ValidationError = responseExcel.ValidationError;
                            response.ExcelValidationError = responseExcel.ExcelValidationError;
                        }
                        response.Status = true;
                    }
                }
                else
                {
                    response.ReturnMessage = "Please Upload xlsx, xls and csv format File only";
                    response.Status = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DeleteFile(path);
            }
            return response;
        }



        public ResponseModel<string> UploadImage(HttpPostedFile hpf)
        {
            ResponseModel<string> response = new ResponseModel<string>();
            string fileName = hpf.FileName;
            string fileExtension = Path.GetExtension(hpf.FileName);
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".ico" };
            int oneMegaByte = 1024 * 1024;
            int validImgSize = oneMegaByte * 4;
            int smallSize = 160;
            int mediumSize = 320;
            int largeSize = 640;
            int fileSize = hpf.ContentLength;
            string saveLocation = string.Empty;
            if (allowedExtensions.Contains(fileExtension))
            {
                if (fileSize > 0 && fileSize < validImgSize)
                {
                    string imageFolder = HttpContext.Current.Server.MapPath("~/File/Employee/");
                    string currentDateTime = DateTime.Now.ToString("yyyyMMddhhmmssffff");
                    string imageName = "EmpImage" + "-" + currentDateTime + Path.GetExtension(hpf.FileName);

                    string original = imageFolder + "/Original/";
                    if (!Directory.Exists(original))
                    {
                        Directory.CreateDirectory(original);
                    }
                    saveLocation = original + imageName;
                    hpf.SaveAs(saveLocation);
                    string smallImageFolder = String.Format("{0}{1}", imageFolder, "smallThumbnail/");
                    string mediumImageFolder = String.Format("{0}{1}", imageFolder, "mediumThumbnail/");
                    string largeImageFolder = String.Format("{0}{1}", imageFolder, "largeThumbnail/");
                    SaveThumbnailImages(saveLocation, smallSize, smallImageFolder, imageName, false);
                    SaveThumbnailImages(saveLocation, mediumSize, mediumImageFolder, imageName, false);
                    SaveThumbnailImages(saveLocation, largeSize, largeImageFolder, imageName, false);
                    response.Status = true;
                    response.Entity = imageName;
                }
                else
                {
                    response.Status = false;
                    response.ReturnMessage = "Please Upload Image size less than 4MB";
                }
            }
            else
            {
                response.Status = false;
                response.ReturnMessage = "Please Upload jpg, png, ico and jpeg format Image only";
            }
            return response;
        }


        public void SaveThumbnailImages(string ImageFilePath, int TargetSize, string TargetLocation, string fileName, bool isZoomThumbnail)
        {
            try
            {
                if (!Directory.Exists(TargetLocation))
                {
                    Directory.CreateDirectory(TargetLocation);
                }

                string saveLocation = TargetLocation + fileName;
                PictureManager.CreateThmnail(ImageFilePath, TargetSize, saveLocation, isZoomThumbnail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ResponseModel<List<ManageEmployeeListViewModel>> ValidateFileData(IEnumerable<DataRow> datalist)
        {
            ResponseModel<List<ManageEmployeeListViewModel>> response = new ResponseModel<List<ManageEmployeeListViewModel>>();
            response.Entity = new List<ManageEmployeeListViewModel>();
            StringBuilder sbError = new StringBuilder();
            List<int> skipedRows = new List<int>();
            int rowNum = 2;
            if (datalist != null && datalist.Any())
            {
                foreach (DataRow row in datalist)
                {
                    sbError.Length = 0;
                    StringBuilder validationError = new StringBuilder();
                    ManageEmployeeListViewModel employeeData = new ManageEmployeeListViewModel()
                    {
                        FullName = row[0] != DBNull.Value ? row[0].ToString() : "",
                        // DateOfBirth = row[1] != DBNull.Value ? Convert.ToDateTime(row[1]) : (DateTime?)null,
                        //Gender = row[2] != DBNull.Value ? row[2].ToString() : "",
                        //Salary = row[3] != DBNull.Value ? Convert.ToDecimal(row[3]) : (Decimal?)null,
                        Designation = row[4] != DBNull.Value ? row[4].ToString() : ""
                    };

                    if (row[0] != DBNull.Value || row[1] != DBNull.Value || row[2] != DBNull.Value || row[3] != DBNull.Value || row[4] != DBNull.Value)
                    {
                        if (row[0] == DBNull.Value)
                        {
                            validationError.Append(" FullName is Blank.");
                        }

                        if (row[1] == DBNull.Value)
                        {
                            validationError.Append(" DateOfBirth is Blank.");
                        }
                        else
                        {
                            string[] dateFormats = {
                                "dd.MM.yyyy hh:mm:ss tt",
                                "dd-MM-yyyy hh:mm:ss tt",
                                "dd/MM/yyyy hh:mm:ss tt",
                                "MM/dd/yyyy hh:mm:ss tt",
                                "M/d/yyyy hh:mm:ss tt",
                                "yyyy/MM/dd hh:mm:ss tt",
                                "dd-MM-yyyy hh:mm:ss tt",
                                "yyyy-MM-dd hh:mm:ss tt",
                                "MM-dd-yyyy hh:mm:ss tt"
                            };
                            if (IsValidDate(row[1].ToString(), dateFormats))
                            {
                                employeeData.DateOfBirth = row[1] != DBNull.Value ? Convert.ToDateTime(row[1]) : (DateTime?)null;
                            }
                            else
                            {
                                validationError.Append(" Invalid Date Format in DateOfBirth.");
                            }
                        }

                        if (row[2] == DBNull.Value)
                        {
                            validationError.Append(" Gender is Blank.");
                        }
                        else
                        {
                            string gender = row[2] != DBNull.Value ? row[2].ToString() : "";
                            _genderRepository.Find(x => x.IsDeleted == false && x.Name == gender).Select(y => y.Id);
                            employeeData.Gender = _genderRepository.Find(x => x.IsDeleted == false && x.Name == gender).Select(y => y.Id).FirstOrDefault();
                        }
                        if (row[3] == DBNull.Value)
                        {
                            validationError.Append(" Salary is Blank.");
                        }
                        else
                        {
                            if (IsDecimal(row[3].ToString()))
                            {
                                employeeData.Salary = row[3] != DBNull.Value ? Convert.ToDecimal(row[3]) : (Decimal?)null;
                            }
                            else
                            {
                                validationError.Append(" Salary is not in numeric.");
                            }
                        }

                        if (row[4] == DBNull.Value)
                        {
                            validationError.Append(" Designation is Blank.");
                        }

                        if (validationError.Length > 0)
                        {
                            sbError.Append("Row: " + rowNum);
                            sbError.Append(validationError);
                            response.ExcelValidationError.Add(sbError.ToString());
                            response.Validation = false;
                        }
                        else
                        {
                            response.Entity.Add(employeeData);
                        }
                    }
                    else
                    {
                        skipedRows.Add(rowNum);
                    }
                    rowNum++;
                }
                if (skipedRows.Count > 0)
                {
                    response.SkipedRow = string.Join(",", skipedRows);
                }
            }
            return response;
        }

        public bool IsValidDate(string value, string[] dateFormats)
        {
            DateTime tempDate;
            bool validDate = DateTime.TryParseExact(value, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out tempDate);
            if (validDate)
                return true;
            else
                return false;
        }


        public bool IsDecimal(string value)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string SaveFile(HttpPostedFile hpf)
        {
            string imageFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/File/Temp/");
            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }
            string currentDateTime = DateTime.Now.ToString("yyyyMMddhhmmssffff");
            string imageName = "fileName" + "-" + currentDateTime + Path.GetExtension(hpf.FileName);
            hpf.SaveAs(imageFolder + imageName);
            return imageFolder + imageName;
        }

        public void DeleteFile(string pdfFile)
        {
            if (File.Exists(pdfFile))
            {
                File.Delete(pdfFile);
            }
        }

        public int[] InsertBulkEmployeeData(List<ManageEmployeeListViewModel> model)
        {
            List<Employee> employee = Mapper.Map<List<ManageEmployeeListViewModel>, List<Employee>>(model);
            employee.ForEach(x => x.CreatedBy = new Guid());
            employee.ForEach(x => x.CreatedOn = DateTime.UtcNow);
            employee.ForEach(x => x.IsImported = true);
            employee.ForEach(x => x.ImportedDate = DateTime.UtcNow);
            _manageEmployeRepository.AddRange(employee);
            _manageEmployeRepository.SaveChanges();
            int[] Ids = employee.Select(x => x.Id).ToArray();
            return Ids;
        }


        public ResponseModel<List<EmployeeListViewModel>> GetEmployeeList(EmployeeViewModelFilter filter)
        {
            ResponseModel<List<EmployeeListViewModel>> response = new ResponseModel<List<EmployeeListViewModel>>();
            response.Entity = _manageEmployeRepository.GetEmployeeList(filter);
            response.Status = true;
            return response;
        }

        public ResponseModel<List<GenderListViewModel>> GetGenderList()
        {
            ResponseModel<List<GenderListViewModel>> response = new ResponseModel<List<GenderListViewModel>>();
            response.Entity = _genderRepository.Find(x => x.IsDeleted == false).Select(x => new GenderListViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            response.Status = true;
            return response;
        }

        public ResponseModel<bool> AddUpdateEmployee(ManageEmployeeCreateViewModel model)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            Employee employee = new Employee();
            if (model.Id > 0)
            {
                employee = _manageEmployeRepository.Get(model.Id);
                employee.FullName = model.FullName;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Gender = model.Gender;
                employee.Designation = model.Designation;
                employee.UpdatedDate = DateTime.UtcNow;
                employee.IsUpdated = true;
                employee.UpdatedDateBy = new Guid();
                employee.ImageName = model.ImageName;
                _manageEmployeRepository.Update(employee);
                response.ReturnMessage = "Employee has been Update Sucessfully";
            }
            else
            {
                employee = Mapper.Map<ManageEmployeeCreateViewModel, Employee>(model);
                employee.CreatedBy = new Guid();
                employee.CreatedOn = DateTime.UtcNow;
                _manageEmployeRepository.Add(employee);
                response.ReturnMessage = "Employee has been Added Sucessfully";
            }

            _manageEmployeRepository.SaveChanges();
            if (employee.Id > 0)
            {
                response.Status = true;
            }
            return response;
        }

        public ResponseModel<EmployeeDetailViewModel> GetEmployeeDetails(int id)
        {
            ResponseModel<EmployeeDetailViewModel> response = new ResponseModel<EmployeeDetailViewModel>();
            response.Entity = Mapper.Map<Employee, EmployeeDetailViewModel>(_manageEmployeRepository.Get(id));
            response.Status = true;
            return response;
        }

        public List<EmployeeExportListViewModel> GetEmployeeListByIds(string exportid)
        {
            int[] arrayExport = exportid.Split(',').Select(Int32.Parse).ToArray();
            List<EmployeeExportListViewModel> employeeList = (from e in _manageEmployeRepository.Find(x => x.IsDeleted == false && arrayExport.Contains(x.Id)).ToList()
                                                              join g in _genderRepository.GetAll()
                                                              on e.Gender equals g.Id
                                                              into t
                                                              from rt in t.DefaultIfEmpty()
                                                              select new EmployeeExportListViewModel()
                                                              {
                                                                  FullName = e.FullName,
                                                                  DateOfBirth = e.DateOfBirth.ToString("MM/dd/yyyy"),
                                                                  Gender = rt.Name,
                                                                  Salary = e.Salary,
                                                                  Designation = e.Designation
                                                              }).ToList();
            return employeeList;
        }



        public string ExportTOCSV(string exportid)
        {
            return WriteTsv(GetEmployeeListByIds(exportid));
        }

        public string WriteTsv<T>(IEnumerable<T> data)
        {
            StringBuilder output = new StringBuilder();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Append(prop.DisplayName); // header
                output.Append("\t");
            }
            output.AppendLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Append(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Append("\t");
                }
                output.AppendLine();
            }
            return output.ToString();
        }

        public PdfPTable WritePdfTable<T>(IEnumerable<T> data)
        {
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;

            StringBuilder output = new StringBuilder();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                PdfPCell cell = new PdfPCell(new Phrase(prop.DisplayName));
                // pdfCell.AddElement()
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(51, 102, 102);
                table.AddCell(cell);
            }

            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {

                    var cell = new PdfPCell(new Phrase(prop.Converter.ConvertToString(
                         prop.GetValue(item))));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell);
                }
            }
            return table;
        }

        public string ExportTOPDF(string exportid)
        {
            string fileName = "pdfho";
            var pdfDocument = new Document();
            var pdfFile = string.Format("{0}{1}.pdf", Path.GetTempPath(), fileName);
            if (File.Exists(pdfFile))
            {
                File.Delete(pdfFile);
            }
            List<string> columns = new List<string>() { "FullName", "DateOfBirth", "Gender", "Salary", "Designation" };
            var pdfWriter = PdfWriter.GetInstance(pdfDocument, new FileStream(pdfFile, FileMode.Create));
            pdfDocument.Open();


            PdfPTable table = WritePdfTable(GetEmployeeListByIds(exportid));
            table.WidthPercentage = 100;
            pdfDocument.Add(table);
            pdfDocument.Close();
            return pdfFile;
        }



        public StringBuilder ExportDateTable(DataTable dataTable)
        {
            var stringBuilder = new StringBuilder();
            for (int column = 0; column < dataTable.Columns.Count; column++)
            {
                stringBuilder.Append(dataTable.Columns[column].ColumnName + ',');
            }
            stringBuilder.Append("\r\n");
            for (int rows = 0; rows < dataTable.Rows.Count; rows++)
            {
                for (int column = 0; column < dataTable.Columns.Count; column++)
                {
                    stringBuilder.Append(dataTable.Rows[rows][column].ToString().Replace(",", ";") + ',');
                }
                stringBuilder.Append("\r\n");
            }
            return stringBuilder;
        }
    }
}
