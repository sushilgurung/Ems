using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class ResponseModel<T>
    {
        public string Token { get; set; }
        public bool Status { get; set; }
        public string ReturnMessage { get; set; }
        public Hashtable Errors;
        public int TotalPages;
        public int TotalRows;
        public int PageSize;
        public Boolean IsAuthenicated;
        public T Entity = default(T);
        public bool Validation { get; set; } = true;
        public List<FieldError> ValidationError { get; set; }
        public List<string> ExcelValidationError { get; set; } = new List<string>();
        public string SkipedRow { get; set; }
        // public Pager pager { get; set; }
        public ResponseModel()
        {
            ReturnMessage = "";
            Status = false;
            Errors = new Hashtable();
            TotalPages = 0;
            TotalPages = 0;
            PageSize = 0;
            IsAuthenicated = false;
            Entity = default(T);
            Validation = true;
            ValidationError = new List<FieldError>();
        }
    }
}
