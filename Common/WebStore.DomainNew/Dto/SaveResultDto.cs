using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.DomainNew.Dto
{
    public class SaveResultDto
    {
        //ctor
        public SaveResultDto()
        {
        }

        //ctor
        public SaveResultDto(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            Errors = new List<string>
            {
                errorMessage
            };
        }

        //ctor
        public SaveResultDto(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}
